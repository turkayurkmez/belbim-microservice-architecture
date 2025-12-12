using BelbimEShop.Identity.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BelbimEShop.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var user = userService.ValidateUserCredentials(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-a-secret-key-for-jwt-token-generation"));

                SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "belbim.server",
                    audience: "belbim.client",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                );


                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
            }
            return Unauthorized("Invalid credentials");
        }
    }

    public record LoginRequest(string Username, string Password);

    
}
