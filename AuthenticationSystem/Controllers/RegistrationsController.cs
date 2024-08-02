using AuthenticationSystem.Models;
using AuthenticationSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthenticationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("adduser")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] Registration user)
        {
            if (user != null)
            {
                await _registrationService.AddUser(user);
                return Ok("!!!User added successfully!!!");
            }
            return BadRequest("!!!User added fails!!!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var allUser = await _registrationService.GetAllUser();
            return Ok(allUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var specificUser = await _registrationService.GetUser(id);
            if (specificUser != null)
            {
                return Ok(specificUser);
            }
            return NotFound($"!!!User with id {id} not found!!!");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] Registration user)
        {
            var existedUser = await _registrationService.UpdateUser(id, user);
            if (existedUser)
            {
                return Ok($"!!!User with id {id} updated successfully!!!");
            }
            return NotFound($"!!!User with id {id} not found!!!");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var existedUser = await _registrationService.DeleteUser(id);
            if (existedUser)
            {
                return Ok($"!!!User with id {id} deleted successfully!!!");
            }
            return NotFound($"!!!User with id {id} not found!!!");
        }
    }
}
