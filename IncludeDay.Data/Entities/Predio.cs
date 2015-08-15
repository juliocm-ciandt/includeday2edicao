using System;
using System.ComponentModel.DataAnnotations;

namespace IncludeDay.Data.Entities
{
    /// <summary>
    /// Prédios dos locais
    /// </summary>
    public class Predio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}

