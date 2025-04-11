using DTOs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.services;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;
        private readonly IUnitOfWork _unitOfWork;

        public GroupController(GroupService groupService, IUnitOfWork unitOfWork)
        {
            _groupService = groupService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("permissions/{groupId}")]
        public async Task<IActionResult> GetGroupPermissions(int groupId)
        {
            var permissions = await _groupService.GetGroupPermissionsAsync(groupId);
            if (permissions == null || !permissions.Any())
                return NotFound("No permissions found for this group.");

            var permissionsDto = new GroupPermissionDto
            {
                Id = permissions.FirstOrDefault().Group.Id,    //ta3del
                Name = permissions.FirstOrDefault()?.Group?.Name,
                Permissions = permissions
                    .GroupBy(p => p.Permission?.Name)
                    .Select(g => new PermissionDto
                    {
                        PermissionName = g.Select(s => s.Permission.Name).FirstOrDefault(),
                        Actions = g.Select(p => p.Action).ToList()
                    })
                    .ToList()
            };

            return Ok(permissionsDto);
        }
        [HttpPost("AddGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingGroup = await _unitOfWork.Repository<Group>()
                   .GetByNameAsync(g => g.Name == groupDto.Name);
                if (existingGroup != null)
                    return BadRequest(new { message = "Group name already exists." });

                var group = new Group { Name = groupDto.Name };
                await _unitOfWork.Repository<Group>().AddAsync(group);
                await _unitOfWork.CompleteAsync();

                if (groupDto.Permissions != null && groupDto.Permissions.Any())
                {
                    var groupPermissions = groupDto.Permissions.Select(p => new GroupPermission
                    {
                        GroupId = group.Id,
                        PermissionId = p.PermissionId,
                        Action = p.Action
                    }).ToList();

                    await _groupService.AddGroupPermissionsAsync(groupPermissions);
                    await _unitOfWork.CompleteAsync();
                }

                return CreatedAtAction(nameof(CreateGroup), new { id = group.Id }, new { message = "Group created successfully with permissions." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
            }
        }


        [HttpGet("AllGroups")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _unitOfWork.Repository<Group>().GetAllAsync();
            if (!groups.Any())
                return NotFound("No groups found.");
            var groupDtos = groups.Select(g => new GroupPermissionDto
            {
                Id = g.Id,   //t3del
                Date = g.Date,  //ta3del
                Name = g.Name,
                Permissions = g.GroupPermissions
               .GroupBy(p => p.Permission.Name)
               .Select(pg => new PermissionDto
               {
                   PermissionName = pg.Key,
                   Actions = pg.Select(p => p.Action).ToList()
               }).ToList()
            }).ToList();
            return Ok(groupDtos);

        }



        [HttpPut("UpdateGroup/{groupId}")]
        public async Task<IActionResult> UpdateGroup(int groupId, [FromBody] UpdateGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = await _unitOfWork.Repository<Group>().GetByIdAsync(groupId);
            if (group == null)
                return NotFound(new { message = "Group not found." });

            try
            {
                if (!string.IsNullOrEmpty(groupDto.Name))
                    group.Name = groupDto.Name;

                if (groupDto.Permissions != null && groupDto.Permissions.Any())
                {
                    foreach (var permDto in groupDto.Permissions)
                    {
                        var existingPermission = group.GroupPermissions
                            .FirstOrDefault(p => p.PermissionId == permDto.PermissionId);

                        if (existingPermission != null)
                        {
                            existingPermission.Action = permDto.Action;
                        }
                        else
                        {
                            var newPermission = new GroupPermission
                            {
                                GroupId = group.Id,
                                PermissionId = permDto.PermissionId,
                                Action = permDto.Action
                            };
                            group.GroupPermissions.Add(newPermission);
                        }
                    }
                }

                await _unitOfWork.CompleteAsync();
                return Ok(new { message = "Group updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
            }
        }


        [HttpDelete("RemoveGroup/{groupId}")]
        public async Task<IActionResult> RemoveGroup(int groupId)
        {
            var group = await _unitOfWork.Repository<Group>().GetByIdAsync(groupId);
            if (group == null)
                return NotFound(new { message = "Group not found." });

            try
            {
                _groupService.RemoveGroupPermissionsAsync(group.Id);

                _unitOfWork.Repository<Group>().DeleteAsync(group);

                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "Group removed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
            }
        }


    }

}
