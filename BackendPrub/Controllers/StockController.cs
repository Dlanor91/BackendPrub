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
        
        [HttpPut]
        public IActionResult Create(Stock stock,string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            string id = jwtToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (id == null)
                return NotFound();
            stock.StockId = Guid.NewGuid().ToString();
            stock.UserId = Int32.Parse(id);
            _context.Add(stock);
            _context.SaveChanges();
            return Ok();
        }
                
        [HttpPost]
        public ActionResult<Stock> ResultByUser(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            string id = jwtToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (id == null)
                return NotFound();
            int idUser = Int32.Parse(id);            
            IEnumerable<Stock> result = _context.Stocks.Where(obj => obj.UserId == idUser);
            return Ok(result);
        }
    }
}
