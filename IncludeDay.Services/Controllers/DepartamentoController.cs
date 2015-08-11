using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using IncludeDay.Data;
using IncludeDay.Data.Entities;
using LinqKit;
using System.Collections.Generic;
using IncludeDay.Services.Models;
using System.Threading.Tasks;

namespace IncludeDay.Services.Controllers
{
    public class DepartamentoController : ApiController
    {
        private IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Departamento
        [ResponseType(typeof(List<DepartamentoDTO>))]
        public List<DepartamentoDTO> GetDepartamento([FromUri]Funcionario filter)
        {
            var predicate = PredicateBuilder.True<Departamento>();

            if (filter != null && !string.IsNullOrEmpty(filter.Nome))
            {
                predicate = predicate.And(p => p.Nome.Contains(filter.Nome));
            }

            if (filter != null && !string.IsNullOrEmpty(filter.Cargo))
            {
                predicate = predicate.And(p => p.Predio.Nome.Contains(filter.Cargo));
            }

            var list = from dept in _db.Departamentos.Include(x => x.Predio).AsExpandable().Where(predicate)
                       select new DepartamentoDTO
                       {
                           Id = dept.Id,
                           Nome = dept.Nome,
                           Descricao = dept.Descricao,
                           Predio = new PredioDTO
                           {
                               Id = dept.Predio.Id,
                               Nome = dept.Predio.Nome,
                               Descricao = dept.Predio.Descricao
                           }
                       };

            return list.ToList();
        }

        // GET: api/Departamento/5
        [ResponseType(typeof(DepartamentoDTO))]
        public async Task<IHttpActionResult> GetDepartamento(int id)
        {
            var department = await _db.Departamentos.Include(b => b.Predio).Select(b =>
                new DepartamentoDTO()
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    Descricao = b.Descricao,
                    Predio = new PredioDTO
                    {
                        Id = b.Predio.Id,
                        Descricao = b.Predio.Descricao,
                        Nome = b.Predio.Nome
                    }
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departamento/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutDepartamento(int id, Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departamento.Id)
            {
                return BadRequest();
            }

            _db.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostDepartamento(Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Departamentos.Add(departamento);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = departamento.Id }, departamento);
        }

        // DELETE: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteDepartamento(int id)
        {
            Departamento department = _db.Departamentos.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            _db.Departamentos.Remove(department);
            await _db.SaveChangesAsync();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return _db.Departamentos.Count(e => e.Id == id) > 0;
        }
    }
}