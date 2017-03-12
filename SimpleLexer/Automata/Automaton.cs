using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLexer.Automata
{
    public class Automaton
    {
        public List<State> States{ get; set; }
        public List<char> Symbols { get; set; }
        public List<Transition> Transitions { get; set; }
        public State StartState { get; set; }
        public List<State> AcceptStates { get; set; }

        private State currentState;
        private string currentInput;

        public Automaton(List<State> states, List<char> symbols, List<Transition> transitions, State startState, List<State> acceptStates)
        {
            // todo: mozno error handling na prazdne listy .. 

            States = states;
            Symbols = symbols;
            Transitions = transitions;
            StartState = startState;
            AcceptStates = acceptStates;

            currentState = states[0];
            currentInput = "";
        }

        public void ProcessInput(string input)
        {
            currentInput = input;
            Console.Write("({0}, {1}) ", currentState, input);

            int i;
            for (i = 0; i < currentInput.Length; i++)
            {
                var currentChar = currentInput[i];
                var transition = Transitions.FirstOrDefault(t => t.FromState == currentState && t.Symbol == currentChar);

                if (transition == null)
                {
                    // todo: EXCEPTION - automaton stuck
                    //return;
                    break;
                }
                if (i != 0)
                    Console.Write("|- ({0}, {1}) ", currentState, input.Substring(i));

                currentState = transition.ToState;
            }

            Console.Write("|- ({0}, {1}) ", currentState, i == currentInput.Length ? "--" : input.Substring(i));

            Console.WriteLine();
            Console.WriteLine();

            if (AcceptStates.Contains(currentState) && i == currentInput.Length)
                Console.WriteLine("WORD '" + input + "' accepted!");
            else
                Console.WriteLine("WORD '" + input + "' refused!");
        }

        public void PrintTransitionTable()
        {
            PrintTransitionTable(this);
        }

        // todo: refactor
        public static void PrintTransitionTable(Automaton automaton)
        {
            Console.Write("{0, 10}", "");
            foreach (var c in automaton.Symbols)
            {
                Console.Write("{0, 20}", c);
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------");

            foreach (var state in automaton.States)
            {
                Console.Write("{0, 10}", state.Name);
                foreach (var c in automaton.Symbols)
                {
                    var transitions = automaton.Transitions.Where(t => t.FromState == state && t.Symbol == c);
                    int i = 0;
                    StringBuilder sb = new StringBuilder();
                    var transitionsCount = transitions.Count();
                    foreach (var transition in transitions)
                    {
                        sb.Append(transition.ToState.Name);
                        if (i != transitionsCount - 1)
                            sb.Append(",");
                        i++;
                    }
                    if (transitionsCount == 0)
                        sb.Append("----");

                    Console.Write("{0, 20}", sb);
                }

                Console.WriteLine();
            }
        }
    }
}
