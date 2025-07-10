using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class PcBuildComponent
    {
        [Key]
        public int BuildComponentID { get; set; }

        [Required]
        public int BuildID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [ForeignKey("BuildID")]
        public virtual PcBuild PcBuild { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}