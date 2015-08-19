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

            var projeto1 = new Projeto { Id = 1, Nome = "Projeto Itaú", Descricao = "Setor do Itaú no Prédio 16", Predio = predio16 };
            var projeto2 = new Projeto { Id = 2, Nome = "Projeto Banco do Brasil", Descricao = "Setor do Banco do Brasil no Prédio 16", Predio = predio16 };
            var projeto3 = new Projeto { Id = 3, Nome = "Projeto Bradesco", Descricao = "Setor de Bradesco no Prédio 16", Predio = predio16 };
            var projeto4 = new Projeto { Id = 4, Nome = "Projeto HSBC", Descricao = "Setor de HSBC no Prédio 16", Predio = predio16 };
            var projeto5 = new Projeto { Id = 5, Nome = "Projeto Caixa", Descricao = "Setor do Caixa no Prédio 16", Predio = predio16 };

            var projeto6 = new Projeto { Id = 6, Nome = "Projeto Kalunga", Descricao = "Setor do Kalunga no Prédio 23B", Predio = predio23b };
            var projeto7 = new Projeto { Id = 7, Nome = "Projeto Ponto Frio", Descricao = "Setor do Ponto Frio no Prédio 23B", Predio = predio23b };
            var projeto8 = new Projeto { Id = 8, Nome = "Projeto Magazine Luiza", Descricao = "Setor de Magazine Luiza no Prédio 23B", Predio = predio23b };
            var projeto9 = new Projeto { Id = 9, Nome = "Projeto Casas Bahia", Descricao = "Setor de Casas Bahia no Prédio 23B", Predio = predio23b };
            var projeto10 = new Projeto { Id = 10, Nome = "Projeto Saraiva", Descricao = "Setor do Saraiva no Prédio 23B", Predio = predio23b };


            context.Predios.AddOrUpdate(x => x.Id,
                predio16,
                predio23b);

            context.Projetos.AddOrUpdate(x => x.Id,
                projeto1,
                projeto2,
                projeto3,
                projeto4,
                projeto5,
                projeto6,
                projeto7,
                projeto8,
                projeto9,
                projeto10);

            context.Funcionarios.AddOrUpdate(x => x.Id,
                new Funcionario() { Id = 1, Nome = "José da Silva", Cargo = "Gerente de RH", Projeto = projeto7, Email = "Jose.silva@includeday.com.br", Idade = 34 },
                new Funcionario() { Id = 2, Nome = "Maria Fernanda da Silva", Cargo = "Gerente de Marketing", Projeto = projeto3, Email = "maria@includeday.com.br", Idade = 28 },
                new Funcionario() { Id = 3, Nome = "Juliana Aparecida Fernandes", Cargo = "Gerente Financeiro", Projeto = projeto1, Email = "juliana@includeday.com.br", Idade = 44 },
                new Funcionario() { Id = 4, Nome = "Usuário IncludeDay", Cargo = "Desenvolvedor", Idade = 18, Email = "includeday@ciandt.com" });

            base.Seed(context);
        }
    }
}
