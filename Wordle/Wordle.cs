using System;
using System.Collections.Generic;
using System.Linq;

namespace Wordle
{
    class Wordle
    {
        public string Word { get; private set; }
        public int LastedTrys { get; private set; }
        public List<string> Inputs { get; private set; }
        private Dictionary<char, int> countedCharsInWord { get; set; }

        public Wordle(string word)
        {
            Word = word.ToLower();
            LastedTrys = 5;
            Inputs = new List<string>();
            countedCharsInWord = GetCountedCharsInWord(Word);
        }

        public void RenewWordle(string input)
        {
            Inputs.Add(input.ToLower());
            LastedTrys--;
            PrintWordle();
        }

        public void PrintWordle()
        {
            Console.Clear();
            Console.WriteLine("### Wordle ###");
            Console.WriteLine("##############");
            Console.WriteLine();
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.ResetColor();
            Console.WriteLine(" Falscher Buchstabe");
            Console.WriteLine();

            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" Falschgesetzter Buchstabe");
            Console.WriteLine();

            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(" ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Korrektgesetzter Buchstabe");
            Console.ResetColor();
            Console.WriteLine();

            PrintTrys();
        }

        private Dictionary<char, int> GetCountedCharsInWord(string word)
        {
            Dictionary<char, int> countedChars = new Dictionary<char, int>();
            foreach (char c in word)
            {
                if (!countedChars.ContainsKey(c))
                {
                    countedChars.Add(c, 1);
                }
                else
                {
                    countedChars[c]++;
                }
            }

            return countedChars;
        }

        private void PrintTrys()
        {
            foreach(string input in Inputs)
            {
                for(int i = 0; i < input.Length; i++)
                {
                    Console.ForegroundColor = GetConsoleColor(input, i);
                    Console.Write(input[i]);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            for(int i = 0; i < LastedTrys; i++)
            {
                for(int j = 0; j < Word.Length; j++)
                {
                    Console.Write("_ ");
                }
                Console.WriteLine();
            }
        }

        private ConsoleColor GetConsoleColor(string input, int charIndex)
        {
            if (input[charIndex] == Word[charIndex])
            {
                return ConsoleColor.Green;
            }

            else if (Word.Contains(input[charIndex]))
            {
                return ConsoleColor.DarkYellow;
            }

            else
            {
                return ConsoleColor.White;
            }
        }
    }
}
