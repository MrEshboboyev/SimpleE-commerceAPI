using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        // inject Application User
        private readonly IApplicationUserService _userService;

        public AdministrationController(IApplicationUserService userService)
        {
            _userService = userService;
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

            var result = _userService.CreateUser(model);
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
    }
}
