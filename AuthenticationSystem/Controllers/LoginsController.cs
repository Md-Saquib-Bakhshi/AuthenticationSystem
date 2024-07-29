using AuthenticationSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        ILoginService _loginService;

        public LoginsController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("login/{email}/{password}")]
        public async Task<IActionResult> LoginUser([FromRoute] string email,[FromRoute] string password)
        {
            var isValidatedUser = await _loginService.IsExistingUser(email, password);
            if(isValidatedUser)
            {
                return Ok($"Welcome {email} to Home");
            }
            return NotFound("!!!Bad Credentials!!!");
        }
    }
}
