using HotelApp.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for BookRoomView.xaml
    /// </summary>
    public partial class BookRoomView : Page
    {
        FontIcon xee;
        Snackbar? snackbar;
        BookRoomViewModel vm;
        public BookRoomView()
        {
            InitializeComponent();
            this.DataContext = vm = new BookRoomViewModel();
            xee = new FontIcon();
            vm._messenger.Register<BookRoomViewModel.sValidateDates>(this, ValidateDates);
            vm._messenger.Register<string>(this, DateChanged);
        }

        private void DateChanged(object recipient, string message)
        {
            xee.Glyph = "❗";

            snackbar = new Snackbar(this.SnackbarPresenter)
            {
                Title = "Alerta",
                Content = message,
                Timeout = TimeSpan.FromSeconds(5),
                Appearance = ControlAppearance.Caution,
                Icon = xee

            };

            snackbar.Show();
        }

        private void ValidateDates(object recipient, BookRoomViewModel.sValidateDates message)
        {
            

            xee.Glyph = "✖️";

            snackbar = new Snackbar(this.SnackbarPresenter)
            {
                Title = "Error",
                Content = $"La fecha de {message.date1} no puede ser mayor a la fecha de {message.date2}",
                Timeout = TimeSpan.FromSeconds(5),
                Appearance = ControlAppearance.Danger,
                Icon = xee

            };

            snackbar.Show();
        }

    }
}