using System;
using SimpleLexer.Automata;

namespace SimpleLexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var languageFileName = args[0];
            var inputFileName = args[1];

            var languageWords = LanguageReader.ReadLanguage(languageFileName);
            if (languageWords == null)
                return;

            var inputLines = InputReader.ReadInput(inputFileName);
            if (inputLines == null)
                return;

            var automaton = AutomatonBuilder.BuildAutomatonFromLanguageWords(languageWords);
            Determinizer.Determinize(automaton);

            var i = 1;
            foreach (var line in inputLines)
            {
                var processLineResult = automaton.ProcessLine(line);
                if (processLineResult != -1)
                {
                    Console.WriteLine($"ERROR: LEXIKÁLNA CHYBA, r. {i}, pozícia {processLineResult}");
                    break;
                }

                i++;
            }
        }
    }
}
