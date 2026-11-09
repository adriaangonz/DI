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

namespace Practica6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Reserva> listaReservas;

        public MainWindow()
        {
            InitializeComponent();

            listaReservas = new ObservableCollection<Reserva>();
            lvReservas.ItemsSource = listaReservas;

            CargarDatosPrueba();
            ActualizarContador();
        }

        private void CargarDatosPrueba()
        {
            listaReservas.Add(new Reserva { Nombre = "Laura Garcia", Actividad = "Visita guiada", Fecha = new DateTime(2026, 10, 29), Turno = "Mañana", Plazas = 2 });
            listaReservas.Add(new Reserva { Nombre = "Carlos Martinez", Actividad = "Taller", Fecha = new DateTime(2026, 10, 30), Turno = "Tarde", Plazas = 1 });
            listaReservas.Add(new Reserva { Nombre = "Ana Lopez", Actividad = "Ruta", Fecha = new DateTime(2026, 11, 5), Turno = "Mañana", Plazas = 3 });
            listaReservas.Add(new Reserva { Nombre = "Pedro Sanchez", Actividad = "Actividad deportiva", Fecha = new DateTime(2026, 11, 7), Turno = "Tarde", Plazas = 2 });
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // validar nombre
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacio", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNombre.Focus();
                return;
            }

            // validar actividad
            if (cmbActividad.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una actividad", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbActividad.Focus();
                return;
            }
            string actividad = ((ComboBoxItem)cmbActividad.SelectedItem).Content.ToString();

            //validar fecha
            if (!dpFecha.SelectedDate.HasValue)
            {
                MessageBox.Show("Debe seleccionar una fecha", "Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpFecha.Focus();
                return;
            }
            DateTime fecha = dpFecha.SelectedDate.Value;

            //validar turno
            if (rbManana.IsChecked != true && rbTarde.IsChecked != true)
            {
                MessageBox.Show("Debe seleccionar un turno (Mañana o Tarde).", "Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string turno = rbManana.IsChecked == true ? "Mañana" : "Tarde";

            //validar plazas
            if (!int.TryParse(txtPlazas.Text.Trim(), out int plazas) || plazas <= 0)
            {
                MessageBox.Show("El numero de plazas debe ser un numero entero mayor que cero", "Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPlazas.Focus();
                return;
            }

            //crear reserva y añadirla
            Reserva nuevaReserva = new Reserva
            {
                Nombre = nombre,
                Actividad = actividad,
                Fecha = fecha,
                Turno = turno,
                Plazas = plazas
            };

            listaReservas.Add(nuevaReserva);
            ActualizarContador();
            LimpiarFormulario();
        }

        private void BtnDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (lvReservas.SelectedItem is Reserva reservaSeleccionada)
            {
                VentanaDetalle ventana = new VentanaDetalle(reservaSeleccionada);
                ventana.Owner = this;
                ventana.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva de la lista", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lvReservas.SelectedItem is Reserva reservaSeleccionada)
            {
                MessageBoxResult resultado = MessageBox.Show(
                    "Quieres eliminar la reserva de {reservaSeleccionada.Nombre}?",
                    "Confirmar eliminacion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    listaReservas.Remove(reservaSeleccionada);
                    ActualizarContador();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva de la lista para eliminarla.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ActualizarContador()
        {
            lblContador.Text = listaReservas.Count.ToString();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            cmbActividad.SelectedIndex = -1;
            dpFecha.SelectedDate = null;
            rbManana.IsChecked = true;
            txtPlazas.Text = "1";
        }
    }
}