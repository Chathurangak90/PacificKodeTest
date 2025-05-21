using PacificKode.Models;
using PacificKode.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using PacificKode.Repositories.Db;

namespace PacificKode.Repositories
{
    // Data Access Layer class for Employee operations implementing IEmployee interface.
    public class EmployeeDAL : IEmployee
    {
        // Deletes an employee by ID 
        public OperationResult DeleteEmployee(int id)
        {
            var result = new OperationResult();

            // Open SQL connection
            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                // Create SQL command for the stored procedure
                using (SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.StatusId = Convert.ToInt32(reader["StatusId"]);
                            result.Message = reader["Message"]?.ToString() ?? string.Empty;
                        }
                        else
                        {
                            // If no response, set error status
                            result.StatusId = -1;
                            result.Message = "No response from procedure.";
                        }
                    }
                }
            }

            return result;
        }

        // Retrieves all employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map each record to an Employee object
                            employees.Add(new Employee
                            {
                                EmployeeId = (int)reader["EmployeeId"],
                                FirstName = reader["FirstName"]?.ToString() ?? string.Empty,
                                LastName = reader["LastName"]?.ToString() ?? string.Empty,
                                Email = reader["Email"]?.ToString() ?? string.Empty,
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Age = (int)reader["Age"],
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                DepartmentId = (int)reader["DepartmentId"],
                                DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            });
                        }
                    }
                }
            }

            return employees;
        }

        // Saves a new employee
        public OperationResult SaveEmployee(Employee employee)
        {
            var result = new OperationResult();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SaveUpdateEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Age", employee.Age);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.StatusId = Convert.ToInt32(reader["StatusId"]);
                            result.Message = reader["Message"]?.ToString() ?? string.Empty;
                        }
                        else
                        {
                            result.StatusId = -1;
                            result.Message = "No response from procedure.";
                        }
                    }
                }
            }

            return result;
        }

        // Updates an existing employee
        public OperationResult UpdateEmployee(Employee employee)
        {
            var result = new OperationResult();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SaveUpdateEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Age", employee.Age);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.StatusId = Convert.ToInt32(reader["StatusId"]);
                            result.Message = reader["Message"]?.ToString() ?? string.Empty;
                        }
                        else
                        {
                            result.StatusId = -1;
                            result.Message = "No response from procedure.";
                        }
                    }
                }
            }

            return result;
        }
    }
}
