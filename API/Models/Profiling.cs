using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Profiling
    {
        [Key]
        [ForeignKey("Account")]
        [Required]
        public string NIK { get; set; }

        [Required][ForeignKey("Education")]
        public int Education_Id { get; set; }

        public Education Education { get; set; }

        public Account Account { get; set; }
    }
}
