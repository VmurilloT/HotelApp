using System;
using System.CodeDom;
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
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Messaging;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void NavigationView_Navigated(Wpf.Ui.Controls.NavigationView sender, Wpf.Ui.Controls.NavigatedEventArgs args)
        {
            if(args.Page.GetType() == typeof(RoomsView))
            {
                ((RoomsView)args.Page).vm._messenger.Send("Cargar...");
            }
        }
    }
}