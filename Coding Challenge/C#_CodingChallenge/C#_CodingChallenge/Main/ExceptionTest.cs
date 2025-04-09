using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C__CodingChallenge.Entity;
using C__CodingChallenge.Exception;

namespace C__CodingChallenge.Main
{
    public class ExceptionTest
    {
        public static void Run()
        {
            bool running = true;
            PetShelter shelter = new PetShelter();

            while (running)
            {
                Console.WriteLine("\n--- Exception Handling Test Menu ---");
                Console.WriteLine("1. Add Pet (Invalid Age Check)");
                Console.WriteLine("2. Display Pets (Null Reference Handling)");
                Console.WriteLine("3. Make Donation (Insufficient Funds)");
                Console.WriteLine("4. Read Pets from File (File Handling)");
                Console.WriteLine("5. Simulate Adoption (Adoption Exception)");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Pet Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Pet Age: ");
                            int age = int.Parse(Console.ReadLine());

                            if (age <= 0)
                                throw new InvalidPetAgeException("Pet age must be a positive number.");

                            Console.Write("Enter Breed: ");
                            string breed = Console.ReadLine();

                            Pet pet = new Pet(name, age, breed);
                            shelter.AddPet(pet);
                            Console.WriteLine("Pet added successfully.");
                        }
                        catch (InvalidPetAgeException ex)
                        {
                            Console.WriteLine($"[InvalidPetAgeException] {ex.Message}");
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine($"[Error] {ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            shelter.ListAvailablePets(); // if pet object contains null, it will simulate NullReferenceException
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("[NullReferenceException] Pet data is incomplete or missing.");
                        }
                        break;

                    case "3":
                        try
                        {
                            Console.Write("Enter donor name: ");
                            string donor = Console.ReadLine();

                            Console.Write("Enter donation amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                            if (amount < 10)
                                throw new InsufficientFundsException("Minimum donation amount is $10.");

                            var donation = new CashDonation(donor, amount, DateTime.Now);
                            donation.RecordDonation();
                        }
                        catch (InsufficientFundsException ex)
                        {
                            Console.WriteLine($"[InsufficientFundsException] {ex.Message}");
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine($"[Error] {ex.Message}");
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.Write("Enter file path to read pets: ");
                            string path = Console.ReadLine();

                            string content = File.ReadAllText(path);
                            Console.WriteLine("\nFile Content:\n" + content);
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("[FileHandlingException] File not found.");
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("[FileHandlingException] Unable to read the file.");
                        }
                        break;

                    case "5":
                        try
                        {
                            Console.Write("Enter pet name to adopt: ");
                            string petName = Console.ReadLine();

                            // Let's simulate failure (e.g., pet doesn't exist or name is blank)
                            if (string.IsNullOrEmpty(petName))
                                throw new AdoptionException("Pet name cannot be empty.");

                            // Further logic: check pet availability (skipped for simplicity)
                            Console.WriteLine("Pet adopted successfully.");
                        }
                        catch (AdoptionException ex)
                        {
                            Console.WriteLine($"[AdoptionException] {ex.Message}");
                        }
                        break;

                    case "6":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
