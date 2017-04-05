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
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
