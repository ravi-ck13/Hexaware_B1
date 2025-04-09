using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace C__CodingChallenge.Util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(DBPropertyUtil.GetConnectionString());
            conn.Open();
            return conn;
        }
    }
}
