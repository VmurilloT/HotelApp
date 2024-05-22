using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp.ViewModels
{
    internal partial class RegisterGuestViewModel : ObservableObject
    {

        internal readonly WeakReferenceMessenger _messenger;
        [ObservableProperty]
        private GuestModel _guestModel = new GuestModel();


        [RelayCommand]
        public void RegisterGuest()
        {
            httpResult result;
            if (validateFields())
            {
                result = new Services.GuestService().RegisterGuest(_guestModel);

                
                RegisterGood(result);
               
            }

        }

        public RegisterGuestViewModel()
        {
            _messenger = WeakReferenceMessenger.Default;
        }

        private bool validateFields()
        {
            bool result = true;
            if(string.IsNullOrEmpty(_guestModel.Name))
            {
                result = false;
            }

            else if (string.IsNullOrEmpty(_guestModel.LastName))
            {
                result = false;
            }

            else if (string.IsNullOrEmpty(_guestModel.Phone))
            {
                result = false;
            }


            if (!result) SendValidation();
            return result;
        }

        public void SendValidation()
        {
            _messenger.Send(_guestModel);
            
        }

        public void RegisterGood(httpResult message)
        {
            _messenger.Send(message);
        }
    }
}