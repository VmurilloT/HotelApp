using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelApp.Models;
using HotelApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp.ViewModels
{
    public partial class RoomsViewModel : ObservableObject
    {
        public ObservableCollection<RoomsModel> Rooms {get; set;}
        internal readonly WeakReferenceMessenger _messenger;
        ICRUD<RoomsModel> RoomCRUD;
        [ObservableProperty]
        private Visibility _loading;

        [RelayCommand]
        private void BookRoom()
        {
            
            _messenger.Send(new object());
        }

        [RelayCommand]
        private async void LoadRooms()
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

        public RoomsViewModel()
        {
            _messenger = WeakReferenceMessenger.Default;
            Rooms = new ObservableCollection<RoomsModel>();
        }

    }
}