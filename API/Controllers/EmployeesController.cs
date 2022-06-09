using API.Base;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            // mengecek apakah email sudah ada di db (tdk boleh duplikat)
            var cekEmail = employeeRepository.IsEmailExist(registerVM.Email);
            if(cekEmail == true) 
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email telah digunakan" });
            }

            // mengecek apakah phone sudah ada di db (tdk boleh duplikat)
            var cekPhone = employeeRepository.IsPhoneExist(registerVM.Phone);
            if (cekPhone == true)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Nomor Telepon telah digunakan" });
            }

            // insert data register
            var hasil = employeeRepository.InsertRegister(registerVM);

            if (hasil != 0) // jika data yg di insert ada
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Insert Data Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Insert Data Gagal" });
            }
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpGet("GetAllRegistered")]
        public ActionResult<GetRegisteredDataVM> GetAllRegistered()
        {
            var hasil = employeeRepository.GetRegister();
            if (hasil != null)
            {
                return Ok(hasil);
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "List Data Tidak DiTemukan" });
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }
    }
}
