using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.InterfaceAndInheritance.Bean;
namespace BankingSystem.InterfaceAndInheritance
{
    public class CustomerNameComparer : IComparer<Account>
    {
        public int Compare(Account x, Account y)
        {
            return string.Compare(x.Customer.Name, y.Customer.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
