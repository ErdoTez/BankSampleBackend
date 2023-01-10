using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.DbRepository
{
    public class DbSettings
    {
        public void Initialize(IConfiguration configuration)
        {
            ConnectionString = configuration["DbSettings:ConnectionString"];
            DatabaseSchemaName = configuration["DbSettings:DatabaseSchemaName"];

        }
        public static DbSettings Default { get; } = new DbSettings() { };

        public string ConnectionString { get; set; }

        public string DatabaseSchemaName { get; set; }
    }
}
