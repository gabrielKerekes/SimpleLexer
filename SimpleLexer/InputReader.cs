using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleLexer
{
    public class InputReader
    {
        public static List<string> ReadInput(string fileName)
        {
            try
            { 
                using (var sr = new StreamReader(fileName))
                {
                    var lines = new List<string>();

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                    
                    return lines;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Chyba vstupného súboru");
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
