
namespace _2_LearnMiddleware.MiddlewareComponenets
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await context.Response.WriteAsync($"Excpetion middleware found, calling next middleware \r\n");
                await next(context);
			}
			catch (Exception e)
			{
                await context.Response.WriteAsync($"Exception occurred {e.Message}\r\n");
			}
            finally 
            {
                await context.Response.WriteAsync($"Excpetion middleware complete \r\n");
            }
        }
    }
}
