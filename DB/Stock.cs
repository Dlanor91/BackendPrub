using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Stock
    {
        [Key]        
        public int StockTicker { get; set; }
        public int Quantity { get; set; }
        public int LoginToken { get; set; }
    }
}
