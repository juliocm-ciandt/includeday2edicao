using System;
using System.ComponentModel.DataAnnotations;

namespace IncludeDay.Data.Entities
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Cargo { get; set; }

        public string Email { get; set; }

        public int Idade { get; set; }

        public virtual Departamento Departamento { get; set; }
    }
}

