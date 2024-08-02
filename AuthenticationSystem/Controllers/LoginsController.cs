using AuthenticationSystem.Repository;
using AuthenticationSystem.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly ILoginService _loginService;

        public LoginsController(ILoginService loginService, IJwtTokenManager jwtTokenManager)
        {
            _loginService = loginService;
            _jwtTokenManager = jwtTokenManager;
        }

        [AllowAnonymous]
        [HttpGet("login/{email}/{password}")]
        public async Task<IActionResult> LoginUser([FromRoute] string email, [FromRoute] string password)
        {
            var isValidatedUser = await _loginService.IsExistingUser(email, password);
            if (isValidatedUser)
            {
                var token = _jwtTokenManager.Authenticate(email); 
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid credentials.");
        }
    }
}
