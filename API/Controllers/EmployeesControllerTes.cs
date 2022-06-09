// harusnya dihapus
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesControllerTes : ControllerBase
    {
        private readonly EmployeeRepositoryTes employeeRepository;

        public EmployeesControllerTes(EmployeeRepositoryTes employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var hasil = employeeRepository.Get();
            if (hasil != null)
            {
                return Ok(hasil);
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "List Data Tidak DiTemukan" });
            }
        }

        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            var hasil = employeeRepository.Insert(employee);
            if (hasil != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Insert Data Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Insert Data Gagal" });
            }
        }

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            var hasil = employeeRepository.Update(employee);
            if (hasil != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Update Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Update Gagal" });
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var hasil = employeeRepository.Delete(NIK);
            if (hasil != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Delete Data Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Delete Data Gagal" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult GetByNIK(string NIK)
        {
            var hasil = employeeRepository.GetByNIK(NIK);
            if (hasil != null)
            {
                return Ok(hasil);
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Get By NIK Gagal" });
            }         
        }
    }
}