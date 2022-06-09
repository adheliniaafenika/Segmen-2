using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public int Salary { get; set; }

        public string Email { get; set; }

        public bool isDeleted { get; set; }

        public string Password { get; set; }

        public string Degree { get; set; }

        public string Gender { get; set; }

        public int University_Id { get; set; }

        public string GPA { get; set; }

    }
}
