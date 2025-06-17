using _10_MinimalAPI___FIlter_Pipeline.Filters;
using _10_MinimalAPI___FIlter_Pipeline.Models;
using Microsoft.AspNetCore.Mvc;

namespace _10_MinimalAPI___FIlter_Pipeline.Endpoints;

public static class EmployeeEndpoints
{
    public static void AddEmployeeEndPoints(this WebApplication builder)
    {
        builder.MapGet("/Employee", (IEmployeesRepository employeesRepository) =>
        {
            return TypedResults.Ok(employeesRepository.GetEmployees());
        });

        builder.MapGet("/Employee/{id:int}", ([FromRoute] int id, IEmployeesRepository employeesRepository) =>
        {
            if (id <= 0)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {
                        "id", new [] { "Invalid Employee Id" }
                    }
                });
            }

            var employee = employeesRepository.FindById(id);
            if (employee == null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {
                        "id", new [] { $"Invalid with Id {id} unvailable" }
                    }
                }, statusCode: 404);
            }

            return TypedResults.Ok(employee);
        });

        builder.MapPost("/Employee", ([FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
        {
            if (employee == null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "employee", new string[] { "Employee information not provided" }}
                }, statusCode: 400);
            }

            employeesRepository.AddEmployee(employee);
            return TypedResults.Created(uri: $"/Employee/{employee.Id}", employee);
        }).WithParameterValidation();

        builder.MapPut("/Employee/{id:int}", ([FromRoute] int id, [FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
        {
            var isExists = employeesRepository.IsExists(id);
            if (!isExists)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {
                        "id", new [] { $"Employee with Id {id} unavailable to edit" }
                    }
                }, statusCode: 404);
            }

            employeesRepository.UpdateEmployee(employee);
            return TypedResults.NoContent();
        }).WithParameterValidation()
        .AddEndpointFilter<EmployeeUpdateFilter>();         // adding filter

        builder.MapDelete("/Employee/{id:int}", ([FromRoute] int id, IEmployeesRepository employeesRepository) =>
        {
            if (id <= 0)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {
                        "id", new [] { "Invalid Employee Id" }
                    }
                });
            }

            var employee = employeesRepository.FindById(id);
            if (employee == null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {
                        "id", new [] { $"Employee with Id {id} unavailable to edit" }
                    }
                }, statusCode: 404);
            }

            employeesRepository.DeleteEmployee(employee);
            return TypedResults.Ok(employee);

        }).WithParameterValidation();
    }
}
