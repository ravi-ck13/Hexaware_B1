using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Employee
    {
        protected int id;
        protected string name;
        protected string dob;
        protected double salary;

        public Employee(int id, string name, string dob, double salary)
        {
            this.id = id;
            this.name = name;
            this.dob = dob;
            this.salary = salary;
        }

        public virtual double ComputeSalary()
        {
            return salary;
        }
    }
}
