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
            {   // Open the text file using a stream reader.
                using (var sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    var file = sr.ReadToEnd();

                    var words = file.Split(new string[] { "\r\n", "\n", " "}, StringSplitOptions.None);

                    var languageWords = new Dictionary<int, string>();
                    for (int i = 0; i < words.Length; i += 2)
                    {
                        languageWords[int.Parse(words[i + 1])] = words[i];
                    }

                    return languageWords;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
