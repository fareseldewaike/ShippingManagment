using AutoMapper;
using DTOs.DTO.Branch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.DTO.Rejection;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BranchController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAllBranches()
        {
            var result = await unitOfWork.Repository<Branch>().GetAllAsync();

            var branches = result.Where(r => !r.isDeleted);

            IEnumerable<BranchDto> BranchesDto = mapper.Map<IEnumerable<BranchDto>>(branches);

            return Ok(BranchesDto);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetBranch(int id)
        {
            var branch = await unitOfWork.Repository<Branch>().GetByIdAsync(id);

            if (branch == null || branch.isDeleted)
                return NotFound();


            BranchDto branchDto = mapper.Map<BranchDto>(branch);

            return Ok(branchDto);
        }



        [HttpPost]
        public async Task<ActionResult<AddBranchDto>> AddBranch(AddBranchDto branchDto)
        {
            if (branchDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var branch = mapper.Map<Branch>(branchDto);

            await unitOfWork.Repository<Branch>().AddAsync(branch);
            await unitOfWork.CompleteAsync();

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branchDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, AddBranchDto branchDto)
        {
            if (branchDto == null)
                return BadRequest();

            var branch = await unitOfWork.Repository<Branch>().GetByIdAsync(id);


            if (branch == null)
                return NotFound();

            //mapping
            branch.Name = branchDto.Name;
            branch.status = branchDto.status;


            unitOfWork.Repository<Branch>().UpdateAsync(branch);
            await unitOfWork.CompleteAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await unitOfWork.Repository<Branch>().GetByIdAsync(id);
            if (branch == null)
                return NotFound();

            branch.isDeleted = true;

            unitOfWork.Repository<Branch>().UpdateAsync(branch);
            await unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeBranchStatus(int id, ChangeStatusDto branchDto)
        {
            var branch = await unitOfWork.Repository<Branch>().GetByIdAsync(id);

            if (branch == null || branch.isDeleted)
                return NotFound();

            branch.status = branchDto.status;
            await unitOfWork.CompleteAsync();

            return NoContent();
        }




    }
}
