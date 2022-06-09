﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class GetRegisteredDataVM
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public int Salary { get; set; }

        public string Email { get; set; }

        public string Degree { get; set; }

        public string GPA { get; set; }

        public string UniversityName { get; set; }
    }
}