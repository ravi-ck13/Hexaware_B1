using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniturePro
{
    public class Bookshelf : Furniture
    {
        public int NumberOfShelves { get; set; }  

        public Bookshelf(string material, double price, int numberOfShelves) : base(material, price)
        {
            NumberOfShelves = numberOfShelves;
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Bookshelf - Material: {Material}, Price: {Price}, Shelves: {NumberOfShelves}");
        }
    }

}
