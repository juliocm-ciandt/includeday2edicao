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
using System.Linq.Expressions;
using System;

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
                           .Include(b => b.Projeto)
                           .Include(b => b.Projeto.Predio)
                           .AsExpandable()
                           .Where(predicate)
                       select new FuncionarioDTO
                       {
                           Id = func.Id,
                           Nome = func.Nome,
                           Cargo = func.Cargo ?? string.Empty,
                           Idade = func.Idade,
                           Email = func.Email ?? string.Empty,
                           Projeto = func.Projeto == null ? null : new ProjetoDTO
                           {
                               Id = func.Projeto.Id,
                               Nome = func.Projeto.Nome,
                               Descricao = func.Projeto.Descricao,
                               Predio = func.Projeto.Predio == null ? null : new PredioDTO
                               {
                                   Id = func.Projeto.Predio.Id,
                                   Nome = func.Projeto.Predio.Nome,
                                   Descricao = func.Projeto.Descricao
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
                .Include(b => b.Projeto)
                .Include(x => x.Projeto.Predio)
                .Select(b =>
                new FuncionarioDTO()
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    Cargo = b.Cargo,
                    Idade = b.Idade,
                    Email = b.Email,
                    Projeto = b.Projeto == null ? null : new ProjetoDTO
                    {
                        Id = b.Projeto.Id,
                        Nome = b.Projeto.Nome,
                        Descricao = b.Projeto.Descricao,
                        Predio = b.Projeto.Predio == null ? null : new PredioDTO
                        {
                            Id = b.Projeto.Predio.Id,
                            Nome = b.Projeto.Predio.Nome,
                            Descricao = b.Projeto.Predio.Descricao
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

            if (FuncionarioExists(Funcionario.Id))
            {
                var funcionario = _db.Funcionarios.Find(Funcionario.Id);
                var departamento = _db.Projetos.Find(Funcionario.Projeto.Id);

                if(departamento != null)
                {
                    funcionario.Projeto = departamento;
                }

                funcionario.Cargo = Funcionario.Cargo;
                funcionario.Email = Funcionario.Email;
                funcionario.Idade = Funcionario.Idade;
                funcionario.Nome = Funcionario.Nome;

                //_db.Funcionarios.Attach(Funcionario);
                _db.Entry(funcionario).State = EntityState.Modified;
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

        private bool FuncionarioExists(int id)
        {
            return _db.Funcionarios.Any(q => q.Id == id);
        }

        private bool Exists(Funcionario entity)
        {
            return _db.Funcionarios.Contains(entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}