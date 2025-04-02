namespace FurniturePro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Chair myChair = new Chair("Wood", 1500, 4);
            myChair.DisplayDetails(); 

            
            Bookshelf myBookshelf = new Bookshelf("Metal", 3000, 5);
            myBookshelf.DisplayDetails();
        }
    }
}
