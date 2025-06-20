using _12_WebAPI_WithEFCore.Data;
using _12_WebAPI_WithEFCore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace _12_WebAPI_WithEFCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository departmentRepository;

    public DepartmentController(IConfiguration config, IDepartmentRepository departmentRepository)
    {
        //var showNewData = bool.Parse(config["DisplaySettings:ShowNewData"]);

        this.departmentRepository = departmentRepository;
    }

    [HttpGet]
    public IResult GetAll()
    {
        return TypedResults.Ok(departmentRepository.GetDepartments());
    }

    [HttpGet("{id:int}")]
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


    [HttpPost]
    public IResult Create([FromBody] Department department)
    {
        if (department == null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> { { "department", new string[] { "Did not provide a department" } } }, statusCode: 400);
        }

        departmentRepository.AddDepartment(department);
        return TypedResults.Created(uri: $"api/Department/{department.Id}", department);
    }

    [HttpPut("{id:int}")]
    public IResult Update([FromRoute] int id, [FromBody] Department department)
    {
        if (!departmentRepository.IsExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> { { "id", new string[] { $"Department with ID {id} unavailable" } } }, statusCode: 404);
        }
        departmentRepository.UpdateDepartment(department);
        return TypedResults.NoContent();
    }

    [HttpDelete("{id:int}")]
    public IResult Delete([FromRoute] int id)
    {
        if (!departmentRepository.IsExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> { { "id", new string[] { $"Department with id {id} unavailable" } } }, statusCode: 404);
            //return Results.Problem(new ProblemDetails { Detail = $"Department with id {id} unavailable", Title = "Not FOund", Status = 404 });
        }
        departmentRepository.DeleteDepartment(id);
        return TypedResults.Ok();
    }
}
