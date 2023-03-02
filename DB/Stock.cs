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
        public string StockTicker { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]       
        public int Quantity { get; set; }
        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal Price { get; set; }
    }
}
