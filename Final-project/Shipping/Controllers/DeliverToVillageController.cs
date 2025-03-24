using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.core.Models;
using Shipping.repo.Implementation;
using Shipping.DTO;
using DTOs.DTO;


namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverToVillageController : ControllerBase
    {
        private readonly UnitOfWork _unit;

        public DeliverToVillageController(UnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliverToVillage>>> GetALL()
        {
            IEnumerable<DeliverToVillage> Delive = await _unit.Repository<DeliverToVillage>().GetAllAsync();
            return Ok(Delive.ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliverToVillage>> GetById(int id)
        {
            DeliverToVillage Delive = await _unit.Repository<DeliverToVillage>().GetByIdAsync(id);
            if (Delive == null)
            {
                return NotFound();
            }
            return Ok(Delive);
        }
        [HttpPost]
        public async Task<ActionResult<DeliverToVillage>> Add(DeliverToVillageDTO Delive)
        {
            if (Delive == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            DeliverToVillage deliverToVillage = new DeliverToVillage
            {
                AdditionalCost = Delive.AdditionalCost
            };
            await _unit.Repository<DeliverToVillage>().AddAsync(deliverToVillage);
            await _unit.CompleteAsync();

            return Created("created", deliverToVillage);
        }
        [HttpDelete]
        public async Task<ActionResult<DeliverToVillage>> Delete(int id)
        {
            DeliverToVillage Delive = await _unit.Repository<DeliverToVillage>().GetByIdAsync(id);
            if (Delive == null)
            {
                return NotFound();
            }
            await _unit.Repository<DeliverToVillage>().DeleteAsync(Delive);
            await _unit.CompleteAsync();
            return Ok(Delive);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<DeliverToVillage>> Update(int id, DeliverToVillage Delive)
        {
            if (Delive == null) return BadRequest();
            if (id != Delive.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                await _unit.Repository<DeliverToVillage>().UpdateAsync(Delive);
                await _unit.CompleteAsync();
                return NoContent();
            }
            else return BadRequest(ModelState);
        }
    }
}
