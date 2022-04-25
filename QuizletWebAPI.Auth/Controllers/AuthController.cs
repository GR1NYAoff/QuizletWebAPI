using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
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
        public async Task<IActionResult> LoginAsync([FromBody] Login request)
        {
            var user = await AuthenticateUser(request.Email, request.Password);

            if (user == null)
                return Unauthorized();

            var token = GenerateJWT(user);

            return Ok(new { access_token = token });

        }

        [Route("reg")]
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(Account account)
        {
            if (await AccountExists(account.Id, account.Email))
                return BadRequest();

            _ = await _context.Accounts.AddAsync(account);

            var access = account.Login == "admin" ?
                new Access(account.Id, GetFullAccess()) :
                new Access(account.Id, GetRandomAccess());

            _ = await _context.Accesses.AddAsync(access);

            _ = await _context.SaveChangesAsync();

            return Ok();

        }

        private Task<bool> AccountExists(Guid id, string email)
        {
            return _context.Accounts.AnyAsync(a => a.Id == id || a.Email == email);
        }

        private string GetRandomAccess()
        {
            var random = new Random();

            var countTests = _context.Tests.Count();
            var testsIds = _context.Tests.Select(t => t.Id).ToArray();

            var result = $"[{testsIds[random.Next(countTests)]}]";

            return result;
        }

        private string GetFullAccess()
        {
            var testsIds = _context.Tests.Select(t => t.Id).ToArray();

            return JsonSerializer.Serialize(testsIds);
        }

        private Task<Account?> AuthenticateUser(string email, string password)
        {
            return _context.Accounts.SingleOrDefaultAsync(a => a.Email == email && a.Password == password);
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
