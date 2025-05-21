using Microsoft.AspNetCore.Mvc;

namespace Pacifickode_Web.Controllers.Department
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
