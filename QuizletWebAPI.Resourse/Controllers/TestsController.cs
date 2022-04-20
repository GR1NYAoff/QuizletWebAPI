#nullable disable
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizletWebAPI.Resourse.Data;
using QuizletWebAPI.Resourse.Models;

namespace QuizletWebAPI.Resourse.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TestsDbContext _context;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public TestsController(TestsDbContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Test>>> GetAvailableTests()
        {
            var accessDict = await GetAccessDictionary();

            if (!accessDict.ContainsKey(UserId))
                return Ok(Enumerable.Empty<Test>());

            var accessTestIds = accessDict.Single(d => d.Key == UserId).Value;

            return await _context.Tests.Where(t => accessTestIds.Contains(t.Id)).ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            return test == null ? (ActionResult<Test>)NotFound() : (ActionResult<Test>)test;
        }
        private Task<Dictionary<Guid, int[]>> GetAccessDictionary()
        {
            return _context.Accesses
                .ToDictionaryAsync(t => t.UserId, t => JsonSerializer.Deserialize<int[]>(t.TestId));
        }
    }
}
