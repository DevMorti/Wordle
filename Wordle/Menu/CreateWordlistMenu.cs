using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle.Menu
{
    class CreateWordlistMenu : Menu
    {
        private List<string> wordList;
        protected override void DisplayMenu()
        {
            Console.WriteLine("Wörterliste erstellen");
            Console.WriteLine("---------------------");
            Console.WriteLine();

            string input;
            LoadWordList();
            
            while (true)
            {
                input = InputWord();

                if(input == "cancel")
                {
                    break;
                }

                else
                {
                    wordList.Add(input.ToLower());
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Die Wörterliste wird gespeichert...");
            Console.ResetColor();
            WordListManager.SaveList(wordList);
            new StartMenu();
        }

        private string InputWord()
        {
            string input;

            while (true) 
            {
                Console.Write("Wort [mit cancel beenden]: ");
                input = Console.ReadLine();

                if (ValidateInput(input))
                    break;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FEHLER: Ungültiges Wort!");
                    Console.ResetColor();
                }
            }

            return input;
        }

        private bool ValidateInput(string input)
        {
            if (!(input.Length > 0))
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            if (wordList.Contains(input))
                return false;

            return true;
        }

        private void LoadWordList()
        {
            try
            {
                wordList = WordListManager.LoadWordList();
            }
            catch (FileNotFoundException)
            {
                wordList = new List<string>();
            }
            catch (Exception ex)
            {
                wordList = new List<string>();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
