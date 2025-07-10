using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}