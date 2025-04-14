using DTOs.DTO.representative;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {



        private readonly ShippingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RepresentativeController(ShippingContext shippingContext, UserManager<ApplicationUser> userManager)
        {
            _context = shippingContext;
            _userManager = userManager;

        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRepresentatives()
        {
            var representatives = (await _userManager.GetUsersInRoleAsync("Representative")).Cast<Representative>().ToList();
            if (representatives == null)
            {
                return NotFound();
            }

            var representativeDTOs = representatives.Select(r =>
            {
                var gs = _context.RepresentativeGovernorates
                    .Include(g => g.Governorate)
                    .Where(g => g.RepresentativeId == r.Id)
                    .ToList();
                var f = new representativeDTO
                {

                    Name = r.Name,
                    Address = r.Address,
                    Phone = r.PhoneNumber,
                    Email = r.Email,
                    brancheName = r.branch.Name,
                    governorates = gs.Select(g => new governorateDTORep
                    {
                        Name = g.Governorate.Name
                    }).ToList()
                };
                return f;

            });
            return Ok(representativeDTOs);
        }







        [HttpGet("{id}")]
        public async Task<IActionResult> GetRepresentativeById(string id)
        {
            var representative = await _userManager.FindByIdAsync(id);
            if (representative == null)
            {
                return NotFound();
            }
            var representativeDTO = new representativeDTO
            {
                Name = representative.Name,
                Address = representative.Address,
                Phone = representative.PhoneNumber,
                Email = representative.Email,
                brancheName = representative.branch.Name,
                governorates = _context.RepresentativeGovernorates
                    .Include(g => g.Governorate)
                    .Where(g => g.RepresentativeId == representative.Id)
                    .Select(g => new governorateDTORep
                    {
                        Name = g.Governorate.Name
                    }).ToList()
            };
            return Ok(representativeDTO);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepresentative(string id)
        {
            var representative = await _userManager.FindByIdAsync(id);
            if (representative == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(representative);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<ActionResult> registerrepresentative(Representativeadd representativeAdd)
        {
            var branchExists = await _context.Branches.AnyAsync(b => b.Id == representativeAdd.BranchId);
            if (!branchExists)
            {
                return BadRequest("Invalid BranchId. The specified branch does not exist.");
            }
            var representative = new Representative
            {
                Name = representativeAdd.Name,
                Address = representativeAdd.Address,
                PhoneNumber = representativeAdd.Phone,
                BranchId = representativeAdd.BranchId,
                Email = representativeAdd.Email,
                UserName = representativeAdd.Email,
                Amount = representativeAdd.Amount,
                Type = (core.Models.AmountType?)representativeAdd.Type,
                RepresentativeGovernorate = representativeAdd.governorates.Select(g => new RepresentativeGovernorate
                {
                    GovernorateId = g.Id,
                   
                }).ToList()

            };

            var result = await _userManager.CreateAsync(representative, representativeAdd.Password);

            var governorates = representativeAdd.governorates.Select(g => new RepresentativeGovernorate
            {
                RepresentativeId = representative.Id,
                GovernorateId = g.Id
            }).ToList();

            _context.RepresentativeGovernorates.AddRange(governorates);
            await _context.SaveChangesAsync();

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(representative, "Representative");

            return Ok(new
            {
                Message = "Representative registered successfully",
            });





        }
    }
}