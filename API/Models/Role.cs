using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Role
    {
        public Role()
        {
            AccountRole = new HashSet<AccountRole>();
        }

        [Key]
        [Required]
        public int ID { get; set; }

        public string Nama { get; set; }

        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
