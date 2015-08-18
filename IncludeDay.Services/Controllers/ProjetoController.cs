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
    public class ProjetoController : ApiController
    {
        private IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Projeto
        [ResponseType(typeof(List<ProjetoDTO>))]
        public List<ProjetoDTO> GetProjeto([FromUri]Projeto filter)
        {
            var predicate = PredicateBuilder.True<Projeto>();

            if (filter != null && !string.IsNullOrEmpty(filter.Nome))
            {
                predicate = predicate.And(p => p.Nome.Contains(filter.Nome));
            }

            if (filter != null && filter.Predio.Id > 0)
            {
                predicate = predicate.And(p => p.Predio.Id == filter.Predio.Id);
            }

            var list = from proj in _db.Projetos
                           .Include(x => x.Predio)
                           .AsExpandable()
                           .Where(predicate)
                       select new ProjetoDTO
                       {
                           Id = proj.Id,
                           Nome = proj.Nome,
                           Descricao = proj.Descricao,
                           Predio = proj.Predio == null ? null : new PredioDTO
                           {
                               Id = proj.Predio.Id,
                               Nome = proj.Predio.Nome,
                               Descricao = proj.Predio.Descricao
                           }
                       };

            return list.ToList();
        }

        // GET: api/Projeto/5
        [ResponseType(typeof(ProjetoDTO))]
        public async Task<IHttpActionResult> GetProjeto(int id)
        {
            var projeto = await _db.Projetos
                .Include(b => b.Predio)
                .Select(b =>
                new ProjetoDTO()
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    Descricao = b.Descricao,
                    Predio = b.Predio == null ? null : new PredioDTO
                    {
                        Id = b.Predio.Id,
                        Descricao = b.Predio.Descricao,
                        Nome = b.Predio.Nome
                    }
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (projeto == null)
            {
                return NotFound();
            }

            return Ok(projeto);
        }

        // PUT: api/Projeto/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutProjeto(int id, Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projeto.Id)
            {
                return BadRequest();
            }

            _db.Entry(projeto).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoExiste(id))
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
        [ResponseType(typeof(Projeto))]
        [HttpPost]
        public async Task<IHttpActionResult> PostProjeto(Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Projetos.Add(projeto);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projeto.Id }, projeto);
        }

        // DELETE: api/Departamento/5
        [ResponseType(typeof(Projeto))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProjeto(int id)
        {
            Projeto department = _db.Projetos.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            _db.Projetos.Remove(department);
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

        private bool ProjetoExiste(int id)
        {
            return _db.Projetos.Count(e => e.Id == id) > 0;
        }
    }
}