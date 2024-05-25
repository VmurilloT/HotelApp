using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HotelApp.Models
{
    public class RoomsModel
    {
        public int RoomId { set; get; }
        public string Name { set; get; } = string.Empty;
        public int RoomTypeId { set; get; }
        public int Status { set; get; }

        public SolidColorBrush StatusColor { 
                                      get 
                                        { 
                                            return Status == 1 ? Brushes.Linen : Brushes.IndianRed; 
                                        }  
                                    }
        public decimal PriceBase { set; get; }

        public RoomTypesModel RoomType { set; get; }
    }
}
