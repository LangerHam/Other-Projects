using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ecomm.Models
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}