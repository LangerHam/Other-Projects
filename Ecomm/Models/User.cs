using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PcBuild> PcBuilds { get; set; }
    }
}