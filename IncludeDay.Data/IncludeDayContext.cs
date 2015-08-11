using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IncludeDay.Data.Entities;

namespace IncludeDay.Data
{
    public class IncludeDayContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Predio> Predios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Permissões
            modelBuilder.Entity<Departamento>()
                .HasRequired(x => x.Predio);
        }
    }
}
