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

            var projeto1 = new Projeto { Id = 1, Nome = "Projeto A", Descricao = "Setor do Projeto A no Prédio 16", Predio = predio16 };
            var projeto2 = new Projeto { Id = 2, Nome = "Projeto B", Descricao = "Setor do Projeto B no Prédio 16", Predio = predio16 };
            var projeto3 = new Projeto { Id = 3, Nome = "RH", Descricao = "Setor de RH no Prédio 23B", Predio = predio23b };
            var projeto4 = new Projeto { Id = 4, Nome = "Marketing", Descricao = "Setor de Marketing no Prédio 23B", Predio = predio23b };
            var projeto5 = new Projeto { Id = 5, Nome = "Financeiro", Descricao = "Setor do Financeiro no Prédio 23B", Predio = predio23b };

            context.Predios.AddOrUpdate(x => x.Id,
                predio16,
                predio23b);

            context.Projetos.AddOrUpdate(x => x.Id,
                projeto1,
                projeto2,
                projeto3,
                projeto4,
                projeto5);

            context.Funcionarios.AddOrUpdate(x => x.Id,
                new Funcionario() { Id = 1, Nome = "José da Silva", Cargo = "Gerente de RH", Projeto = projeto3, Email = "Jose.silva@includeday.com.br" },
                new Funcionario() { Id = 2, Nome = "Maria Fernanda da Silva", Cargo = "Gerente de Marketing", Projeto = projeto4, Email = "maria@includeday.com.br" },
                new Funcionario() { Id = 3, Nome = "Juliana Aparecida Fernandes", Cargo = "Gerente Financeiro", Projeto = projeto5, Email = "juliana@includeday.com.br" },
                new Funcionario() { Id = 4, Nome = "Usuário Participante do IncludeDay", Cargo = null, Projeto = null });

            base.Seed(context);
        }
    }
}
