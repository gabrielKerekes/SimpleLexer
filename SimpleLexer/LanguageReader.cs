using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleLexer
{
    public class LanguageReader
    {
        public static Dictionary<int, string> ReadLanguage(string fileName)
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    var file = sr.ReadToEnd();

                    var words = file.Split(new[] { "\r\n", "\n", " "}, StringSplitOptions.None);

                    var languageWords = new Dictionary<int, string>();
                    for (var i = 0; i < words.Length; i += 2)
                    {
                        var wordId = int.Parse(words[i + 1]);
                        var word = words[i];
                        languageWords[wordId] = word;
                    }

                    return languageWords;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Chyba jazykového súboru");
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
