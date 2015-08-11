using System.ComponentModel.DataAnnotations;

namespace IncludeDay.Data.Entities
{
    public class Department
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

    }
}

