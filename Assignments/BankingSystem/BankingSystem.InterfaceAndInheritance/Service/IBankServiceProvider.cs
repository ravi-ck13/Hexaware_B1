using BankingSystem.InterfaceAndInheritance.Bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.Service
{
    public interface IBankServiceProvider
    {
        void CreateAccount(Customer customer, string accType, float balance);
        void ListAccounts();
        void CalculateInterest();
    }
}
