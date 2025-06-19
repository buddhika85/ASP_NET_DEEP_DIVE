using Microsoft.AspNetCore.Mvc;

namespace _12_WebAPI_WithEFCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    public IActionResult Index()
    {
        return View();
    }
}
