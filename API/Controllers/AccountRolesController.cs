using API.Base;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class AccountRolesController : ControllerBase
    {
        private readonly AccountRoleRepository accountRoleRepository;

        public AccountRolesController(AccountRoleRepository accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult SignManager(SignManagerVM signManagerVM)
        {
            if (accountRoleRepository.InsertDataManager(signManagerVM) == 400)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Karyawan sudah menjadi manager" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Insert Data Manager Berhasil" });
            }
        }
    }
}


