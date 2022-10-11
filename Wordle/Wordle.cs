﻿using System;
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
            LastedTrys = 6;
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

        public Dictionary<char, int> GetCountedCharsInWord(string word)
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

        public Dictionary<char, int> CompareCountedCharsWithWord(string word)
        {
            Dictionary<char, int> countedChars = new Dictionary<char, int>(countedCharsInWord); //Copy of countedChars
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == Word[i])
                {
                    countedChars[word[i]]--;
                }
            }

            return countedChars;
        }

        private void PrintTrys()
        {
            foreach(string input in Inputs)
            {
                Dictionary<char, int> comparedChars = CompareCountedCharsWithWord(input);

                for (int i = 0; i < input.Length; i++)
                {
                    ConsoleColor consoleColor;
                    if (input[i] == Word[i])
                    {
                        consoleColor = ConsoleColor.Green;
                    }

                    else if (Word.Contains(input[i]) && comparedChars[input[i]] != 0)
                    {
                        comparedChars[input[i]]--;
                        consoleColor = ConsoleColor.DarkYellow;
                    }

                    else
                    {
                        consoleColor = ConsoleColor.White;
                    }
                    Console.ForegroundColor = consoleColor;
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
    }
}
