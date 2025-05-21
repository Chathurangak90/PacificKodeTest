using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PacificKode.Models;
using PacificKode.Services;

namespace PacificKode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Dependency injected service interface to manage employee data operations
        private readonly IEmployee _employee;

        // Constructor injects the IEmployee service to be used in the controller
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        // Retrieves a list of all employees
        [HttpGet("loadallemployees")]
        public ActionResult<IEnumerable<Department>> GetAllEmployees()
        {
            var employees = _employee.GetAllEmployees();
            return Ok(employees);
        }

        // Saves a new employee record, expects employee data in the request body
        [HttpPost("saveemployee")]
        public ActionResult<OperationResult> SaveEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest(new OperationResult
                {
                    StatusId = -1,
                    Message = "Invalid input"
                });

            var result = _employee.SaveEmployee(employee);
            return Ok(result);
        }

        // Updates an existing employee record, expects employee data with valid EmployeeId
        [HttpPut("updateemployee")]
        public ActionResult<OperationResult> UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null || employee.EmployeeId <= 0)
            {
                return BadRequest(new OperationResult
                {
                    StatusId = -1,
                    Message = "Invalid employee data for update."
                });
            }
            var result = _employee.UpdateEmployee(employee);
            return Ok(result);
        }

        // Deletes an employee by the given employee ID 
        [HttpDelete("deleteemployee/{id}")]
        public ActionResult<OperationResult> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new OperationResult
                {
                    StatusId = -1,
                    Message = "Invalid employee ID."
                });
            }

            var result = _employee.DeleteEmployee(id);
            return Ok(result);
        }
    }
}
