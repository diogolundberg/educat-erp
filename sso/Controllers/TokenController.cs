using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SSO.Data.Entity;
using SSO.Models;

namespace SSO
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<User> _userRepository;


        public TokenController(IConfiguration configuration, DatabaseContext context)
        {
            _configuration = configuration;
            _context = context;
            _userRepository = new BaseRepository<User>(context);
        }

        [AllowAnonymous]
        [HttpPost("", Name = "SSO/TOKEN")]
        public IActionResult RequestToken([FromBody]TokenRequest request)
        {
            if (_userRepository.List().Any(x => x.Email.ToLower() == request.Username && x.Password == request.Password))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, request.Username) };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SECURITY_KEY"]));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken(
                        issuer: _configuration["SSO_HOST"],
                        audience: _configuration["SSO_HOST"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }
    }
}
