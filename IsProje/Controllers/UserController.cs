using IsProje.Data;
using IsProje.Models;
using IsProje.Models.ViewModels;
using IsProje.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using IsProje.Services;
using IsProje.Services.Interface;

namespace IsProje.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;



        public UserController(IUserService userService, JwtService jwtService)
        {
            
            _userService = userService;
            _jwtService = jwtService;

        }

        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            User user = await _userService.FindByEmailCheckPasswordAsync(model.UserName, model.UserPassword);
            if (user == null)
            {

                return BadRequest(new
                {
                    message = "User Name or password is incorrect"
                });
            }

            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = _jwtService.GenerateJwtToken(user)

            });
        }

    }
}
