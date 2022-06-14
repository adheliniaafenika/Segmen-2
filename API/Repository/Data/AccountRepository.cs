using API.Context;
using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        // mengecek apakah email sudah ada di db (tdk boleh duplikat)
        public bool IsEmailExist(string email)
        {
            int result = myContext.Employees.Where(e => e.Email == email).Count();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // melihat kecocokan password di vm dgn db
        public bool ValidasiPassword(LoginVM loginVM)
        {
            // cek password di db
            var password = (from e in myContext.Employees 
                            join a in myContext.Accounts
                            on e.NIK equals a.NIK
                            where e.Email == loginVM.Email
                            select a.Password).FirstOrDefault();

            // lazy loading -> aplikasinya jd lebih berat
            // cek lazy loading, pas ngisi
            var employee = (from e in myContext.Employees
                            where e.Email == loginVM.Email
                            select e).FirstOrDefault();

            var objek = employee.Account.OTP;

            // cek kecocokan password dan hash passwordnya
            var cekPassword = ValidatePassword(loginVM.Password, password);

            if (cekPassword == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Hashing Password
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        // cek kecocokan password dan hash passwordnya
        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }

        public string getOTP(string email)
        {
            // membuat nilai random antara 100.000 - 999.999 (6 digit)
            Random random = new Random();
            string randomNumber = (random.Next(100000, 999999)).ToString();

            // mengambil data account yg email db = email vm
            var Account3 = (from e in myContext.Employees
                            join a in myContext.Accounts
                            on e.NIK equals a.NIK
                            where e.Email == email
                            select a).FirstOrDefault();

            Account3.OTP = randomNumber;
            Account3.ExpiredTime = DateTime.Now.AddMinutes(5);

            // update, save di db
            myContext.Entry(Account3).State = EntityState.Modified;
            var result = myContext.SaveChanges();

            return randomNumber;
        }

        // mengirim OTP ke email tujuan
        public void sendEmail(string email)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("51282b37bf8a0a", "418c49cd93b42f"),
                EnableSsl = true
            };
            client.Send("nia@gmail.com", email, "OTP for your Account", "Your OTP is:" + getOTP(email));
        }

        // cek apakah OTP aktif
        public bool cekIsActive(string email)
        {
            // cek apakah waktu blm melewati 5 menit
            var cekExpiredTime = (from e in myContext.Employees
                                  join a in myContext.Accounts
                                  on e.NIK equals a.NIK
                                  where e.Email == email
                                  where a.ExpiredTime > DateTime.Now
                                  select a.ExpiredTime).FirstOrDefault();

            if (cekExpiredTime != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // apakah data otp yg diinputkan cocok dgn di db
        public bool ValidateOTP(string otp)
        {
            var cekOTP = (from a in myContext.Accounts
                          where a.OTP == otp
                          select a.OTP).FirstOrDefault();

            if (cekOTP != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // input password baru ke db
        public void inputPassword(ChangePasswordVM changePasswordVM)
        {
            // ambil data account di db yg emailnya cocok dgn vm
            var Account1 = (from e in myContext.Employees
                            join a in myContext.Accounts
                            on e.NIK equals a.NIK
                            where e.Email == changePasswordVM.Email
                            select a).FirstOrDefault();

            // update password baru (yg sdh di hash) dr vm ke db
            Account1.Password = HashPassword(changePasswordVM.Password_Baru);

            // update, save ke db
            myContext.Entry(Account1).State = EntityState.Modified;
            var result = myContext.SaveChanges();
        }

        // get NIK
        public string GetNIK(string email)
        { 
            var nik = (from e in myContext.Employees
                       where e.Email == email
                       select e.NIK).SingleOrDefault();
            return nik;
        }

        // get role id
        public List<string> GetRoleName(string nik)
        {
            var roleName = (from r in myContext.Roles
                           join ar in myContext.AccountRoles
                           on r.ID equals ar.Role_Id
                           where ar.Account_NIK == nik
                           select r.Nama).ToList();
            return roleName;
        }

        [Authorize(Roles = "Directur")]
        public bool SignManager(SignManagerVM signManagerVM)
        {
            // mencari data employee yg akan menjadi manager (bedasarkan nik)

            // apakah role di nik tsb sudah manager atau blm
            AccountRole AccountRole1 = myContext.AccountRoles.Where(ar => ar.Account_NIK == signManagerVM.NIK && ar.Role_Id == 2).SingleOrDefault();
            
            if(AccountRole1 != null) // jika role di nik tsb sudah manager
            {
                return true;
            }

            AccountRole AccountRole2 = new AccountRole // jika role di nik tsb blm jd manager
            {
                Account_NIK = signManagerVM.NIK,
                Role_Id = 2
            };

            return true;
        }
    }
}



