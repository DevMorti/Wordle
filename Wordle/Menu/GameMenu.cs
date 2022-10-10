using System;

namespace Wordle.Menu
{
    abstract class GameMenu
    {
        protected Wordle Wordle { get; set; }

        public GameMenu(string word)
        {
            wordle = new Wordle(word);
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            bool correctWordle = false;

            wordle.PrintWordle();
            while(wordle.LastedTrys != 0)
            {
                Console.WriteLine();
                string input = InputOption();
                wordle.RenewWordle(input);

                if (wordle.Inputs[wordle.Inputs.Count - 1] == wordle.Word)
                {
                    correctWordle = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Herzlichen Glückwunsch!");
                    Console.WriteLine("Du hast gewonnen!");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
                }
            }

            if (!correctWordle)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Viel Glück beim nächsten Mal!");
                Console.WriteLine("Das Wort ist: " + wordle.Word);
                Console.ResetColor();
                Console.ReadKey();
            }

            new StartMenu();
        }

        private string InputOption()
        {
            string input;
            bool correctInput;

            do
            {
                Console.Write("Gebe ein Wort mit " + wordle.Word.Length + " Buchstaben ein: ");
                input = Console.ReadLine();
                correctInput = true;
                if (!IsValidateInput(input))
                {
                    correctInput = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FEHLER: Ungültiges Wort!");
                    Console.ResetColor();
                }
            } while (!correctInput);
            return input;
        }

        protected abstract bool IsValidateInput(string input);
    }
}
