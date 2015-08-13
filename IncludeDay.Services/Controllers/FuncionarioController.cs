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
using IncludeDay.Services.Models;
using System.Threading.Tasks;

namespace IncludeDay.Services.Controllers
{
    public class FuncionarioController : ApiController
    {
        private readonly IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Funcionario
        [ResponseType(typeof(List<FuncionarioDTO>))]
        public List<FuncionarioDTO> GetFuncionario([FromUri]Funcionario filter)
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

            if (filter != null && !string.IsNullOrEmpty(filter.Email))
            {
                predicate = predicate.And(p => p.Email.Contains(filter.Email));
            }

            var list = from func in _db.Funcionarios
                           .Include(b => b.Departamento)
                           .Include(b => b.Departamento.Predio)
                           .AsExpandable()
                           .Where(predicate)
                       select new FuncionarioDTO
                       {
                           Id = func.Id,
                           Nome = func.Nome,
                           Cargo = func.Cargo,
                           Email = func.Email,
                           Departamento = func.Departamento == null ? null : new DepartamentoDTO
                           {
                               Id = func.Departamento.Id,
                               Nome = func.Departamento.Nome,
                               Descricao = func.Departamento.Descricao,
                               Predio = func.Departamento.Predio == null ? null : new PredioDTO
                               {
                                   Id = func.Departamento.Predio.Id,
                                   Nome = func.Departamento.Predio.Nome,
                                   Descricao = func.Departamento.Descricao
                               }
                           }
                       };

            //Proposital para causar um lentidão no retorno do serviço para os testers
            System.Threading.Thread.Sleep(10000);

            return list.ToList();
        }

        // GET: api/Funcionario/5
        [ResponseType(typeof(FuncionarioDTO))]
        public async Task<IHttpActionResult> GetFuncionario(int id)
        {
            var funcionario = await _db.Funcionarios
                .Include(b => b.Departamento)
                .Include(x => x.Departamento.Predio)
                .Select(b =>
                new FuncionarioDTO()
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    Cargo = b.Cargo,
                    Email = b.Email,
                    Departamento = b.Departamento == null ? null : new DepartamentoDTO
                    {
                        Id = b.Departamento.Id,
                        Nome = b.Departamento.Nome,
                        Descricao = b.Departamento.Descricao,
                        Predio = b.Departamento.Predio == null ? null : new PredioDTO
                        {
                            Id = b.Departamento.Predio.Id,
                            Nome = b.Departamento.Predio.Nome,
                            Descricao = b.Departamento.Predio.Descricao
                        }
                    }
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionario/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutFuncionario(int id, Funcionario Funcionario)
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
                await _db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostFuncionario(Funcionario Funcionario)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if(FuncionarioExists(Funcionario.Id))
            {
                var departamento = _db.Departamentos.Find(Funcionario.Departamento.Id);

                if(departamento != null)
                {
                    Funcionario.Departamento = departamento;
                }

                _db.Funcionarios.Attach(Funcionario);
                _db.Entry(Funcionario).State = EntityState.Modified;
            }
            else
            {
                _db.Funcionarios.Add(Funcionario);
            }
            
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Funcionario.Id }, Funcionario);
        }

        // DELETE: api/Funcionario/5
        [ResponseType(typeof(Funcionario))]
        public async Task<IHttpActionResult> DeleteFuncionario(int id)
        {
            Funcionario Funcionario = _db.Funcionarios.Find(id);
            if (Funcionario == null)
            {
                return NotFound();
            }

            _db.Funcionarios.Remove(Funcionario);
            await _db.SaveChangesAsync();

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