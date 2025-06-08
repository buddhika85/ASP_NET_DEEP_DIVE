
namespace _2_LearnMiddleware.MiddlewareComponenets
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom middleware called\r\n");

            // pass context to next middleware in the pipiline
            await next(context);

            await context.Response.WriteAsync("Last line of custom middleware\r\n");
        }
    }
}
