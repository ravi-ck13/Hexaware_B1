using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace PayrollManagement.Utility
{
    internal static class DBConnUtil
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PayXpert"].ConnectionString;
        }
    }
}
