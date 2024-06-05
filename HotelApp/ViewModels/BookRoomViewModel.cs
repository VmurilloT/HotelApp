using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows;
using HotelApp.Models;
using HotelApp.Services;

namespace HotelApp.ViewModels
{
    public partial class BookRoomViewModel : ObservableObject
    {
        public ObservableCollection<RoomsModel> Rooms {get; set;}
        [ObservableProperty]
        private Visibility _loading;
        
        [RelayCommand]
        private async Task LoadRooms()
        {
            Loading = Visibility.Visible;
            ICRUD<RoomsModel> service = new RoomService();
            var rooms = await service.GetData();
            Rooms.Clear();
            foreach (var item in rooms)
            {
                Rooms.Add(new RoomsModel
                {
                    Name = item.Name,
                    PriceBase = item.PriceBase,
                    RoomId = item.RoomId,
                    RoomTypeId = item.RoomTypeId,
                    Status = item.Status,
                    RoomType = item.RoomType
                });
            }
            Loading = Visibility.Collapsed;
        }
        
        /// <summary>
        /// contenedor para las fechas que se validan mayores/menores
        /// </summary>
        public class sValidateDates()
        {
            public string? Date1 { get; set; }

            public string? Date2 { get; set; }
        }


        internal readonly WeakReferenceMessenger Messenger;
        private DateTime _dateIn;

        public DateTime DateIn {  
            get => _dateIn;
            set {
                
                LoadRoomsCommand.Execute(null);
                if (value.Date < DateTime.Now.Date)
                {
                    Messenger.Send(new sValidateDates() { Date1 = "Hoy", Date2 = "CheckIn"});
                    value = DateTime.Now;
                }
                else if(value.Date > _dateOut.Date)
                {
                    DateOut = value;
                    Messenger.Send("Fecha del CheckOut cambio!");
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
                    Messenger.Send(new sValidateDates() { Date1 = "CheckIn", Date2 = "CheckOut" });
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
            Messenger = WeakReferenceMessenger.Default;
            Rooms = new ObservableCollection<RoomsModel>();
            DateIn = DateTime.Now;
            DateOut = DateTime.Now;
        }
    }
}
