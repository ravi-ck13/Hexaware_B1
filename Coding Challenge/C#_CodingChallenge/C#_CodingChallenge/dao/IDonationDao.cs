using C__CodingChallenge.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__CodingChallenge.dao
{
    public interface IDonationDao
    {
        void InsertCashDonation(CashDonation donation);
        void InsertItemDonation(ItemDonation donation);
    }
}
