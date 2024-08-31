using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        // inject Application User, Role Service
        private readonly IApplicationUserService _userService;
        private readonly IRoleService _roleService;

        public AdministrationController(IApplicationUserService userService, 
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        #region User
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("get-user-by-email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Ok(_userService.GetUserByEmail(email));
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            return Ok(_userService.GetUserById(userId));
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(model);
            if (result)
                return Ok("Create user : success!");
            else
                return BadRequest("Create user : failed!");
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (userId != model.UserId)
                return BadRequest("UserId and model.UserId must be equal!");

            var result = _userService.UpdateUser(model);
            if (result)
                return Ok("Update user : success!");
            else
                return BadRequest("Update user : failed!");
        }

        [HttpDelete("remove-user")]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var result = _userService.DeleteUser(userId);
            if (result)
            {
                return Ok("Successfully deleted!");
            }
            return BadRequest();
        }

        [HttpGet("get-user-roles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var result = await _userService.GetUserRolesAsync(email);
            if (result is not null)
                return Ok(result);
            else 
                return NotFound();
        }
        #endregion

        #region Role
        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(_roleService.GetAllRoles());
        }

        [HttpGet("get-role-by-name")]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            return Ok(_roleService.GetRoleByName(roleName));
        }

        [HttpGet("get-role-by-id")]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            return Ok(_roleService.GetRoleById(roleId));
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _roleService.CreateRole(model);
            if (result)
                return Ok("Create role : success!");
            else
                return BadRequest("Create role : failed!");
        }

        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (roleId != model.RoleId)
                return BadRequest("RoleId and model.RoleId must be equal!");

            var result = _roleService.UpdateRole(model);
            if (result)
                return Ok("Update role : success!");
            else
                return BadRequest("Update role : failed!");
        }

        [HttpDelete("remove-role")]
        public async Task<IActionResult> RemoveRole(string roleId)
        {
            var result = _roleService.DeleteRole(roleId);
            if (result)
            {
                return Ok("Role Successfully deleted!");
            }
            return BadRequest();
        }

        [HttpGet("get-role-users")]
        public async Task<IActionResult> GetRoleUsers(string roleName)
        {
            var result = await _roleService.GetRoleUsersAsync(roleName);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }
        #endregion
    }
}
