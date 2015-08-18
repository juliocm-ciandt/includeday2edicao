using System;
using System.ComponentModel.DataAnnotations;

namespace IncludeDay.Data.Entities
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public virtual Predio Predio { get; set; }

    }
}

