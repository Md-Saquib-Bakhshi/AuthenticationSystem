using AuthenticationSystem.Models;
using AuthenticationSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser(Registration user)
        {
            if (user != null)
            {
                await _registrationService.AddUser(user);
                return Ok("!!!User added successfully!!!");
            }
            return NotFound("!!!User added fails!!!");
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
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,[FromBody] Registration user)
        {
            var existedUser = await _registrationService.UpdateUser(id, user);
            if(existedUser)
            {
                return Ok($"!!!User with id {id} updated successfully!!!");
            }
            return NotFound($"!!!User with id {id} not found!!!");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var existedUser = await _registrationService.DeleteUser(id);
            if(existedUser)
            {
                return Ok($"!!!User with id {id} deleted successfully!!!");
            }
            return NotFound($"!!!User with id {id} not found!!!");
        }
    }
}
