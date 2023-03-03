using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendPrub.Controllers
{    
    [Route("api/stocks/")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private DevTestDBContext _context;
        private IConfiguration config;

        public UserController(DevTestDBContext context,IConfiguration config)
        {
            _context = context;
            this.config = config;
        }              
        
        [HttpGet("{UserName}/{Password}")]        
        public ActionResult<User> GetByLogin(string UserName, string Password)
        {            
            Password = sha256_hash(Password);
            var user = _context.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);
            if (user is null)
                return NotFound();
            string jwtToken = GenerateToken(user);
            return Ok(new { token = jwtToken});

        }

        [HttpPost]
        public IActionResult Create(User user)
        {            
            user.Password = sha256_hash(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return Ok();
        }

        private static String sha256_hash(string value)
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

        private string GenerateToken(User user) 
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(1),
                                    signingCredentials:creds
                                    );
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
