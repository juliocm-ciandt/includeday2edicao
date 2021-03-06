﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IncludeDay.Data.Entities;

namespace IncludeDay.Data
{
    public class IncludeDayContext : DbContext
    {
        public IncludeDayContext()
            : base("IncludeDayContext")
        {
            //Database.SetInitializer<IncludeDayContext>(new SchoolDBInitializer<IncludeDayContext>());
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Predio> Predios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Permissões
            modelBuilder.Entity<Projeto>()
                .HasRequired(x => x.Predio);
        }
    }
}
