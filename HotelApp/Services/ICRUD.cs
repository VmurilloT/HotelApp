using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public interface ICRUD<T>
    {
        Task<httpResult> Register(T entity);
        Task<List<T>> GetData();
    }
}
