using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IncludeDay.Data.Entities;

namespace IncludeDay.Data
{
    public class IncludeDayContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Permissões
            modelBuilder.Entity<Employee>()
                .HasRequired(x => x.Department);
        }
    }
}
