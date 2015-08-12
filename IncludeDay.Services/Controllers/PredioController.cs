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
    public class PredioController : ApiController
    {
        private IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Departamento
        [ResponseType(typeof(List<PredioDTO>))]
        public List<PredioDTO> GetPredio([FromUri]Predio filter)
        {
            var predicate = PredicateBuilder.True<Predio>();

            if (filter != null && !string.IsNullOrEmpty(filter.Nome))
            {
                predicate = predicate.And(p => p.Nome.Contains(filter.Nome));
            }

            if (filter != null && filter.Id > 0)
            {
                predicate = predicate.And(p => p.Id == filter.Id);
            }

            var list = from pred in _db.Predios.AsExpandable().Where(predicate)
                       select new PredioDTO
                        {
                            Id = pred.Id,
                            Nome = pred.Nome,
                            Descricao = pred.Descricao
                        };

            return list.ToList();
        }

        // GET: api/Departamento/5
        [ResponseType(typeof(PredioDTO))]
        public async Task<IHttpActionResult> GetPredio(int id)
        {
            var department = await _db.Predios.Select(b =>
                new PredioDTO
                {
                    Id = b.Id,
                    Descricao = b.Descricao,
                    Nome = b.Nome
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    }
}
