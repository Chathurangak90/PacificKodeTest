using Microsoft.AspNetCore.Mvc;

namespace Pacifickode_Web.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
