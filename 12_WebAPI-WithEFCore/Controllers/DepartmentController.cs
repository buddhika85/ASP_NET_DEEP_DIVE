using _12_WebAPI_WithEFCore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _12_WebAPI_WithEFCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        this.departmentRepository = departmentRepository;
    }

    [HttpGet]
    public IResult GetAll()
    {
        return TypedResults.Ok(departmentRepository.GetDepartments());
    }

    [HttpGet("/{id:int}")]
    public IResult Get([FromRoute] int id)
    {
        if (id <= 0)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "id", new string[] { "id cannot be zero or less" }
                }
            }, statusCode: 400);
        }

        var department = departmentRepository.FindById(id);
        if (department == null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "id", new string[] { $"cannot find a department with ID {id}" }
                }
            }, statusCode: 404);
        }

        return TypedResults.Ok(department);
    }
}
