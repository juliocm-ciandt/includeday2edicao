using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using IncludeDay.Data;
using IncludeDay.Data.Entities;

namespace IncludeDay.Services.Controllers
{
    public class DepartamentoController : ApiController
    {
        private IncludeDayContext db = new IncludeDayContext();

        // GET: api/Departamento
        public IQueryable<Departamento> GetDepartamento()
        {
            return db.Departamentos.AsQueryable();
        }

        // GET: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult GetDepartamento(int id)
        {
            Departamento department = db.Departamentos.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departamento/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutDepartamento(int id, Departamento departmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departmento.Id)
            {
                return BadRequest();
            }

            db.Entry(departmento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departamento
        [ResponseType(typeof(Departamento))]
        [HttpPost]
        public IHttpActionResult PostDepartamento(Departamento departmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departamentos.Add(departmento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departmento.Id }, departmento);
        }

        // DELETE: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        [HttpDelete]
        public IHttpActionResult DeleteDepartamento(int id)
        {
            Departamento department = db.Departamentos.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departamentos.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departamentos.Count(e => e.Id == id) > 0;
        }
    }
}