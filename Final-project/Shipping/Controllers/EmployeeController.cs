using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs.DTO.Employee;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShippingContext _context;

        public EmployeeController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ShippingContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.branch)
                .Include(e => e.Group)
                .ToListAsync();

            var employeedtos = employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Address = e.Address,
                BranchId = e.BranchId,
                BranchName = e.branch?.Name,
                GroupId = e.GroupId,
                GroupName = e.Group?.Name,
                IsDeleted = e.IsDeleted
            }).ToList();

            return employeedtos;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(string id)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id && !e.IsDeleted)
                .Include(e => e.branch)
                .Include(e => e.Group)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            var employeedtos = new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                BranchId = employee.BranchId,
                BranchName = employee.branch?.Name,
                GroupId = employee.GroupId,
                GroupName = employee.Group?.Name
            };

            return employeedtos;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(ADDEmployeeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify branch exists
            var branch = await _context.Branches.FindAsync(model.BranchId);
            if (branch == null)
            {
                return BadRequest("Invalid branch ID");
            }

            // Verify group exists
            var group = await _context.Groups.FindAsync(model.GroupId);
            if (group == null)
            {
                return BadRequest("Invalid group ID");
            }

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return Conflict("Email already in use");
            }

            // Create new employee
            var employee = new Employee
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                BranchId = model.BranchId,
                GroupId = model.GroupId,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(employee, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Add to Employee role
            await _userManager.AddToRoleAsync(employee, "Employee");

            // Refresh employee with navigation properties for return
            var ADDEmployee = await _context.Employees
                .Where(e => e.Id == employee.Id)
                .Include(e => e.branch)
                .Include(e => e.Group)
                .FirstOrDefaultAsync();

            var employeeDto = new EmployeeDTO
            {
                Id = ADDEmployee.Id,
                Name = ADDEmployee.Name,
                Email = ADDEmployee.Email,
                PhoneNumber = ADDEmployee.PhoneNumber,
                Address = ADDEmployee.Address,
                BranchId = ADDEmployee.BranchId,
                BranchName = ADDEmployee.branch?.Name,
                GroupId = ADDEmployee.GroupId,
                GroupName = ADDEmployee.Group?.Name
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employeeDto);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, UpdateEmployeeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null || employee.IsDeleted)
            {
                return NotFound();
            }

            // Verify branch exists
            if (model.BranchId.HasValue)
            {
                var branch = await _context.Branches.FindAsync(model.BranchId.Value);
                if (branch == null)
                {
                    return BadRequest("Invalid branch ID");
                }
                employee.BranchId = model.BranchId.Value;
            }

            // Verify group exists
            if (model.GroupId.HasValue)
            {
                var group = await _context.Groups.FindAsync(model.GroupId.Value);
                if (group == null)
                {
                    return BadRequest("Invalid group ID");
                }
                employee.GroupId = model.GroupId.Value;
            }

            // Update employee fields
            if (!string.IsNullOrEmpty(model.Name))
                employee.Name = model.Name;

            if (!string.IsNullOrEmpty(model.PhoneNumber))
                employee.PhoneNumber = model.PhoneNumber;

            if (!string.IsNullOrEmpty(model.Address))
                employee.Address = model.Address;

            // Update email if provided and different
            if (!string.IsNullOrEmpty(model.Email) && model.Email != employee.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null && existingUser.Id != id)
                {
                    return Conflict("Email already in use by another user");
                }

                employee.Email = model.Email;
                employee.UserName = model.Email; // Username is typically set to email
                employee.NormalizedEmail = model.Email.ToUpper();
                employee.NormalizedUserName = model.Email.ToUpper();
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(employee);
                var passwordResult = await _userManager.ResetPasswordAsync(employee, token, model.Password);
                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null || employee.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            employee.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id && !e.IsDeleted);
        }
    
    }
}
