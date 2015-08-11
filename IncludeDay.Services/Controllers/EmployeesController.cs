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
    public class EmployeesController : ApiController
    {
        private readonly IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Employees
        public List<Employee> GetEmployees([FromUri]Employee filter)
        {

            var predicate = PredicateBuilder.True<Employee>();

            if (filter != null && !string.IsNullOrEmpty(filter.Name))
            {
                predicate = predicate.And(p => p.Name.Contains(filter.Name));
            }

            if (filter != null && !string.IsNullOrEmpty(filter.Position))
            {
                predicate = predicate.And(p => p.Position.Contains(filter.Position));
            }

            var list = _db.Employees.AsExpandable().Where(predicate);
            return list.ToList();

        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            _db.Entry(employee).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Employees.Add(employee);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employee);
            _db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return _db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}