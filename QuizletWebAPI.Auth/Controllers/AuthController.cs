using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

            // TODO: get JWT
            throw new NotImplementedException();

        }

        private Account? AuthenticateUser(string email, string password)
        {
            return _context.Accounts.SingleOrDefault(a => a.Email == email && a.Password == password);
        }

    }
}
