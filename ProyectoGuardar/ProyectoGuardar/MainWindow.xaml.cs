using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ProyectoGuardar
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CrearInterfaz();
        }

        private void CrearInterfaz()
        {
           
            StackPanel contenedorPrincipal = new StackPanel();
            contenedorPrincipal.Orientation = Orientation.Vertical;
            contenedorPrincipal.Margin = new Thickness(10);

        
            TextBox txtTexto = new TextBox();
            txtTexto.Width = 200;
            txtTexto.Height = 100;
            contenedorPrincipal.Children.Add(txtTexto);

            Button btnGuardarClasico = new Button();
            btnGuardarClasico.Content = "Guardar al estilo clásico";
            btnGuardarClasico.Click += (sender, e) => BtnGuardarClasico_Click(txtTexto);
            contenedorPrincipal.Children.Add(btnGuardarClasico);

            Button btnGuardarWpf = new Button();
            btnGuardarWpf.Content = "Guardar al estilo WPF";
            btnGuardarWpf.Click += (sender, e) => BtnGuardarWpf_Click(txtTexto);
            contenedorPrincipal.Children.Add(btnGuardarWpf);

           
            this.Content = contenedorPrincipal;
        }

        private void BtnGuardarClasico_Click(TextBox txtTexto)
        {
            string textoIngresado = txtTexto.Text;
            GuardarTextoEnFichero(textoIngresado, "textos_clasicos.txt");

            MessageBox.Show("Texto guardado en el fichero de estilo clásico.", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnGuardarWpf_Click(TextBox txtTexto)
        {
            string textoIngresado = txtTexto.Text;
            GuardarTextoEnAppConfig(textoIngresado, "TextoGuardado");

            MessageBox.Show("Texto guardado en App.config al estilo de WPF.", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GuardarTextoEnFichero(string texto, string rutaArchivo)
        {
            File.AppendAllText(rutaArchivo, texto + System.Environment.NewLine);
        }

        private void GuardarTextoEnAppConfig(string texto, string key)
        {
            try
            {
               
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

               
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = texto;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, texto);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show($"Error al guardar en App.config: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}