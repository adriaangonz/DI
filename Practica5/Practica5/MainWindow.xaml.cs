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
        private List<Incidencia> listaIncidencias = new List<Incidencia>();
        public MainWindow()
        {
            InitializeComponent();
            lvIncidencias.ItemsSource = listaIncidencias;
        }

        private void BtnRegistrar_Click(
            object sender,
            RoutedEventArgs e)
        {
            //validamos titulo
            if (String.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show(
                    "El nombre es obligatorio");
                return;
            }
            //validar categoria
            if (cbCategoria.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selecciona una categoria");
                return;
            }
            //validar prioridad
            if (rbAlta.IsChecked != true && rbMedia.IsChecked != true && rbBaja.IsChecked != true)
            {
                MessageBox.Show(
                    "Selecciona una prioridad");
                return;
            }

            //validar edad
            if (!int.TryParse(txtTiempo.Text, out int tiempo))
            {
                MessageBox.Show(
                    "Introduce una edad válida",
                    "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //obtener categoria
            string categoria = (cbCategoria.SelectedItem as ComboBoxItem)?
                .Content?
                .ToString() ?? "";

            //obtener prioridad
            string prioridad;
            if (rbAlta.IsChecked == true)
            {
                prioridad = "Alta";
            }
            if (rbMedia.IsChecked == true)
            {
                prioridad = "Media";
            }
            else
            {
                prioridad = "Baja";
            }

            //crear incidencia
            Incidencia incidencia = new Incidencia()
            {
                Titulo = txtTitulo.Text,
                Categoria = categoria,
                Prioridad = prioridad,
                Tiempo = tiempo
            };

            //añadir lista
            listaIncidencias.Add(incidencia);
            //actualizar lista
            lvIncidencias.Items.Refresh();
        }
    }
}