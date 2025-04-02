using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IStudent
    {
        int StudentId { get; set; }
        string Name { get; set; }
        double Fees { get; set; }
        void ShowDetails();
    }
}
