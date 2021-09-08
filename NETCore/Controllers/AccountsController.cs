using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using NETCore.Context;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        public IConfiguration configuration;
       // private readonly MyContext myContext;
        private readonly AccountRepository repository;

        public AccountsController(IConfiguration config, AccountRepository repository) : base(repository)
        {
            this.configuration = config;
            this.repository = repository;
        }

        [HttpGet("GetAccount")]
        public ActionResult GetAccount()
        {
            var getAccount = repository.DataLogin();
            if (getAccount != null)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = getAccount,
                    message = "Data berhasil Di tampilkan"
                });
            }
            else
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data tidak ditemukan"
                });
            }
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                string NIK = repository.CheckLoginEmail(loginVM.Email);
                if (string.IsNullOrEmpty(NIK))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new
                    {
                        status = (int)HttpStatusCode.NotFound,
                        message = "Login gagal. Email yang Anda masukan tidak terdaftar"
                    });
                }
                else if ((repository.CheckLoginPassword(NIK, loginVM.Password)))
                {
                    string[] roles = repository.GetRoles(NIK);
                    var Claims = new List<Claim>();
                    Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    Claims.Add(new Claim("NIK", NIK));
                    Claims.Add(new Claim("Email", loginVM.Email));
                    foreach (string role in roles)
                    {
                        Claims.Add(new Claim("roles", role));
                    }
                    
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims : Claims, 
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "Login berhasil !!!",
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        tokenexpired = token.ValidTo
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Login gagal. Password yang Anda masukan salah"
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = e.Message
                });
            }
        } 

        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(LoginVM loginVM)
        {
            string NIK = repository.CheckLoginEmail(loginVM.Email);
            if (string.IsNullOrEmpty(NIK))
            {
                return StatusCode((int)HttpStatusCode.NotFound,new
                {
                    status = (int)HttpStatusCode.NotFound,
                    message = "Reset Password gagal. Email yang Anda masukan tidak terdaftar"
                });
            }
            else if (repository.CheckPhone(NIK, loginVM.Phone))
            {
                string newPass = repository.GeneratePass();
                string saltPass = BCrypt.Net.BCrypt.GenerateSalt(12);
                string hashPass = BCrypt.Net.BCrypt.HashPassword(newPass, saltPass);
                Account account = new Account();
                account.NIK = NIK;
                account.Password = hashPass;
                Update(account);
                repository.SendEmail(loginVM.Email, newPass);
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Reset password berhasil! Silahkan cek email Anda untuk login kembali"
                });
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = "Reset password gagal. Phonenumber yang Anda masukan tidak terdaftar"
                });
            } 
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePassVM changePassVM)
        {
            try
            {
                string NIK = repository.CheckLoginEmail(changePassVM.Email);
                if (string.IsNullOrEmpty(NIK))
                {
                    return StatusCode((int)HttpStatusCode.NotFound,
                        new
                        {
                            status = (int)HttpStatusCode.NotFound,
                            message = "Periksa kembali! Mungkin ada kesalahan dalam pengetikan email Anda"
                        });
                }
                else if ((repository.CheckLoginPassword(NIK, changePassVM.Password)))
                {
                    string saltPass = BCrypt.Net.BCrypt.GenerateSalt(12);
                    Account account = new Account();
                    account.NIK = NIK;
                    account.Password = BCrypt.Net.BCrypt.HashPassword(changePassVM.NewPassword, saltPass);
                    if (changePassVM.Password != changePassVM.NewPassword)
                    {
                        Update(account);
                        return Ok(new
                                {
                                status = HttpStatusCode.OK,
                                message = "Password telah berhasil diubah !!!"
                                });
                    }
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        message = "Password baru yang Anda masukan tidak boleh sama dengan password lama Anda"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        message = "Password lama yang Anda masukan salah"
                    });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = e.Message
                });
            }
        }
    }
}
