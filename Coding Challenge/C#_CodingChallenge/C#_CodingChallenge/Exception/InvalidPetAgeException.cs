using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__CodingChallenge.Exception
{
    public class InvalidPetAgeException : System.Exception
    {
        public InvalidPetAgeException(string message) : base(message) { }
    }
}
