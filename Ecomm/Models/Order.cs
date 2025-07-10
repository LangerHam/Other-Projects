using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int? UserID { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(20)]
        public string CustomerPhone { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}