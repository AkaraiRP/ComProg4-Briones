using System;

namespace _01LAB_Briones
{
    class Program
    {
        public static readonly string[] WordList = { "String", "CSharp", "Operators", "Integer", "Char" };
        static void Main(string[] args)
        {

            Console.WriteLine("Headless Hangman\n");

            int RandomWord = new Random().Next(0, WordList.Length - 1);
            string To_Guess = WordList[RandomWord];
            string Answer = To_Guess.ToLower();

            string Chopped_Word = To_Guess;
            int RandomChar = new Random().Next(1, To_Guess.Length - 3);

            for (int i = 0; i < RandomChar; i++) 
            {
                char Char_To_Replace = To_Guess[new Random().Next(0, To_Guess.Length - 1)];
                Chopped_Word = Chopped_Word.Replace(Char_To_Replace, '_');
            }

            string CleanedUp_Word = "    ";
            for (int z = 0; z < Chopped_Word.Length; z++)
            {
                CleanedUp_Word += $@"{Chopped_Word[z]} ";
            }

            Console.WriteLine(CleanedUp_Word);

            Console.WriteLine("\nGuess the Word.");
            string Input = Console.ReadLine().ToLower();

            if (Input.Equals(Answer))
            {
                Console.Clear();
                Console.WriteLine($@"You guessed correctly! The word is {To_Guess}!");
            }

            else
            {
                Console.Clear();
                Console.WriteLine($@"You guessed incorrectly, the word is {To_Guess}.");
            }

        }
    }
}
