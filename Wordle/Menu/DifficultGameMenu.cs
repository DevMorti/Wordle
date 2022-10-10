using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Menu
{
    class DifficultGameMenu : GameMenu
    {
        public DifficultGameMenu(string word) : base(word)
        {
        }

        protected override bool IsValidateInput(string input)
        {
            if (input.Length != wordle.Word.Length)
            {
                return false;
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    return false;
                }
            }
            if (!CouldBeWord(input))
                return false;
            //Hier sollte durch eine Api geprüft werden, ob das Wort existiert
            return true;
        }

        private bool CouldBeWord(string input)
        {
            
        }
    }
}
