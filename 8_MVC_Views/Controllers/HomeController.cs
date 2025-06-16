using _8_MVC_Views.Model;
using Microsoft.AspNetCore.Mvc;

namespace _8_MVC_Views.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDepartmentsRepository departmentsRepository;

        public HomeController(IDepartmentsRepository departmentsRepository)
        {
            this.departmentsRepository = departmentsRepository;
        }

        public IActionResult Index()
        {
            return View();
            //return View("NewIndex");
        }


        public IActionResult Details([FromRoute]int id)
        {
            var model = departmentsRepository.GetDepartmentById(id);
            return View(model);
        }
    }
}
