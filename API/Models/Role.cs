using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Role
    {
        [Key]
        [Required]
        public int ID { get; set; }

        public string Nama { get; set; }

        public ICollection<AccountRole> AccountRole { get; set; }
    }
}
