using System.Collections.Generic;
using System.Linq;

namespace SimpleLexer.Automata
{
    public class AutomatonBuilder
    {
        // builds an automaton for each word, each starting from the same startState
        public static Automaton BuildAutomatonFromLanguageWords(Dictionary<int, string> languageWords)
        {
            var states = new List<State>();
            var symbols = new List<char>();
            var transitions = new List<Transition>();
            var startState = new State("q0");
            var acceptStates = new List<State>();

            states.Add(startState);

            foreach (var kvp in languageWords)
            {
                var id = kvp.Key;
                var word = kvp.Value;

                for (var i = 0; i < word.Length; i++)
                {
                    var c = word[i];

                    if (!symbols.Contains(c))
                        symbols.Add(c);

                    var counter = GetStateNameCounter(states, id, c);

                    var newState = new State(State.BuildStateName(c, id, counter));
                    states.Add(newState);

                    transitions.Add(i == 0
                        ? new Transition(startState, c, newState)
                        : new Transition(states[states.Count - 2], c, newState));

                    if (i == word.Length - 1)
                        acceptStates.Add(newState);
                }
            }
            
            return new Automaton(states, symbols, transitions, startState, acceptStates)
            {
                LanguageWords = languageWords
            };
        }

        // so that you don't get same State names
        private static int GetStateNameCounter(List<State> states, int id, char c)
        {
            var counter = 0;
            while (states.Any(s => s.Name == State.BuildStateName(c, id, counter)))
            {
                counter++;
            }

            return counter;
        }
    }
}
