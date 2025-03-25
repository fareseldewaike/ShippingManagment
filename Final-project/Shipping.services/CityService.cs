using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.DTO;

namespace Shipping.services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddCity(CityDTO cityDto)
        {
            var city = new City
            {
                Name = cityDto.Name
            };

            await _unitOfWork.Repository<City>().AddAsync(city);
            await _unitOfWork.CompleteAsync();

        }

        public async Task DeleteCity(int id)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(id);
            if (city == null || city.isDeleted)
                throw new Exception("City not found");
            city.isDeleted = true;
            await _unitOfWork.Repository<City>().UpdateAsync(city);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CityDTO>> GetCities()
        {
            var cities = await _unitOfWork.Repository<City>().GetAllAsync();
            return cities
                .Where(c => !c.isDeleted)
                .Select(c => new CityDTO
                {
                    Name = c.Name
                }).ToList();
        }

        public async Task<CityDTO?> GetCityById(int id)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(id);
            if (city == null || city.isDeleted)
                return null;
            return new CityDTO
            {
                Name = city.Name
            };
        }

        public async Task UpdateCity(int id, CityDTO cityDto)
        {
            var existingCity = await _unitOfWork.Repository<City>().GetByIdAsync(id);
            if (existingCity == null || existingCity.isDeleted)
                throw new Exception("City not found");
            existingCity.Name = cityDto.Name;
            await _unitOfWork.Repository<City>().UpdateAsync(existingCity);
            await _unitOfWork.CompleteAsync();
        }
    }
}
