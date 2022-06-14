using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class University
    {
        public University()
        {
            //Educations -> nama tabel di db
            //Education -> nama tabel di model
            Educations = new HashSet<Education>();
        }

        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        //Education -> nama tabel di model
        //Educations -> nama tabel di db
        //virtual -> supaya terhubung dgn tabel lain yg berelasi (utk pakai lazy loading)
        public virtual ICollection<Education> Educations { get; set; }
    }
}
