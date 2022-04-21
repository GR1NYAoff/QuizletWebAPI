using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizletWebAPI.Auth.Data;
using QuizletWebAPI.Auth.Models;

namespace QuizletWebAPI.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly DataContext _context;
        private string? UserId => User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        public TestsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetAvailableTests()
        {
            var checkUserId = Guid.TryParse(UserId, out var userId);
            var accessDict = await GetAccessDictionary();

            if (!checkUserId)
                return Unauthorized();
            else if (!accessDict.ContainsKey(userId))
                return Ok(Enumerable.Empty<Test>());

            var accessTestIds = accessDict.Single(i => i.Key == userId).Value;

            return await _context.Tests.Where(t => accessTestIds.Contains(t.Id)).ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var checkUserId = Guid.TryParse(UserId, out var userId);
            var accessDict = await GetAccessDictionary();

            if (!checkUserId)
                return Unauthorized();
            else if (!accessDict.ContainsKey(userId))
                return Ok(Enumerable.Empty<Test>());

            var accessTestIds = accessDict.Single(i => i.Key == userId).Value;

            if (!accessTestIds.Contains(id))
                return (ActionResult<Test>)NotFound();

            var test = await _context.Tests.FindAsync(id);

            return test == null ? (ActionResult<Test>)NotFound() : (ActionResult<Test>)test;
        }

        private Task<Dictionary<Guid, int[]>> GetAccessDictionary()
        {
            return _context.Accesses
                .ToDictionaryAsync(t => t.UserId, t => JsonSerializer.Deserialize<int[]>(t.TestId))!;
        }
    }
}
