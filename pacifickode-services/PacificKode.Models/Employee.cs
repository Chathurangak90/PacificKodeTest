using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificKode.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
