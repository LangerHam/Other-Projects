using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class ProductSpecification
    {
        [Key]
        public int SpecificationID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        [StringLength(100)]
        public string SpecKey { get; set; }
        [Required]
        [StringLength(255)]
        public string SpecValue { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}