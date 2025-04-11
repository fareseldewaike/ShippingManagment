using DTOs.DTO;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.services
{
    public class GovernorateService : IGovernorateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GovernorateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GovernorateDTO>> GetGovernorates()
        {
            var governorates = await _unitOfWork.Repository<Governorate>().GetAllAsync();

            return governorates
        .Where(g => !g.IsDeleted)
        .Select(g => new GovernorateDTO
        {
            Id = g.Id,
            Name = g.Name,
            Cities = g.Cities.Where(c => !c.isDeleted)
                             .Select(c => new CityDTO { Name = c.Name, id = c.Id, Pickup = c.Pickup, Price = c.Price }).ToList() // Convert to CityDTO
        }).ToList();

        }

        public async Task<GovernorateDTO?> GetGovernorateById(int id)
        {
            var governorate = await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);

            if (governorate == null || governorate.IsDeleted)
                return null;

            return new GovernorateDTO
            {
                Id = governorate.Id,
                Name = governorate.Name,
                Cities = governorate.Cities.Select(c => new CityDTO { Name = c.Name, id = c.Id, Pickup = c.Pickup, Price = c.Price }).ToList() // Convert to CityDTO
            };
        }


        public async Task AddGovernorate(GovernorateAdd governorateDto)
        {
            var governorate = new Governorate
            {
                Name = governorateDto.Name
            };

            await _unitOfWork.Repository<Governorate>().AddAsync(governorate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateGovernorate(int id, GovernorateDTO governorateDto)
        {
            var existingGovernorate = await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);
            if (existingGovernorate == null || existingGovernorate.IsDeleted)
                throw new KeyNotFoundException("Governorate not found.");

            existingGovernorate.Name = governorateDto.Name;

            await _unitOfWork.Repository<Governorate>().UpdateAsync(existingGovernorate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteGovernorate(int id)
        {
            var governorate = await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);
            if (governorate == null || governorate.IsDeleted)
                throw new KeyNotFoundException("Governorate not found.");

            governorate.IsDeleted = true;
            await _unitOfWork.Repository<Governorate>().UpdateAsync(governorate);
            await _unitOfWork.CompleteAsync();
        }
    }
}
