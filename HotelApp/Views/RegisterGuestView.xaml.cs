using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm;
using HotelApp.ViewModels;
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
using HotelApp.Models;
using System.Windows.Threading;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterGuest.xaml
    /// </summary>
    public partial class RegisterGuestView : Page
    {
        private DispatcherTimer _timer;
        RegisterGuestViewModel vm;
        public RegisterGuestView()
        {
            InitializeComponent();
            this.DataContext =  vm = new RegisterGuestViewModel();

            vm._messenger.Register<GuestModel>(this, ValidateModel);
            vm._messenger.Register<httpResult>(this, RegisterGood);

        }


        private void RegisterGood(object recipient, httpResult message)
        {
            if (message.Result)
            {
                TxbGuardado.Visibility = Visibility.Visible;
                vm.GuestModel = new GuestModel();
            }
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
           
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            TxbGuardado.Visibility = Visibility.Hidden;
            _timer.Stop();
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
        }

        public void Subscribe<TEvent>(EventHandler<TEvent> handler)
        {

        }
    }
}
