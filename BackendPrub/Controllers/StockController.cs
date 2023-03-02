using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BackendPrub.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock is null)
                return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        public IActionResult Create(Stock stock)
        {            
            stock.StockId = Guid.NewGuid().ToString();
            _context.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.StockId }, stock);
        }

    }
}
