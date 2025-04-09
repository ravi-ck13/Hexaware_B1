using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__CodingChallenge.Util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString()
        {
            return "Data Source=RAVI\\SQLEXPRESS;Initial Catalog=PetPals;Integrated Security=True;Encrypt=False;";
        }
    }
}
