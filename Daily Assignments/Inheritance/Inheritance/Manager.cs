using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Manager : Employee
    {
        private double onsite_bonus;
        private double bonus;

        public Manager(int id, string name, string dob, double salary, double onsite_bonus, double bonus)
        : base(id, name, dob, salary)
        {
            this.onsite_bonus = onsite_bonus;
            this.bonus = bonus;
        }

        public override double ComputeSalary()
        {
            return salary + onsite_bonus + bonus;
        }
    }
}
