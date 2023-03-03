using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BackendPrub.Controllers
{
    [Route("api/stocks/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private DevTestDBContext _context;
        
        public StockController(DevTestDBContext context)
        {
            _context = context;            
        }

        [HttpGet]
        public IEnumerable<Stock> Get() => _context.Stocks.ToList();

        [Authorize]
        [HttpPut]
        public IActionResult Create(Stock stock,string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            string id = jwtToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            stock.StockId = Guid.NewGuid().ToString();
            stock.UserId = Int32.Parse(id);
            _context.Add(stock);
            _context.SaveChanges();
            return Ok();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Hacer(Stock stock)
        {            
            _context.Add(stock);
            _context.SaveChanges();
            return Ok();
        }
    }
}
