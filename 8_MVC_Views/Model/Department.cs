using System.ComponentModel.DataAnnotations;

namespace _8_MVC_Views.Model
{
    public class Department
    {
        public Department()
        {

        }

        public Department(int id, string name, string? description = "")
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
