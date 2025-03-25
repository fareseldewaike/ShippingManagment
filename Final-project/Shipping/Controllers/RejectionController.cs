using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.DTO.Rejection;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RejectionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRejections()
        {
            var result = await unitOfWork.Repository<Rejection>().GetAllAsync();

            var rejections = result.Where(r => !r.isDeleted);

            IEnumerable<RejectionDto> rejectionDtos = mapper.Map<IEnumerable<RejectionDto>>(rejections);

            return Ok(rejectionDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRejection(int id)
        {
            var reject = await unitOfWork.Repository<Rejection>().GetByIdAsync(id);

            if (reject == null || reject.isDeleted)
                return NotFound();


            RejectionDto rejectDto = mapper.Map<RejectionDto>(reject);

            return Ok(rejectDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddRejection([FromBody] AddRejectionDto rejectionDto)
        {
            if (rejectionDto == null)
                return BadRequest();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            //manual mapping
            var rejection = new Rejection
            {
                Name = rejectionDto.Name
            };

            await unitOfWork.Repository<Rejection>().AddAsync(rejection);
            await unitOfWork.CompleteAsync();


            return CreatedAtAction("GetRejection", new { id = rejection.Id }, rejection);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRejection(int id, [FromBody] RejectionDto rejectionDto)
        {
            if (rejectionDto == null || id != rejectionDto.Id)
                return BadRequest();


            var Rejection = await unitOfWork.Repository<Rejection>().GetByIdAsync(id);


            if (Rejection == null)
                return NotFound();

            //mapping
            Rejection.Name = rejectionDto.Name;

            unitOfWork.Repository<Rejection>().UpdateAsync(Rejection);
            await unitOfWork.CompleteAsync();

            return NoContent();



        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRejection(int id)
        {
            var rejection = await unitOfWork.Repository<Rejection>().GetByIdAsync(id);
            if (rejection == null)
                return NotFound();

            rejection.isDeleted = true;

            unitOfWork.Repository<Rejection>().UpdateAsync(rejection);
            await unitOfWork.CompleteAsync();

            return Ok(rejection);
        }








    }
}
