using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IUnitOfWork _weightRepository;

        public WeightController(IUnitOfWork weightRepository)
        {
            _weightRepository = weightRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _weightRepository.Repository<Weight>().GetAllAsync();

            return Ok(result);
        }

        // GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var weight = await _weightRepository.Repository<Weight>().GetByIdAsync(id);
            if (weight == null)
            {
                return NotFound();
            }
            return Ok(weight);
        }

        // Add
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Weight weight)
        {
            //if (weight == null)
            //{
            //    return BadRequest("Invalid data.");
            //}

            //await _weightRepository.Repository<Weight>().AddAsync(weight);
            //await _weightRepository.CompleteAsync();

            //return CreatedAtAction(nameof(GetById), new { id = weight.Id }, weight);

            var existingWeightt = await _weightRepository.Repository<Weight>().GetAllAsync();
            var existingWeight = existingWeightt.FirstOrDefault();
            if (existingWeight == null)
            {
                return NotFound();
            }

            existingWeight.DefaultWeight = weight.DefaultWeight;
            existingWeight.AdditionalPrice = weight.AdditionalPrice;

            _weightRepository.Repository<Weight>().UpdateAsync(existingWeight);
            await _weightRepository.CompleteAsync();

            return NoContent();
        }
        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Weight weight)
        {
            if (weight == null || id != weight.Id)
            {
                return BadRequest("Invalid data.");
            }
            var existingWeight = await _weightRepository.Repository<Weight>().GetByIdAsync(id);
            if (existingWeight == null)
            {
                return NotFound();
            }

            existingWeight.DefaultWeight = weight.DefaultWeight;
            existingWeight.AdditionalPrice = weight.AdditionalPrice;

            _weightRepository.Repository<Weight>().UpdateAsync(existingWeight);
            await _weightRepository.CompleteAsync();

            return NoContent();
        }
    }
}