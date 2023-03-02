using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

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

        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
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
        
        [HttpGet("/users/{UserName}/{Password}")]        
        public ActionResult<User> GetByLogin(string UserName, string Password)
        {            
            Password = sha256_hash(Password);
            var user = _context.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);
            if (user is null)
                return NotFound();
            return Ok(user);

        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            
            user.Password = sha256_hash(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id= user.UserId},user);
        }
    }
}
