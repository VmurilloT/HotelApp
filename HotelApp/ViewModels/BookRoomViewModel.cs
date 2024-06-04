using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public partial class BookRoomViewModel : ObservableObject
    {
        public class sValidateDates()
        {
            public string date1 { get; set; }

            public string date2 { get; set; }
        }


        internal readonly WeakReferenceMessenger _messenger;
        private DateTime _dateIn;

        public DateTime DateIn {  
            get { return _dateIn; } 
            set {
                
                if (value.Date < DateTime.Now.Date)
                {
                    _messenger.Send(new sValidateDates() { date1 = "Hoy", date2 = "CheckIn"});
                    value = DateTime.Now;
                }
                else if(value.Date > _dateOut.Date)
                {
                    DateOut = value;
                    _messenger.Send("Fecha del CheckOut cambio!");
                }
                SetProperty(ref _dateIn, value);  
            } 
        }

        private DateTime _dateOut;

        public DateTime DateOut
        {
            get { return _dateOut; }
            set
            {
                if (_dateIn.Date > value.Date)
                {
                    _messenger.Send(new sValidateDates() { date1 = "CheckIn", date2 = "CheckOut" });
                    value = _dateIn.Date;
                }
                SetProperty(ref _dateOut, value);
            }
        }

        [RelayCommand]
        public void presioname()
        {
            
        }
        
        
        public bool isBusy { get; set; } = false;
        public BookRoomViewModel()
        {
            _messenger = WeakReferenceMessenger.Default;
            DateIn = DateTime.Now;
            DateOut = DateTime.Now;
        }
    }
}
