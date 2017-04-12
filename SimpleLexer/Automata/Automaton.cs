using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLexer.Automata
{
    public class Automaton
    {
        public List<State> States { get; set; }
        public List<char> Symbols { get; set; }
        public List<Transition> Transitions { get; set; }
        public State StartState { get; set; }
        public List<State> AcceptStates { get; set; }
        public Dictionary<int, string> LanguageWords { get; set; }

        private State currentState;
        private string currentInput;

        public Automaton(List<State> states, List<char> symbols, List<Transition> transitions, State startState,
            List<State> acceptStates)
        {
            States = states;
            Symbols = symbols;
            Transitions = transitions;
            StartState = startState;
            AcceptStates = acceptStates;
            LanguageWords = new Dictionary<int, string>();

            currentState = StartState;
            currentInput = "";
        }

        public int ProcessLine(string line)
        {
            currentState = StartState;
            var tempState = currentState;

            currentInput = line;

            int i;
            var tempI = 0;
            var currentWord = "";
            var tempCurrentWord = "";
            for (i = 0; i < currentInput.Length; i++)
            {
                var currentChar = currentInput[i];
                if (char.IsWhiteSpace(currentChar))
                    continue;

                if (AcceptStates.Contains(currentState))
                {
                    tempState = currentState;
                    tempI = i;
                    tempCurrentWord = currentWord;
                }

                var transition = Transitions.FirstOrDefault(t => t.FromState == currentState && t.Symbol == currentChar);
                if (transition == null)
                {
                    if (AcceptStates.Contains(currentState))
                    {
                        currentState = StartState;
                        i--;
                        tempState = null;
                        Console.WriteLine(currentWord + " " + LanguageWords.FirstOrDefault(kvp => kvp.Value == currentWord).Key);
                        currentWord = "";
                        continue;
                    }

                    if (tempState == null || tempState == StartState)
                        return i;

                    currentState = tempState;
                    currentWord = tempCurrentWord;
                    i = tempI - 1;
                    i--;
                    tempState = null;
                    continue;
                }

                currentState = transition.ToState;
                currentWord += currentChar;
            }

            if (!AcceptStates.Contains(currentState))
            {
                return i;
            }

            Console.WriteLine(currentWord + " " + LanguageWords.FirstOrDefault(kvp => kvp.Value == currentWord).Key);
            return -1;
        }
    }
}
