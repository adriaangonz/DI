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

namespace Practica6
{
    /// <summary>
    /// Lógica de interacción para VentanaDetalle.xaml
    /// </summary>
    public partial class VentanaDetalle : Window
    {
        public VentanaDetalle(Reserva reserva)
        {
            InitializeComponent();
            // asignacion al datacontext
            this.DataContext = reserva;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
