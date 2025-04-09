using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__CodingChallenge.Entity
{
    public abstract class Donation
    {
        public string DonorName { get; set; }
        public decimal Amount { get; set; }

        public Donation(string donorName, decimal amount)
        {
            DonorName = donorName;
            Amount = amount;
        }

        public abstract void RecordDonation();
    }
}
