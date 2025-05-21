using Microsoft.Extensions.Configuration;
using PacificKode.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacificKode.Repositories.Db;

namespace PacificKode.Repositories
{
    public class HomeDAL : IHome
    {
        //Retrieves counts for home page
        public object LoadDepEmpCount()
        {
            using (SqlConnection con = DbConnection.GetOpenedConnection(DbConnection.DB_PACIFIC))
            {
                using (SqlCommand cmd = new SqlCommand("sp_LoadDepEmpCount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Return an anonymous object as object
                            return new
                            {
                                DepartmentCount = Convert.ToInt32(reader["DepartmentCount"]),
                                EmployeeCount = Convert.ToInt32(reader["EmployeeCount"])
                            };
                        }
                    }
                }
            }

            // If no data, return null or empty object
            return null;
        }


    }
}
