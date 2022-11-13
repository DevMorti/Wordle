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

        protected override async Task<bool> IsValidateInput(string input)
        {
            Task<bool> isWord = Task.Run(() => Wiktionary.wiktionary.IsWordOrNoConnection(input));
            if (input.Length != Wordle.Word.Length)
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
            await isWord;
            return isWord.Result;
        }
    }
}
