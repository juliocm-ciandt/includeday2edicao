using System.Data.Entity.Migrations;
using IncludeDay.Data.Entities;

namespace IncludeDay.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IncludeDayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IncludeDayContext context)
        {
            var predio16 = new Predio { Id = 1, Nome = "Prédio 16", Descricao = "Prédio 16 - CPS" };
            var predio23b = new Predio { Id = 2, Nome = "Prédio 23B", Descricao = "Prédio 23B - CPS" };

            var departamento1 = new Departamento { Id = 1, Nome = "Projeto A", Descricao = "Setor do Projeto A no Prédio 16", Predio = predio16 };
            var departamento2 = new Departamento { Id = 2, Nome = "Projeto B", Descricao = "Setor do Projeto B no Prédio 16", Predio = predio16 };
            var departamento3 = new Departamento { Id = 3, Nome = "RH", Descricao = "Setor de RH no Prédio 23B", Predio = predio23b };
            var departamento4 = new Departamento { Id = 4, Nome = "Marketing", Descricao = "Setor de Marketing no Prédio 23B", Predio = predio23b };
            var departamento5 = new Departamento { Id = 5, Nome = "Financeiro", Descricao = "Setor do Financeiro no Prédio 23B", Predio = predio23b };

            context.Predios.AddOrUpdate(x => x.Id,
                predio16,
                predio23b);

            context.Departamentos.AddOrUpdate(x => x.Id,
                departamento1,
                departamento2,
                departamento3,
                departamento4,
                departamento5);

            context.Funcionarios.AddOrUpdate(x => x.Id,
                new Funcionario() { Id = 1, Nome = "José da Silva", Cargo = "Gerente de RH", Departamento = departamento3 },
                new Funcionario() { Id = 2, Nome = "Maria Fernanda da Silva", Cargo = "Gerente de Marketing", Departamento = departamento4 },
                new Funcionario() { Id = 3, Nome = "Juliana Aparecida Fernandes", Cargo = "Gerente Financeiro", Departamento = departamento5 });

            base.Seed(context);
        }
    }
}
