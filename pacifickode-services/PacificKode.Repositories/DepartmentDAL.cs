using PacificKode.Models;
using PacificKode.Repositories.Db;
using PacificKode.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificKode.Repositories
{
    // Data Access Layer class for Department operations implementing IDepartment interface.
    public class DepartmentDAL: IDepartment
    {
        // Deletes a department by its ID
        public OperationResult DeleteDepartment(int id)
        {
            var result = new OperationResult();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.StatusId = Convert.ToInt32(reader["StatusId"]);
                           
                            result.Message = reader["Message"]?.ToString() ?? "No message returned.";
                        }
                        else
                        {
                            result.StatusId = -1;
                            result.Message = "No response from stored procedure.";
                        }
                    }
                }
            }

            return result;
        }

        // Retrieves all departments from the database
        public IEnumerable<Department> GetAllDepartments()
        {
            var departments = new List<Department>();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllDepartments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                DepartmentId = (int)reader["DepartmentId"],
                                DepartmentCode = reader["DepartmentCode"]?.ToString() ?? string.Empty,
                                DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }

            return departments;
        }

        // Saves a new department
        public OperationResult SaveDepartment(Department department)
        {
            var result = new OperationResult();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SaveUpdateDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

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

        // Updates an existing department
        public OperationResult UpdateDepartment(Department department)
        {
            var result = new OperationResult();

            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SaveUpdateDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.StatusId = Convert.ToInt32(reader["StatusId"]);
                            result.Message = reader["Message"]?.ToString() ?? string.Empty;
                        }
                        else
                        {
                            result.StatusId = -99;
                            result.Message = "Unexpected error.";
                        }
                    }
                }
            }

            return result;
        }

    }
}
