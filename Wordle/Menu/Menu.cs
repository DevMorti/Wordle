using System;

namespace Wordle.Menu
{
    abstract class Menu
    {
        public Menu()
        {
            Console.Clear();
            DisplayMenu();
        }

        protected abstract void DisplayMenu();
    }
}
