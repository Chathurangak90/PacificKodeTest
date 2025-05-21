using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PacificKode.Services;

namespace PacificKode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHome _home;

        public HomeController(IHome home)
        {
            _home = home;
        }
        //Retrieves counts for home page
        [HttpGet("depempcount")]
        public IActionResult GetDepartmentEmployeeCount()
        {
            var data = _home.LoadDepEmpCount();

            if (data == null)
            {
                return NotFound(new { message = "No data found" });
            }

            return Ok(data);
        }
    }
}
