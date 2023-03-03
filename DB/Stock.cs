using Microsoft.EntityFrameworkCore;
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
        public string StockId { get; set; }

        [Required]        
        public string NameProduct { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]       
        public int Quantity { get; set; }
        [Required]        
        public decimal Price { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }        
        
    }
}
