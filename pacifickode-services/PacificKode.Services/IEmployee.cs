using PacificKode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificKode.Services
{
    public interface IEmployee
    {
        // Retrieves a collection of all Employee objects
        IEnumerable<Employee> GetAllEmployees();

        // Saves a new Employee record 
        OperationResult SaveEmployee(Employee employee);

        // Updates an existing Employee record
        OperationResult UpdateEmployee(Employee employee);

        // Deletes an Employee record by ID
        OperationResult DeleteEmployee(int id);
    }
}
