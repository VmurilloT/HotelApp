using HotelApp.Models;
using HotelApp.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : Page
    {

        internal RoomsViewModel vm;
        internal readonly WeakReferenceMessenger _messenger;
        public RoomsView()
        {
            InitializeComponent();
            this.DataContext = vm = new RoomsViewModel();
            _messenger = WeakReferenceMessenger.Default;

            _messenger.Register<string>(this, LoadRooms);
        }


        private void LoadRooms(object recipient, string message)
        {
            if(vm.Rooms.Count == 0)
            {
                vm.LoadRoomsCommand.Execute(null);
            }
        }
    }
}