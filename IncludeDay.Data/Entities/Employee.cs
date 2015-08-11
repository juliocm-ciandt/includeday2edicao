using System.ComponentModel.DataAnnotations;

namespace IncludeDay.Data.Entities
{
    public class Employee
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public virtual Department Department { get; set; }

    }

}

