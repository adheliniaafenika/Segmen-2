using API.Base;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;

        public IConfiguration _configuration;

        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            // mengecek apakah email sudah terdaftar di db 
            var cekEmail = accountRepository.IsEmailExist(loginVM.Email);
            if (cekEmail == false)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar" });
            }

            // melihat kecocokan password di vm dgn db
            var cekPassword = accountRepository.ValidasiPassword(loginVM);

            //if (cekPassword == true)
            //{
            //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Login Berhasil" });
            //}
            //else
            //{
            //    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Password tidak sesuai" });
            //}

            if (cekPassword == false)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Password tidak sesuai" });
            }

            var NIK = accountRepository.GetNIK(loginVM.Email);
            var cekRole = accountRepository.GetRoleName(NIK);
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", loginVM.Email));
            foreach (string cr in cekRole)
            {
                claims.Add(new Claim("roles", cr));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
            var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
            if(idtoken != null)
            {
                return Ok(new { status = HttpStatusCode.OK, idtoken, message = "Berhasil login" });
            }
            else
            {
                return BadRequest();
            }        
        }
        

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (accountRepository.IsEmailExist(forgotPasswordVM.Email)) // jika email sudah terdaftar di db 
            {
                accountRepository.sendEmail(forgotPasswordVM.Email); // mengirim OTP ke email tujuan
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "OTP telah terkirim" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar" });
            }
            
        }

        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            // mengecek apakah email sudah terdaftar di db 
            var cekEmail = accountRepository.IsEmailExist(changePasswordVM.Email);
            if (cekEmail == false)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar" });
            }

            // apakah OTP masih aktif
            bool cekExpiredTime = accountRepository.cekIsActive(changePasswordVM.Email); 
            if (cekExpiredTime == false)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP sudah kadaluarsa" });
            }

            // apakah data otp yg diinputkan sesuai dgn di db
            bool cekOTP = accountRepository.ValidateOTP(changePasswordVM.OTP); 
            if (cekOTP == false)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP tidak sesuai" });
            }

            // input password baru ke db
            accountRepository.inputPassword(changePasswordVM);

            return StatusCode(200, new { status = HttpStatusCode.OK, message = "OTP sesuai" });
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }
    }
}