using System;
using System.Collections.Generic;
using System.Linq;

namespace AfjZadanie2
{
    public class State
    {
        public string Name { get; set; }
        public List<Transition> Transitions { get; set; }
    }

    public class Transition
    {
        public State FromState { get; set; }
        public char OnChar { get; set; }
        public State ToState { get; set; }
    }

    public class Automaton
    {
        public List<State> States{ get; set; }
        public List<char> Symbols { get; set; }
        public List<Transition> Transitions { get; set; }
        public State StartState { get; set; }
        public List<State> AcceptStates { get; set; }

        public Automaton(List<State> states, List<char> symbols, List<Transition> transitions, State startState, List<State> acceptStates)
        {
            States = states;
            Symbols = symbols;
            Transitions = transitions;
            StartState = startState;
            AcceptStates = acceptStates;

            //States = new List<string> { "a", "b", "c", "d", "e" };
            //Transitions = new List<Transition>();

            //Transitions.Add(new Transition { FromState = "a", ToState = "b", OnChar = 'a' });
        }

        public void PrintAutomatonTable()
        {
            foreach (var c in Characters)
            {
                Console.Write(c + "     ");
            }

            Console.WriteLine();

            foreach (var state in States)
            {
                Console.Write(state.Name);
                Console.Write("     ");
                foreach (var c in Characters)
                {
                    var transitions = state.Transitions.Where(t => t.OnChar == c);
                    foreach (var transition in transitions)
                    {
                        Console.Write(transition.ToState);
                        Console.Write(",");
                    }
                    Console.Write("      ");
                }

                Console.WriteLine();
            }
        }
    }
}
