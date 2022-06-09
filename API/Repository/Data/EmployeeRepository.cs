using API.Context;
using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string> 
    {
        private readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        // insert data register
        public int InsertRegister(RegisterVM registerVM)
        {
            // membuat objek baru utk employee, education, account
            Employee Employee1 = new Employee();
            Education Education1 = new Education();
            Account Account1 = new Account();

            // input data employee1 dari vm ke db
            Employee1.FirstName = registerVM.FirstName;
            Employee1.LastName = registerVM.LastName;
            Employee1.Phone = registerVM.Phone;
            Employee1.BirthDate = registerVM.BirthDate;
            Employee1.Salary = registerVM.Salary;
            Employee1.Email = registerVM.Email;
            Employee1.Gender = (Gender)Enum.Parse(typeof(Gender), registerVM.Gender);

            // input data account1 dari vm ke db
            Account1.Password = HashPassword(registerVM.Password);

            // objek university1 diisi dgn data university yang id = university.id
            University University1 = myContext.Universities.Find(registerVM.University_Id);

            // input data education1 dari vm ke db
            Education1.Degree = (Degree)Enum.Parse(typeof(Degree), registerVM.Degree);
            Education1.GPA = registerVM.GPA;
            Education1.University = University1;

            // input data profiling1 dari vm ke db
            Profiling Profiling1 = new Profiling();
            Profiling1.Education = Education1;

            // input data accountRole dari vm ke db
            AccountRole AccountRole1 = new AccountRole();
            AccountRole1.Role_Id = 3;

            // input data account1 dan employee1 ke db
            Account1.Profiling = Profiling1;
            Employee1.Account = Account1;

            // input nik -> format : tgl-bln-tahun(saat mendaftar)-0001(auto increment), misal 020620220001 (2 juni 2022 urutan ke 1)
            
            // nik <= data nik terakhir
            var nik = (from e in myContext.Employees
                       orderby e.NIK
                       select e.NIK).LastOrDefault();

            // nik2 <= 4 digit terakhir (urutan)
            var nik2 = "0000";

            if (!String.IsNullOrWhiteSpace(nik)) // jika nik tidak null atau " "
            {
                nik2 = nik.Substring(nik.Length - 4); // nik2 <= mengambil 4 digit terakhir
            }

            var nik3 = (Convert.ToInt32(nik2)) + 1; // nik3 <= 4 digit terakhir + 1

            // menentukan jumlah angka nol didepan nik
            string nik4;

            if (nik3 < 10)
            {
                nik4 = "000" + Convert.ToString(nik3);
            } 
            else if (nik3 < 100)
            {
                nik4 = "00" + Convert.ToString(nik3);
            }
            else if (nik3 < 1000)
            {
                nik4 = "0" + Convert.ToString(nik3);
            }
            else
            {
                nik4 = Convert.ToString(nik3);
            }

            // NIK <= tgl sekarang + urutannya (4 digit)
            string nik5 = DateTime.Now.ToString("MMddyyyy") + nik4;
            Employee1.NIK = nik5;
            AccountRole1.Account_NIK = Employee1.NIK;

            // tambahkan data ke db, save
            myContext.Add(Employee1);
            myContext.Add(AccountRole1);
            var result = myContext.SaveChanges();
            return result; // jumlah data yg di insert
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

        // mengecek apakah phone sudah ada di db (tdk boleh duplikat)
        public bool IsPhoneExist(string phone)
        {
            int result = myContext.Employees.Where(e => e.Phone == phone).Count();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        // hashing password
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12); // 12 iterasi
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        // get data register
        public List<GetRegisteredDataVM> GetRegister()
        {
            var Employee = (from e in myContext.Employees
                            select e).ToList();

            List<GetRegisteredDataVM> getRegisteredDataVMs = new List<GetRegisteredDataVM>();

            foreach (var emp in Employee)
            {
                var Education1 = (from p in myContext.Profilings
                                 join e in myContext.Educations
                                 on p.Education_Id equals e.ID
                                 select e).FirstOrDefault();

                var University1 = (from e in myContext.Educations
                                    join u in myContext.Universities
                                    on e.University_Id equals u.ID
                                    select u).FirstOrDefault();

                GetRegisteredDataVM GetRegisteredDataVM1 = new GetRegisteredDataVM();
                GetRegisteredDataVM1.FullName = emp.FirstName + " " + emp.LastName;
                GetRegisteredDataVM1.PhoneNumber = emp.Phone;
                GetRegisteredDataVM1.BirthDate = emp.BirthDate;
                GetRegisteredDataVM1.Degree = Enum.GetName(typeof(Degree), Education1.Degree);
                GetRegisteredDataVM1.Salary = emp.Salary;
                GetRegisteredDataVM1.Email = emp.Email;
                GetRegisteredDataVM1.Gender = Enum.GetName(typeof(Degree), emp.Gender);
                GetRegisteredDataVM1.GPA = Education1.GPA;
                GetRegisteredDataVM1.UniversityName = University1.Name;

                getRegisteredDataVMs.Add(GetRegisteredDataVM1);
            }

            return getRegisteredDataVMs;   
        }
    }
}
