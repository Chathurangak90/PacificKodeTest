using PacificKode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificKode.Services
{
    public  interface IDepartment
    {
        //Retrieves all departments.
        IEnumerable<Department> GetAllDepartments();

        // Saves a new department.
        OperationResult SaveDepartment (Department department);

        // Updates an existing department.
        OperationResult UpdateDepartment(Department department);

        // Deletes a department by its ID.
        OperationResult DeleteDepartment(int id);
    }
}
