using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniturePro
{
    public class Chair : Furniture
    {
        public int NumberOfLegs { get; set; } 

      
        public Chair(string material, double price, int numberOfLegs): base(material, price) 
        {
            NumberOfLegs = numberOfLegs;
        }

     
        public override void DisplayDetails()
        {
            Console.WriteLine($"Chair - Material: {Material}, Price: {Price}, Legs: {NumberOfLegs}");
        }
    }
}
