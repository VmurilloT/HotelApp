using MahApps.Metro.IconPacks;
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
using Wpf.Ui.Controls;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.MainWindowsViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm = new ViewModels.MainWindowsViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.Psw = Txt_Psw.Password;
            vm.ValidateLoginCommand.Execute(null);

            
            if (vm.Login)
            {
                Views.Dashboard dashboard = new Views.Dashboard();
                dashboard.Show();
                this.Close();
            }
            else
            {
                FontIcon xee = new FontIcon();
                xee.Glyph = "✖️";

                Snackbar snackbar = new Snackbar(SnackbarPresenter)
                {
                    Title = "Error",
                    Content = "Ocurrio un error validando las credenciales.",
                    Timeout = TimeSpan.FromSeconds(2),
                    Appearance = ControlAppearance.Danger,
                    Icon = xee
                   
                };

                snackbar.Show();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

       

    }
}