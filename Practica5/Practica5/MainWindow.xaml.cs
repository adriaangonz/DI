using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practica5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Colección observable para almacenar las incidencias en memoria
        private ObservableCollection<Incidencia> listaIncidencias;

        public MainWindow()
        {
            InitializeComponent();

            listaIncidencias = new ObservableCollection<Incidencia>();
            lvIncidencias.ItemsSource = listaIncidencias;

            CargarDatosPrueba();
            ActualizarContador();
        }

        private void CargarDatosPrueba()
        {
            listaIncidencias.Add(new Incidencia { Titulo = "Fallo en la tarjeta de red", Categoria = "Hardware", Prioridad = "Alta", Tiempo = 45, Estado = "Pendiente" });
            listaIncidencias.Add(new Incidencia { Titulo = "Actualización sistema operativo", Categoria = "Software", Prioridad = "Media", Tiempo = 60, Estado = "En proceso" });
            listaIncidencias.Add(new Incidencia { Titulo = "Configurar router planta 2", Categoria = "Red", Prioridad = "Baja", Tiempo = 30, Estado = "Resuelta" });
            listaIncidencias.Add(new Incidencia { Titulo = "Sustitución de periféricos", Categoria = "Otros", Prioridad = "Media", Tiempo = 20, Estado = "Pendiente" });
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // validar título
            string titulo = txtTitulo.Text.Trim();
            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("El título no puede estar vacío.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTitulo.Focus();
                return;
            }

            // validar categoría
            if (cbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbCategoria.Focus();
                return;
            }
            string categoria = ((ComboBoxItem)cbCategoria.SelectedItem).Content.ToString() ?? "";

            // validar prioridad
            if (rbAlta.IsChecked != true && rbMedia.IsChecked != true && rbBaja.IsChecked != true)
            {
                MessageBox.Show("Debe seleccionar una prioridad (Baja, Media o Alta).", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string prioridad = rbAlta.IsChecked == true ? "Alta" : (rbMedia.IsChecked == true ? "Media" : "Baja");

            // validar tiempo estimado (entero mayor que cero)
            if (!int.TryParse(txtTiempo.Text.Trim(), out int tiempo) || tiempo <= 0)
            {
                MessageBox.Show("El tiempo estimado debe ser un número entero mayor que cero.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTiempo.Focus();
                return;
            }

            // crear la incidencia con estado inicial Pendiente
            Incidencia nuevaIncidencia = new Incidencia
            {
                Titulo = titulo,
                Categoria = categoria,
                Prioridad = prioridad,
                Tiempo = tiempo,
                Estado = "Pendiente"
            };

            listaIncidencias.Add(nuevaIncidencia);
            ActualizarContador();
            LimpiarFormulario();
        }

        private void BtnGestionar_Click(object sender, RoutedEventArgs e)
        {
            // comprobar si hay un elemento seleccionado
            if (lvIncidencias.SelectedItem is Incidencia incidenciaSeleccionada)
            {
                // enviamos el mismo objeto por el constructor a la segunda ventana
                VentanaGestion ventana = new VentanaGestion(incidenciaSeleccionada);
                ventana.Owner = this;

                // ShowDialog abre de forma modal y DialogResult indica si se aceptó
                if (ventana.ShowDialog() == true)
                {
                    // Items.Refresh() refresca la vista del ListView para mostrar el nuevo estado
                    lvIncidencias.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una incidencia de la lista.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private void ActualizarContador()
        {
            lblContador.Text = listaIncidencias.Count.ToString();
        }

        private void LimpiarFormulario()
        {
            txtTitulo.Clear();
            cbCategoria.SelectedIndex = -1;
            rbBaja.IsChecked = false;
            rbMedia.IsChecked = true;
            rbAlta.IsChecked = false;
            txtTiempo.Clear();
            txtTitulo.Focus();
        }
    }
}
