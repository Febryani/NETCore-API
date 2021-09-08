using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Repository;
using NETCore.Repository.Data;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>

    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {

            return Ok(new
            {
                status = 200,
                data = repository.Get(),
                message = "Data berhasil Di tampilkan"
            });
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            if (repository.Get(key) != null)
            {

                //return Ok(personRepository.Get(NIK));
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Get(key),
                    message = "Data berhasil Di tampilkan"
                });
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data dengan ID/NIK Tersebut Tidak Ditemukan"
                //return Ok(personRepository.Get(NIK));
            });

        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            if (repository.Insert(entity) != 0)
            {
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
                    message = "Gagal menambahkan data, periksa kembali"
                });
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                if (repository.Update(entity) != 0)
                {
                    //return Ok("Data Berhasil di Update");
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data berhasil Di Update"
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data dengan ID/NIK tersebut Tidak Ditemukan"

            });
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            if (repository.Get(key) != null)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Get(key),
                    deletedata = repository.Delete(key),
                    message = "Data berhasil Di Hapus"
                });
            }
            else
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data dengan ID/NIK tersebut Tidak Ditemukan"
                });
            }

        }

    }
}
