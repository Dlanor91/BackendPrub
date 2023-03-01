using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendPrub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private DevTestDBContext _context;

        public UserController(DevTestDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get() => _context.Users.ToList();

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user is null)  
                return NotFound();
            return Ok(user);

        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id= user.UserId},user);
        }
    }
}
