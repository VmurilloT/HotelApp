using HotelApp.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : Page
    {
        FontIcon xee;
        Snackbar? snackbar;
        internal RoomsViewModel vm;
        
        public RoomsView()
        {
            InitializeComponent();
            this.DataContext = vm = new RoomsViewModel();
            xee = new FontIcon();
            vm._messenger.Register<string>(this, LoadRooms);
            vm._messenger.Register<object>(this, BooKRoom);
        }

        private void BooKRoom(object recipient, object message)
        {
            //BookRoomView view = new BookRoomView();
            //view.ShowDialog();
        }

        private void LoadRooms(object recipient, string message)
        {
            try
            {
                if (vm.Rooms.Count == 0)
                {
                    vm.LoadRoomsCommand.Execute(null);
                }
            }

            catch {
                xee.Glyph = "❗";

                snackbar = new Snackbar(new SnackbarPresenter())
                {
                    Title = "Error",
                    Content = $"hubo un problema al cargar los datos de los cuertos.",
                    Timeout = TimeSpan.FromSeconds(5),
                    Appearance = ControlAppearance.Danger,
                    Icon = xee

                };

                snackbar.Show();
            }
        }

        private void CardControl_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LstRooms_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LstRooms_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //var item = sender as Wpf.Ui.Controls.ListView;
            //if(item != null && item.SelectedItem != null) 
            //{
            //    BookRoomView v = new BookRoomView();
            //    v.Item = item.SelectedItem;
            //    v.ShowDialog();
            //}
            //item.SelectedItem = null;
        }
    }
}