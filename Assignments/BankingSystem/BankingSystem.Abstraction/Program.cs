namespace BankingSystem.Abstraction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Bank bank = new Bank();
            bank.CreateAccount();
            bank.PerformOperations();
        }
    }
}
