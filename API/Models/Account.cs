using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account
    {
        public Account()
        {
            AccountRole = new HashSet<AccountRole>();
        }

        [Key]
        [ForeignKey("Employee")]
        [Required]
        public string NIK { get; set; }

        [Required]
        public string Password { get; set; }

        public string OTP { get; set; }

        public bool isActive { get; set; } = true;

        public DateTime? ExpiredTime { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Profiling Profiling { get; set; }

        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}

