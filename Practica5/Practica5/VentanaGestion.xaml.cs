using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Practica5
{
    /// <summary>
    /// Lógica de interacción para VentanaGestion.xaml
    /// </summary>
    public partial class VentanaGestion : Window
    {
        // Guardamos la referencia al mismo objeto Incidencia recibido
        private Incidencia incidencia;

        public VentanaGestion(Incidencia incidencia)
        {
            InitializeComponent();

            this.incidencia = incidencia;

            // Mostramos los datos de la incidencia
            txtTitulo.Text = incidencia.Titulo;
            txtCategoria.Text = incidencia.Categoria;
            txtPrioridad.Text = incidencia.Prioridad;
            txtTiempo.Text = $"{incidencia.Tiempo} minutos";
            txtEstadoActual.Text = incidencia.Estado;

            // Seleccionamos el estado actual en el ComboBox
            SeleccionarEstadoActual();
        }

        private void SeleccionarEstadoActual()
        {
            foreach (ComboBoxItem item in cmbNuevoEstado.Items)
            {
                if (item.Content.ToString() == incidencia.Estado)
                {
                    cmbNuevoEstado.SelectedItem = item;
                    return;
                }
            }

            if (cmbNuevoEstado.Items.Count > 0)
            {
                cmbNuevoEstado.SelectedIndex = 0;
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Asignamos el nuevo estado al objeto que ya teníamos en memoria
            if (cmbNuevoEstado.SelectedItem is ComboBoxItem item)
            {
                incidencia.Estado = item.Content.ToString() ?? "";
            }

            // DialogResult = true indica a la ventana principal que la acción fue confirmada
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerramos sin aplicar cambios sobre el objeto
            this.DialogResult = false;
            this.Close();
        }
    }
}
