using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            return await Task.Run(async () =>
            {
                var result = request.Username == "admin" && request.Password == "password";

                if (!result)
                {
                    throw new BadPasswordException();
                }


                var res = await GenerateAuthenticationResultForUserAsync(request);

                return Ok(res);
            });
        }


        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(UserLoginRequest user)
        {
            return await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("very secret password");

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                // ReSharper disable once InconsistentNaming
                const int TOKEN_EXPIRE_DAY_COUNT = 10;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = "Project of tour guide for Dorak Holding",
                    Issuer = "Dorak Holding",
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(TOKEN_EXPIRE_DAY_COUNT),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);


                return new AuthenticationResult(tokenHandler.WriteToken(token));
            });
        }
    }
}