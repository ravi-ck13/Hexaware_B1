namespace Strings
{
    internal class Program
    {
        static void Main()
        {
           

            // 1: Find the length of a word
            WordLength();

            // 2: Reverse a word
            ReverseWord();

            // 3: Compare two words
            CompareWords();
        }

        static void WordLength()
        {
            Console.Write("Enter a word: ");
            string word = Console.ReadLine();
            Console.WriteLine($"Length of the word: {word.Length}");
        }

        static void ReverseWord()
        {
            Console.Write("Enter a word: ");
            string word = Console.ReadLine();
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            string reversedWord = new string(charArray);
            Console.WriteLine($"Reversed word: {reversedWord}");
        }

        static void CompareWords()
        {
            Console.Write("Enter first word: ");
            string word1 = Console.ReadLine();
            Console.Write("Enter second word: ");
            string word2 = Console.ReadLine();
            if (word1.Equals(word2, StringComparison.OrdinalIgnoreCase))
                Console.WriteLine("The words are the same.");
            else
                Console.WriteLine("The words are different.");
        }
    }
}
