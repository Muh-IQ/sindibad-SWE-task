using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Infrastructure.Persistence
{
    internal class ConnectionProvider
    {
        private readonly string _connectionString;

        // Constructor reads the connection string from an environment variable of windows system   
        // Server=.;Database=Sindibad;Integrated Security=SSPI;TrustServerCertificate=True;
        public ConnectionProvider()
        {
            _connectionString = Environment.GetEnvironmentVariable("SINDIBAD____DB_CONNECTION")
                ?? throw new InvalidOperationException("Database connection string is not set in environment variables.");
        }

        public string GetConnectionString() => _connectionString;
    }
}
