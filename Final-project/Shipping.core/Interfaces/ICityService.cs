using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.DTO;

namespace Shipping.core.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetCities();
        Task<CityDTO?> GetCityById(int id);
        Task AddCity(CityDTO cityDto);
        Task UpdateCity(int id, CityDTO cityDto);
        Task DeleteCity(int id);
    }
}
