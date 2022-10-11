using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using JAP_Management.Core.Models;
using JAP_Management.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JAP_Management.Backoffice.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                var token = await _userService.LoginAndGenerateJWTAsync(loginModel);

                if (String.IsNullOrEmpty(token))
                    return BadRequest("User account not found!");

                return Ok(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("Something went wrong!");
            }
        }
    }
}
