using _2_LearnMiddleware.MiddlewareComponenets;

var builder = WebApplication.CreateBuilder(args);

// register middleware with in DI
builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();


var app = builder.Build();

// exception handling middleware should be called FIRST on the pipe line to catch any exception thrown in down the pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

// creating a middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1: Before calling next\r\n");

    // calling next middleware
    await next(context);

    await context.Response.WriteAsync("Middleware #1: After calling next\r\n");
    throw new Exception("Exception thrown from middleware 3");
});


app.UseMiddleware<MyCustomMiddleware>();

// creating a middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before calling next\r\n");

    // calling next middleware
    await next(context);   

    await context.Response.WriteAsync("Middleware #3: After calling next\r\n");
});

app.Run();


//// creating a middleware #2
//app.Run(async (HttpContext context) =>                  // does not take parameter RequestDelegate next
//{
//    await context.Response.WriteAsync("Middleware #2: Processed. NOT CALL NEXT.\r\n");
//});

