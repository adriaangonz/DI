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

namespace Practica7
{
    /// <summary>
    /// Lógica de interacción para VentanaPreparacion.xaml
    /// </summary>
    public partial class VentanaPreparacion : Window
    {
        public VentanaPreparacion(Pedido pedido)
        {
            InitializeComponent();

            // Asignación al DataContext para permitir el enlace de datos (Binding) con los controles
            this.DataContext = pedido;
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            // DialogResult = true indica a la ventana principal que se confirmó la preparación
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // DialogResult = false indica cancelación, por lo que el pedido no se mueve
            this.DialogResult = false;
            this.Close();
        }
    }
}
