using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniturePro
{
    public abstract class Furniture
    {
        public string Material { get; set; }  
        public double Price { get; set; }     

        
        public Furniture(string material, double price)
        {
            Material = material;
            Price = price;
        }

        public abstract void DisplayDetails();
    }
}
