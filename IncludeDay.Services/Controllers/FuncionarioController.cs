using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using IncludeDay.Data;
using IncludeDay.Data.Entities;
using LinqKit;

namespace IncludeDay.Services.Controllers
{
    public class FuncionarioController : ApiController
    {
        private readonly IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Funcionario
        public List<Funcionario> GetFuncionario([FromUri]Funcionario filter)
        {
            var predicate = PredicateBuilder.True<Funcionario>();

            if (filter != null && !string.IsNullOrEmpty(filter.Nome))
            {
                predicate = predicate.And(p => p.Nome.Contains(filter.Nome));
            }

            if (filter != null && !string.IsNullOrEmpty(filter.Cargo))
            {
                predicate = predicate.And(p => p.Cargo.Contains(filter.Cargo));
            }

            var list = _db.Funcionarios.AsExpandable().Where(predicate);
            return list.ToList();
        }

        // GET: api/Funcionario/5
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult GetFuncionario(int id)
        {
            Funcionario Funcionario = _db.Funcionarios.Find(id);
            if (Funcionario == null)
            {
                return NotFound();
            }

            return Ok(Funcionario);
        }

        // PUT: api/Funcionario/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutFuncionario(int id, Funcionario Funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Funcionario.Id)
            {
                return BadRequest();
            }

            _db.Entry(Funcionario).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // POST: api/Funcionario
        [ResponseType(typeof(Funcionario))]
        [HttpPost]
        public IHttpActionResult PostFuncionario(Funcionario Funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Funcionarios.Add(Funcionario);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Funcionario.Id }, Funcionario);
        }

        // DELETE: api/Funcionario/5
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult DeleteFuncionario(int id)
        {
            Funcionario Funcionario = _db.Funcionarios.Find(id);
            if (Funcionario == null)
            {
                return NotFound();
            }

            _db.Funcionarios.Remove(Funcionario);
            _db.SaveChanges();

            return Ok(Funcionario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionarioExists(int id)
        {
            return _db.Funcionarios.Count(e => e.Id == id) > 0;
        }
    }
}