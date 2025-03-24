using Microsoft.AspNetCore.Mvc;
using Shipping.core.Interfaces;
using Shipping.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly IGovernorateService _governorateService;

        public GovernorateController(IGovernorateService governorateService)
        {
            _governorateService = governorateService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<GovernorateDTO>>> GetGovernorates()
        {
            var governorates = await _governorateService.GetGovernorates();

            if (governorates == null || !governorates.Any())
            {
                return NotFound(new { Status = 404, Message = "No governorates found." });
            }

            return Ok(governorates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GovernorateDTO>> GetGovernorateById(int id)
        {
            var governorate = await _governorateService.GetGovernorateById(id);
            if (governorate == null)
            {
                return NotFound(new { Status = 404, Message = "Governorate not found." });
            }
            return Ok(governorate);
        }

        [HttpPost]
        public async Task<ActionResult> AddGovernorate(GovernorateDTO governorateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _governorateService.AddGovernorate(governorateDto);
            return Ok(new { Status = 201, Message = "Governorate created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGovernorate(int id, GovernorateDTO governorateDto)
        {
            var existingGovernorate = await _governorateService.GetGovernorateById(id);
            if (existingGovernorate == null)
            {
                return NotFound(new { Status = 404, Message = "Governorate not found." });
            }

            await _governorateService.UpdateGovernorate(id, governorateDto);
            return Ok(new { Status = 200, Message = "Governorate updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGovernorate(int id)
        {
            var existingGovernorate = await _governorateService.GetGovernorateById(id);
            if (existingGovernorate == null)
            {
                return NotFound(new { Status = 404, Message = "Governorate not found." });
            }

            await _governorateService.DeleteGovernorate(id);
            return Ok(new { Status = 200, Message = "Governorate deleted successfully." });
        }
    }
}
