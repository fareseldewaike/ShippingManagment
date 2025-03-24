using Shipping.core.Models;
using Shipping.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
 
namespace Shipping.core.Interfaces
{
    public interface IGovernorateService
    {
        Task<IEnumerable<GovernorateDTO>> GetGovernorates();
        Task<GovernorateDTO?> GetGovernorateById(int id);
        Task AddGovernorate(GovernorateDTO governorateDto);
        Task UpdateGovernorate(int id,GovernorateDTO governorateDto);
        Task DeleteGovernorate(int id);
    }
}
