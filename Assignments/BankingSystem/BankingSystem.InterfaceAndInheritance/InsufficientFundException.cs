﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance
{
    public class InsufficientFundException : Exception
    {
        public InsufficientFundException(string message) : base(message) { }
    }
}
