using DTOs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.repo.Implementation;
using Shipping.repo.ShippingCon;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly ShippingContext _context;
        private readonly ISpecialPrice _specialShippingPrice;
        private readonly UserManager<ApplicationUser> _userManager;


        public MerchantController(ShippingContext shippingContext ,ISpecialPrice specialShippingPrice, UserManager<ApplicationUser> userManager)
        {
            _context = shippingContext;
            _specialShippingPrice = specialShippingPrice;
            _userManager = userManager;

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMerchants()
        {
            var merchants = (await _userManager.GetUsersInRoleAsync("Merchant"))
                            .Cast<Merchant>()
                            .ToList();

            if (merchants == null || !merchants.Any())
            {
                return NotFound();
            }

            var getAllMerchantsDto = merchants.Select(m => new GetAllMerchantsDto
            {
                Id = m.Id,
                Name = m.Name,
                StoreName = m.StoreName,
                Phone = m.PhoneNumber,
                Email = m.Email,
                BranchName = m.branch?.Name,
                GovernateName = m.Governorate?.Name,
                IsDeleted = m.IsDeleted,
                ReturnerPercent = m.ReturnerPercent
            }).ToList();

            return Ok(getAllMerchantsDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterMerchant(MerchantAdd merchantAdd)
        {

            List<SpecialPrice> specialPrices = merchantAdd.SpecialPrices.Select(p => new SpecialPrice
            {
                Price = p.Price,
                CityId = p.CityId,
                GovernorateId = p.GovernorateId,
                MerchentId = null
            }).ToList();
            var merchant = new Merchant
            {
                Name = merchantAdd.MerchantName,
                UserName = merchantAdd.Email,
                StoreName = merchantAdd.StoreName,
                Address = merchantAdd.Address,
                PickUp = merchantAdd.PickUp,
                ReturnerPercent = merchantAdd.ReturnerPercent,
                Email = merchantAdd.Email,
                PhoneNumber = merchantAdd.PhoneNumber,
                BranchId = merchantAdd.BranchId,
                CityId = merchantAdd.CityId,
                GovernorateId = merchantAdd.GovernorateId,
                SpecialPrices = specialPrices
            };

            var result = await _userManager.CreateAsync(merchant, merchantAdd.Password); 

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            foreach (var specialPrice in specialPrices)
            {
                specialPrice.MerchentId = merchant.Id;
            }

            await _specialShippingPrice.AddRangeAsync(specialPrices);

            await _userManager.AddToRoleAsync(merchant, "Merchant");

           //  var token = await Helper.GenerateJwtToken(merchant, _userManager);

            return Ok(new
            {
                Message = "Merchant registered successfully", 
            });
        }



        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMerchant(string id)
        {
            var merchant = await _userManager.FindByIdAsync(id);
            if (merchant == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(merchant);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("MerchantWithSpecialPrices/{id}")]
        public async Task<IActionResult> GetMerchantWithSpecialPrices(string id)
        {
            var merchant = await _userManager.Users
                .OfType<Merchant>()  
                .FirstOrDefaultAsync(m => m.Id == id);
            if (merchant == null) return NotFound("Merchant not found");

            var specialPrices = await _specialShippingPrice.GetSpecialPricesByMerchantId(id) ?? new List<SpecialPrice>();

            var result = new MerchantWithSpecialPricesDto
            {
                StoreName = merchant.StoreName,
                MerchantName = merchant.Name,  
                SpecialPrices = specialPrices.Select(sp => new SpecialPriceDto
                {
                    Price = sp.Price
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateMerchant(string id, [FromBody] UpdateMerchantDTO merchantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "Merchant not found" });
            }

             if (!string.IsNullOrWhiteSpace(merchantDTO.StoreName))
            {
                user.Name = merchantDTO.StoreName;
            }
            if (!string.IsNullOrWhiteSpace(merchantDTO.Address))
            {
                user.Address = merchantDTO.Address;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Merchant updated successfully", UpdatedMerchant = user });
            }
            return BadRequest(result.Errors);
        }




    }
}

