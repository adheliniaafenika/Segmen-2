using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountRole
    {
        //[ForeignKey("Account")]
        public string Account_NIK { get; set; }

        //[ForeignKey("Role")]
        public int Role_Id { get; set; }

        public Role Role { get; set; }

        public Account Account { get; set; }
    }
}
