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

namespace Practica7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Colecciones observables: notifican cambios a la interfaz automáticamente sin usar Items.Refresh()
        private ObservableCollection<Pedido> pedidosPendientes;
        private ObservableCollection<Pedido> pedidosPreparados;

        public MainWindow()
        {
            InitializeComponent();

            // Inicializamos las colecciones y las enlazamos a sus ListView
            pedidosPendientes = new ObservableCollection<Pedido>();
            pedidosPreparados = new ObservableCollection<Pedido>();

            lvPendientes.ItemsSource = pedidosPendientes;
            lvPreparados.ItemsSource = pedidosPreparados;

            CargarDatosPrueba();
            ActualizarContadores();
        }

        private void CargarDatosPrueba()
        {
            pedidosPendientes.Add(new Pedido { Cliente = "Lucía Gómez", Producto = "Café", Cantidad = 2, TipoRecogida = "En local", Observaciones = "Café con leche desnatada" });
            pedidosPendientes.Add(new Pedido { Cliente = "David Ruiz", Producto = "Bocadillo", Cantidad = 1, TipoRecogida = "Para llevar", Observaciones = "Calentar el pan" });
            pedidosPendientes.Add(new Pedido { Cliente = "Elena Marín", Producto = "Repostería", Cantidad = 3, TipoRecogida = "En local", Observaciones = "" });
            pedidosPreparados.Add(new Pedido { Cliente = "Marcos Soto", Producto = "Té", Cantidad = 1, TipoRecogida = "Para llevar", Observaciones = "Té verde con miel" });
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // validar cliente
            string cliente = txtCliente.Text.Trim();
            if (string.IsNullOrWhiteSpace(cliente))
            {
                MessageBox.Show("El nombre del cliente no puede estar vacío.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCliente.Focus();
                return;
            }

            // validar producto
            if (cmbProducto.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbProducto.Focus();
                return;
            }
            string producto = ((ComboBoxItem)cmbProducto.SelectedItem).Content.ToString() ?? "";

            // validar cantidad
            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero mayor que cero.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCantidad.Focus();
                return;
            }

            // validar tipo de recogida
            if (rbEnLocal.IsChecked != true && rbParaLlevar.IsChecked != true)
            {
                MessageBox.Show("Debe seleccionar un tipo de recogida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string tipoRecogida = rbEnLocal.IsChecked == true ? "En local" : "Para llevar";

            // observaciones opcionales
            string observaciones = txtObservaciones.Text.Trim();

            // crear pedido y añadirlo a la colección de pendientes
            Pedido nuevoPedido = new Pedido
            {
                Cliente = cliente,
                Producto = producto,
                Cantidad = cantidad,
                TipoRecogida = tipoRecogida,
                Observaciones = observaciones
            };

            pedidosPendientes.Add(nuevoPedido);
            ActualizarContadores();
            LimpiarFormulario();
        }

        private void BtnPreparar_Click(object sender, RoutedEventArgs e)
        {
            // comprobar si hay un pedido seleccionado en pendientes
            if (lvPendientes.SelectedItem is Pedido pedidoSeleccionado)
            {
                // enviamos el mismo objeto por constructor a la ventana de confirmación
                VentanaPreparacion ventana = new VentanaPreparacion(pedidoSeleccionado);
                ventana.Owner = this;

                // si el usuario confirma en la ventana modal
                if (ventana.ShowDialog() == true)
                {
                    // trasladamos el mismo objeto de pendientes a preparados sin duplicarlo
                    pedidosPendientes.Remove(pedidoSeleccionado);
                    pedidosPreparados.Add(pedidoSeleccionado);
                    ActualizarContadores();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un pedido de la lista de pendientes.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDevolver_Click(object sender, RoutedEventArgs e)
        {
            // comprobar si hay un pedido seleccionado en preparados
            if (lvPreparados.SelectedItem is Pedido pedidoSeleccionado)
            {
                // trasladamos el mismo objeto de vuelta a pendientes
                pedidosPreparados.Remove(pedidoSeleccionado);
                pedidosPendientes.Add(pedidoSeleccionado);
                ActualizarContadores();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un pedido preparado para devolver a pendientes.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // actualiza los contadores usando la propiedad Count de las colecciones
        private void ActualizarContadores()
        {
            tbContadorPendientes.Text = $"Pedidos pendientes ({pedidosPendientes.Count})";
            tbContadorPreparados.Text = $"Pedidos preparados ({pedidosPreparados.Count})";
        }

        private void LimpiarFormulario()
        {
            txtCliente.Clear();
            cmbProducto.SelectedIndex = -1;
            txtCantidad.Text = "1";
            rbEnLocal.IsChecked = true;
            txtObservaciones.Clear();
            txtCliente.Focus();
        }
    }
}
