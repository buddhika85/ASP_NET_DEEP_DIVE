using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _7_MVC_Controllers.Controllers
{
    [ApiController]
    [Route("api")]
    public class DepartmentController : ControllerBase
    {

        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            return new ContentResult
            {
                Content = "These are the deparments",
                ContentType = "text/plain",
                StatusCode = 200
            };
        }

        [HttpGet("departments/{id:int}")]
        public IActionResult GetDepartmentById([FromRoute] int id)
        {
            return new JsonResult(new Department { Id = id, Description = "Accounts", Name = "Accounts" });
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
