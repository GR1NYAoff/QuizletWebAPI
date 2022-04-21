using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizletWebAPI.Auth.Data;
using QuizletWebAPI.Auth.Models;
using QuizletWebAPI.Common;

namespace QuizletWebAPI.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IOptions<AuthOptions> _options;

        public AuthController(DataContext dataContext, IOptions<AuthOptions> options)
        {
            _context = dataContext;
            _options = options;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            if (user == null)
                return Unauthorized();

            var token = GenerateJWT(user);

            return Ok(token);

        }

        private Account? AuthenticateUser(string email, string password)
        {
            return _context.Accounts.SingleOrDefault(a => a.Email == email && a.Password == password);
        }

        private string GenerateJWT(Account user)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Login),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
