using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp.ViewModels
{
    internal partial class MainWindowsViewModel : ObservableObject
    {
        #region PROPERTIES
        [ObservableProperty]
        private string _usr = string.Empty;

        [ObservableProperty]
        private string _psw = string.Empty;

        [ObservableProperty]
        private bool _login = false;
        #endregion

        internal MainWindowsViewModel()
        {

        }

        [RelayCommand]
        private void ValidateLogin()
        {
            if (Usr.Equals("Admin") && Psw.Equals("123"))
            {
                Login = true;
            }
        }

    }
}
