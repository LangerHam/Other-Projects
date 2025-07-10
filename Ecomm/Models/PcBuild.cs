using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class PcBuild
    {
        [Key]
        public int BuildID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(150)]
        public string BuildName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual ICollection<PcBuildComponent> PcBuildComponents { get; set; }
    }
}