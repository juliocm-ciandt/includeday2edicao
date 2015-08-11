namespace IncludeDay.Data.Migrations
{
    using IncludeDay.Data.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IncludeDay.Data.IncludeDayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(IncludeDay.Data.IncludeDayContext context)
        {
            var predio16 = new Predio { Id = 1, Nome = "Prédio 16", Descricao = "Prédio 16 - CPS" };
            var predio23b = new Predio { Id = 2, Nome = "Prédio 23B", Descricao = "Prédio 23B - CPS" };

            context.Predios.AddOrUpdate(x => x.Id,
                predio16,
                predio23b);

            context.Departamentos.AddOrUpdate(x => x.Id,
                new Departamento() { Id = 1, Nome = "Projeto A", Descricao = "Setor do Projeto A no Prédio 16", Predio = predio16 },
                new Departamento() { Id = 2, Nome = "Projeto B", Descricao = "Setor do Projeto B no Prédio 16", Predio = predio16 },
                new Departamento() { Id = 1, Nome = "RH", Descricao = "Setor de RH no Prédio 23B", Predio = predio23b },
                new Departamento() { Id = 1, Nome = "Marketing", Descricao = "Setor de Marketing no Prédio 23B", Predio = predio23b },
                new Departamento() { Id = 1, Nome = "Financeiro", Descricao = "Setor do Financeiro no Prédio 23B", Predio = predio23b });
        }
    }
}
