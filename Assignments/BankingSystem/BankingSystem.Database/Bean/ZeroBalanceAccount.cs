using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DatabaseConnectivity.Bean
{
    public class ZeroBalanceAccount : Account
    {
        public ZeroBalanceAccount(Customer customer)
        {
            this.Balance = 0;
            this.Customer = customer;
            this.AccountType = "ZeroBalance";
        }

        public ZeroBalanceAccount() { }
    }
}
