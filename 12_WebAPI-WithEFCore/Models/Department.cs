using _12_WebAPI_WithEFCore.Models;
using System.ComponentModel.DataAnnotations;

namespace _12_WebAPI_WithEFCore.Data;

public class Department
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }


    // navigation property
    public virtual ICollection<Employee>? Employees { get; set; }
}