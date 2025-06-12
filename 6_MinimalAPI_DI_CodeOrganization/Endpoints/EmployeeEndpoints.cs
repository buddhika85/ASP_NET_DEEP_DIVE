using _6_MinimalAPI_DI_CodeOrganization.Models;
using Microsoft.AspNetCore.Mvc;


namespace _6_MinimalAPI_DI_CodeOrganization.Endpoints
{
    // static extension class for end points
    public static class EmployeeEndpoints
    {
        // static extension method WebApplication
        public static void MapEmployeeEndpoints(this WebApplication app)
        {

            app.MapGet("/Employee", (IEmployeesRepository employeesRepository) =>
            {
                return TypedResults.Ok(employeesRepository.GetEmployees());
            });

            app.MapGet("/Employee/{id:int}", ([FromRoute] int id, IEmployeesRepository employeesRepository) =>
            {
                var employee = employeesRepository.FindById(id);
                return employee is null
                    ? Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                        {
                { "id", new [] { $"Employee with ID {id} unavailable." } }
                        })
                    : TypedResults.Ok(employee);
            });

            app.MapPost("/Employee", ([FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
            {
                if (employee == null)
                {
                    return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        { "employee", new [] { "Employee information not provided" } }
                    });
                }
                employeesRepository.AddEmployee(employee);

                // URL get by ID, Object created
                return TypedResults.Created(uri: $"/employee/{employee.Id}", value: employee);

            }).WithParameterValidation();

            app.MapPut("/Employee/{id:int}", ([FromRoute] int id, [FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
            {
                if (employee == null)
                {
                    return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        { "employee", new [] { "Employee information not provided" } }
                    });
                }
                if (employee.Id != id)
                {
                    return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        { "employee", new [] { "Employee ids provided do not match." } }
                    });
                }
                if (!employeesRepository.IsExists(employee.Id))
                {
                    return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        { "id", new [] { $"Employee with ID {employee.Id} does not exist." } }
                    });
                }
                employeesRepository.UpdateEmployee(employee);

                // PUT should return No Content if successful
                return TypedResults.NoContent();
            }).WithParameterValidation(); ;

            app.MapDelete("/Employee/{id:int}", ([FromRoute] int id, IEmployeesRepository employeesRepository) =>
            {

                var employeeToDelete = employeesRepository.FindById(id);
                if (employeeToDelete == null)
                {
                    return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        { "id", new [] { $"Employee with ID {id} does not exist." } }
                    });
                }
                employeesRepository.DeleteEmployee(employeeToDelete);
                return TypedResults.Ok(employeeToDelete);
            });

        }
    }
}
