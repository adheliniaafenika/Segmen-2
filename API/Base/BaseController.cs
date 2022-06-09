using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
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
        public ActionResult<Entity> Get()
        {
            var hasil = repository.Get();
            if(hasil != null)
            {
                return Ok(hasil);
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "List Data Tidak DiTemukan" });
            }
        }

        [HttpPost]
        public ActionResult<Entity> Insert(Entity entity)
        {
            var hasil = repository.Insert(entity);
 
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
        public ActionResult<Entity> Update(Entity entity)
        {
            var hasil = repository.Update(entity);
            if (hasil != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Update Data Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Update Data Gagal" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            var hasil = repository.Delete(key);
            if (hasil != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Delete Data Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Delete Data Gagal" });
            }
        }

        [HttpGet("{key}")]
        public ActionResult<Entity> Get(Key key)
        {
            var hasil = repository.Get(key);
            if (hasil != null)
            {
                return Ok(hasil);
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Get By ID Gagal" });
            }
        }


    }
}
