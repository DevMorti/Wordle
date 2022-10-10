using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Menu
{
    class EasyGameMenu : GameMenu
    {
        public EasyGameMenu(string word) : base(word)
        {
        }

        protected override bool IsValidateInput(string input)
        {
            if (input.Length != wordle.Word.Length)
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
            //Hier sollte durch eine Api geprüft werden, ob das Wort existiert
            return true;
        }
    }
}
