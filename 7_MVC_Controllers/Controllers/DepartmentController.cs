using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _7_MVC_Controllers.Controllers
{
    [ApiController]
    [Route("api")]
    public class DepartmentController : ControllerBase
    {

        [HttpGet("departments")]
        public string GetDepartments()
        {
            return "These are the deparments";
        }

        [HttpGet("departments/{id:int}")]
        public string GetDepartmentById([FromRoute] int id)
        {
            return $"Department info: {id}";
        }


        [HttpPost("departments")]
        public Department GetDepartmentById([FromForm] Department department)
        {
            if (!ModelState.IsValid)
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }
            }

            return department;
        }
    }
}


public class Department
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }
}
