using Microsoft.AspNetCore.Mvc;
using QuizletWebAPI.Auth.Data;

namespace QuizletWebAPI.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        public AuthController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            throw new NotImplementedException();
        }

    }
}
