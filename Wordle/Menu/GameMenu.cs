using System;

namespace Wordle.Menu
{
    abstract class GameMenu
    {
        protected Wordle Wordle { get; set; }

        public GameMenu(string word)
        {
            Wordle = new Wordle(word);
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            bool correctWordle = false;

            Wordle.PrintWordle();
            while(Wordle.LastedTrys != 0)
            {
                Console.WriteLine();
                string input = InputOption();
                Wordle.RenewWordle(input);

                if (Wordle.Inputs[Wordle.Inputs.Count - 1] == Wordle.Word)
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
                Console.WriteLine("Das Wort ist: " + Wordle.Word);
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
                Console.Write("Gebe ein Wort mit " + Wordle.Word.Length + " Buchstaben ein: ");
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
