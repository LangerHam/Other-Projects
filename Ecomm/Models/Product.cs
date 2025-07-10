using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        [StringLength(50)]
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
        public string ImageURL { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }

        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PcBuildComponent> PcBuildComponents { get; set; }
    }
}