using C__CodingChallenge.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__CodingChallenge.dao
{
    public interface IPetDao
    {
        List<Pet> GetAvailablePets();
        void AdoptPet(string petName);
        void AddNewPet(Pet pet);


    }
}
