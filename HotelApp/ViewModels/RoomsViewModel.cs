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

namespace HotelApp.ViewModels
{
    public partial class RoomsViewModel : ObservableObject
    {
        public ObservableCollection<RoomsModel> Rooms {get; set;}
        ICRUD<RoomsModel> RoomCRUD;
        [RelayCommand]
        private async void LoadRooms()
        {
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
        }

        public RoomsViewModel()
        {
            Rooms = new ObservableCollection<RoomsModel>();
        }


        public class objetosRicos
        {
            public string Name { get; set; } = string.Empty;
        }
    }
}
