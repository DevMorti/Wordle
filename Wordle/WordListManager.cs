using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    static class WordListManager
    {
        public static string GetRandomWord()
        {
            List<string> wordList = LoadWordList();
            Random random = new Random();
            return wordList[random.Next(wordList.Count)];
        }
        
        public static List<string> LoadWordList()
        { 
            if (!File.Exists(AppContext.BaseDirectory + "WordList.list"))
            {
                throw new FileNotFoundException("Die Wörterliste konnte nicht gefunden werden!");
            }

            List<string> wordList = new List<string>();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(AppContext.BaseDirectory + "WordList.list", FileMode.Open))
            {
                wordList = (List<string>)binaryFormatter.Deserialize(stream);
            }
            return wordList;
        }

        public static void SaveList(List<string> wordList)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(AppContext.BaseDirectory + "WordList.list", FileMode.Create))
                {
                    binaryFormatter.Serialize(stream, wordList);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine("Ein Fehler ist aufgetreten: " +
                    "Die WörterListe konnte nicht fehlerfrei gespeichert werden.");
                Console.ResetColor();
            }
        }
    }
}
