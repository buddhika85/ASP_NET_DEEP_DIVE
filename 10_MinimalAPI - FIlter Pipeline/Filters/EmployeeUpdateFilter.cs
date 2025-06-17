
using _10_MinimalAPI___FIlter_Pipeline.Models;

namespace _10_MinimalAPI___FIlter_Pipeline.Filters;

// This Filter targets - Employee Update endpoint handler
// builder.MapPut("/Employee/{id:int}", ([FromRoute] int id, [FromBody] Employee employee,
public class EmployeeUpdateFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // extracting paramters of end point
        var id = context.GetArgument<int>(0);
        var employee = context.GetArgument<Employee>(1);

        // validation logic
        if (id <= 0 || employee.Id != id)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {
                        "id", new [] { "Invalid Employee Id" }
                    }
                });
        }

        return await next(context);         // call next filter in the filter pipeline
    }
}
