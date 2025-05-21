using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace PacificKode.Repositories.Db
{
    public class DbConnection
    {
        private static readonly Dictionary<int, string> connections = new Dictionary<int, string>();
        public const int DB_PACIFIC = 0;

        static DbConnection()
        {
            // Build configuration to read appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Ensures path works even in published apps
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var pacificConnString = config["ConnectionStrings:PACIFIC"];

            if (string.IsNullOrWhiteSpace(pacificConnString))
            {
                throw new InvalidOperationException("Connection string for PACIFIC is missing or empty in appsettings.json.");
            }

            connections.Add(DB_PACIFIC, pacificConnString);
        }

        public static SqlConnection GetOpenedConnection(int database)
        {
            var connection = new SqlConnection(connections[database]);
            connection.Open();
            return connection;
        }
    }
}
