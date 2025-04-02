namespace UDException
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            try
            {
                BankAccount account = new BankAccount(101, 5000);  
                Console.WriteLine($"Account created with Balance: {account.Balance:C}");

                
                account.TransferFunds(6000);  
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
