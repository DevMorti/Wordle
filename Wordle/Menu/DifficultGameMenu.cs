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
            if (!CouldBeWord(input))
                return false;
            //Hier sollte durch eine Api geprüft werden, ob das Wort existiert
            return true;
        }

        private bool CouldBeWord(string lastInput)
        {
            foreach (string input in Wordle.Inputs)
            {
                Dictionary<char, int> comparedChars = Wordle.CompareCountedCharsWithWord(input);

                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == Wordle.Word[i])
                    {
                        if (lastInput[i] != Wordle.Word[i])
                            return false;
                    }
                    else if (Wordle.Word.Contains(input[i]))//Word enthält Word, Stelle ist Benutzer bekannt
                    {
                        comparedChars[input[i]]--;
                        if(!lastInput.Contains(input[i]) || lastInput[i] == input[i])//aktueller Input: enthält, nicht an dieser Stelle
                            return false;
                    }
                    else if (lastInput.Contains(input[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
