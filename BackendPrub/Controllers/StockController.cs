using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BackendPrub.Controllers
{
    [Route("api/stocks/")]
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
        public IActionResult Create(Stock stock)
        {            
            stock.StockId = Guid.NewGuid().ToString();
            _context.Add(stock);
            _context.SaveChanges();
            return Ok();
        }
        /*
        [Authorize]
        [HttpPost]
        public IActionResult Hacer(Stock stock)
        {            
            _context.Add(stock);
            _context.SaveChanges();
            return Ok();
        }*/
    }
}
