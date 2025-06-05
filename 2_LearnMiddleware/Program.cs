var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// creating a middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1: Before calling next\r\n");

    // calling next middleware
    await next(context);

    await context.Response.WriteAsync("Middleware #1: After calling next\r\n");
});


// creating a middleware #2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #2: Before calling next\r\n");

    // calling next middleware
    //await next(context);              // short circuiting 

    await context.Response.WriteAsync("Middleware #2: After calling next\r\n");
});


// creating a middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before calling next\r\n");

    // calling next middleware
    await next(context);

    await context.Response.WriteAsync("Middleware #3: After calling next\r\n");
});

app.Run();
