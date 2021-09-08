using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;


        public PersonsController(PersonRepository repository) : base(repository)
        {
            this.repository = repository;
        }

       // [Authorize]
        [EnableCors("AllowOrigin")]
        [HttpGet("GetPerson")]
        public ActionResult GetPerson()
        {
            var getPerson = repository.GetPersonVMs();
            if (getPerson == null)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data masih kosong"
                });
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                data = getPerson,
                message = "Data berhasil Di tampilkan"
            });
        }

        [HttpGet("GetPerson/{NIK}")]
        public ActionResult GetPerson(string NIK)
        {
            var getPerson = repository.GetPersonVMs(NIK);
            if (getPerson != null)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = getPerson,
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

        [HttpPost("InsertVM")]
        public ActionResult InsertVM(InsertPersonVM get)
        {
           try
            {
               if (repository.Checking(get.NIK, get.Email, get.PhoneNumber) == "successful")
                {
                    repository.InsertPerson(get);
                    return Ok(new
                    {
                       status = HttpStatusCode.OK,
                       message = "Data berhasil Di Tambahkan"
                    });
                }
               else
               {
                   return BadRequest(new
                   {
                      status = HttpStatusCode.BadRequest,
                      message = repository.Checking(get.NIK, get.Email, get.PhoneNumber)
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
    }
}
