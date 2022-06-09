using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }

        public string Password_Baru { get; set; }

        public string OTP { get; set; }
    }
}
