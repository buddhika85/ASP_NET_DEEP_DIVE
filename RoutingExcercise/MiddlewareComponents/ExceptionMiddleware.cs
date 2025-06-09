
namespace RoutingExcercise.MiddlewareComponents
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 501;		// internal server error
				await context.Response.WriteAsync($"{ex.ToString()}");				
			}
        }
    }
}
