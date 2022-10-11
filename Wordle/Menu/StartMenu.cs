using System;
using System.IO;
using Wordle;

namespace Wordle.Menu
{
    class StartMenu : Menu
    {
        protected override void DisplayMenu()
        {
            Console.WriteLine("### Wordle ###");
            Console.WriteLine("##############");
            Console.WriteLine();
            Console.WriteLine("Wähle eine Option aus:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] Einfaches Wordle");
            Console.WriteLine("[2] Schweres Wordle");
            Console.WriteLine("[3] Wörterliste erstellen");
            Console.ResetColor();

            InputOption();
        }

        private void InputOption()
        {
            string input;
            bool correctInput;

            do
            {
                Console.Write("Eingabe: ");
                input = Console.ReadLine();
                correctInput = false;

                switch (input)
                {
                    case "1":
                        try
                        {
                            new EasyGameMenu(WordListManager.GetRandomWord());
                        }
                        catch(FileNotFoundException ex)
                        {
                            new CreateWordlistMenu();
                        }
                        catch(Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            Console.ReadKey();
                            new StartMenu();
                        }
                        correctInput = true;
                        break;
                    case "2":
                        try
                        {
                            new DifficultGameMenu(WordListManager.GetRandomWord());
                        }
                        catch (FileNotFoundException ex)
                        {
                            new CreateWordlistMenu();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            Console.ReadKey();
                            new StartMenu();
                        }
                        correctInput = true;
                        break;
                    case "3":
                        new CreateWordlistMenu();
                        correctInput = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("FEHLER: Ungültige Eingabe!");
                        Console.ResetColor();
                        break;
                }
            } while (!correctInput);
        }
    }
}
