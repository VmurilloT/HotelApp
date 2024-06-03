using CommunityToolkit.Mvvm.Messaging;
using HotelApp.ViewModels;
using System.Windows.Controls;
using System.Windows.Media;
using HotelApp.Models;
using Wpf.Ui.Controls;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterGuest.xaml
    /// </summary>
    public partial class RegisterGuestView : Page
    {
        FontIcon xee;
        Snackbar? snackbar;
        RegisterGuestViewModel vm;
        public RegisterGuestView()
        {
            InitializeComponent();
            this.DataContext =  vm = new RegisterGuestViewModel();
            xee = new FontIcon();
            vm._messenger.Register<GuestModel>(this, ValidateModel);
            vm._messenger.Register<httpResult>(this, RegisterGood);

        }

        private void RegisterGood(object recipient, httpResult message)
        {
            if (message.Result)
            {
                xee.Glyph = "✔️";

                snackbar = new Snackbar(this.SnackbarPresenter)
                {
                    Title = "Correcto",
                    Content = "Huesped guardado correctamente.",
                    Timeout = TimeSpan.FromSeconds(2),
                    Appearance = ControlAppearance.Success,
                    Icon = xee

                };

                snackbar.Show();
                vm.GuestModel = new GuestModel();
            }
           
           
        }


        private void ValidateModel(object recipient, GuestModel message)
        {
            TxtLastName.BorderBrush = TxtName.BorderBrush = TxtPhone.BorderBrush = Brushes.Transparent;
            if (string.IsNullOrEmpty(message.LastName)) 
            {
                TxtLastName.BorderBrush = Brushes.Red;
            }

            if(string.IsNullOrEmpty(message.Name))
            {
                TxtName.BorderBrush = Brushes.Red;
            }

            if(string.IsNullOrEmpty(message.Phone))
            {
                TxtPhone.BorderBrush = Brushes.Red;
            }
            
            xee.Glyph = "❗";

            snackbar = new Snackbar(SnackbarPresenter)
            {
                Title = "Error",
                Content = "Faltan datos por capturar.",
                Timeout = TimeSpan.FromSeconds(2),
                Appearance = ControlAppearance.Danger,
                Icon = xee

            };

            snackbar.Show();
        }

        public void Subscribe<TEvent>(EventHandler<TEvent> handler)
        {

        }
    }
}
