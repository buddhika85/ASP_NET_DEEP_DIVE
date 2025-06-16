using _8_MVC_Views.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDepartmentsRepository, DepartmentsRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
