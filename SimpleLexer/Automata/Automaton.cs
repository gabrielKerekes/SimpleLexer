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
                Console.WriteLine("WORD '" + input + "' NOT accepted!");
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
                    var transitions = automaton.Transitions.Where(t => t.FromState == state && t.Symbol == c).ToList();
                    int i = 0;
                    var sb = new StringBuilder();
                    var transitionsCount = transitions.Count;
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

        public void Determinize()
        {
            for (int symbolsIndex = 0; symbolsIndex < Symbols.Count; symbolsIndex++)
            {
                for (int statesIndex = 0; statesIndex < States.Count; statesIndex++)
                {
                    var transitions = Transitions.Where(t => t.FromState == States[statesIndex] && t.Symbol == Symbols[symbolsIndex]).ToList();
                    if (transitions.Count == 0)
                        continue;

                    var toStates = transitions.Select(t => t.ToState).Distinct().ToList();
                    if (toStates.Count < 2)
                        continue;

                    var newState = CreateJoinedState(toStates);
                    if (newState == null)
                        continue;

                    if (!States.Contains(newState))
                        States.Add(newState);

                    if (AcceptStates.Any(toStates.Contains))
                        AcceptStates.Add(newState);

                    Transitions.Add(new Transition(States[statesIndex], Symbols[symbolsIndex], newState));

                    Transitions.RemoveAll(transitions.Contains);

                    symbolsIndex = -1;
                    break;
                }
            }

            Minimalize();
            Minimalize();
        }

        private State CreateJoinedState(List<State> states)
        {
            var newState = new JoinedState(states);

            foreach (var state in states)
            {
                var stateTransitions = Transitions.Where(t => t.FromState == state).ToList();

                for (int i = 0; i < stateTransitions.Count; i++)
                {
                    var iTransition = stateTransitions[i];

                    if (Transitions.Any(t =>
                                t.ToState == iTransition.ToState 
                                && t.Symbol == iTransition.Symbol 
                                && t.FromState == newState))
                        continue;

                    Transitions.Add(new Transition(newState, iTransition.Symbol, iTransition.ToState));
                }
            }

            return newState;
        }

        // todo: GABO - maybe finish
        private void Minimalize()
        {
            RemoveUnreachableStates();
        }

        private void RemoveUnreachableStates()
        {
            var unreachableStates = new List<State>();
            foreach (var state in States.Where(s => s != StartState))
            {
                if (Transitions.Any(t => t.ToState == state))
                    continue;

                unreachableStates.Add(state);
                AcceptStates.Remove(state);
            }

            States.RemoveAll(unreachableStates.Contains);
        }
    }
}
