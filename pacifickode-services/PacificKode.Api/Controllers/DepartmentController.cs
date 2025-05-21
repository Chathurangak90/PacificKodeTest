using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PacificKode.Models;
using PacificKode.Repositories;
using PacificKode.Services;

namespace PacificKode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // Dependency injection of the IDepartment service to access department-related data operations
        private readonly IDepartment _department;

        // Constructor injecting the IDepartment implementation
        public DepartmentController(IDepartment department)
        {
            _department = department;
        }

        //Retrieves all departments from the database.
        [HttpGet("loadalldepartment")]
        public ActionResult<IEnumerable<Department>> GetAllDepartments()
        {
            var departments = _department.GetAllDepartments();
            return Ok(departments);
        }

        //Saves a new department to the database.
        [HttpPost("savedepartment")]
        public ActionResult<OperationResult> SaveDepartment([FromBody] Department department)
        {
            if (department == null)
                return BadRequest(new OperationResult
                {
                    StatusId = -1,
                    Message = "Invalid input"
                });

            var result = _department.SaveDepartment(department);
            return Ok(result);
        }

        //Updates an existing department in the database.
        [HttpPut("updatedepartment")]
        public ActionResult<OperationResult> UpdateDepartment([FromBody] Department department)
        {
            if (department == null || department.DepartmentId <= 0)
            {
                return BadRequest(new OperationResult
                {
                    StatusId = -1,
                    Message = "Invalid department data for update."
                });
            }

            var result = _department.UpdateDepartment(department);
            return Ok(result);
        }

        //Deletes a department by its ID.
        [HttpDelete("deletedepartment/{id}")]
        public ActionResult<OperationResult> DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new OperationResult
                {
                    StatusId = -1,
                    Message = "Invalid department ID."
                });
            }

            var result = _department.DeleteDepartment(id);
            return Ok(result);
        }
    }
}
