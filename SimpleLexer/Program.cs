using System;
using System.Collections.Generic;
using SimpleLexer.Automata;

namespace SimpleLexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var states = new List<State>();
            var q0 = new State("q0");
            var q1 = new State("q1");
            var q2 = new State("q2");
            var q3 = new State("q3");
            states.Add(q0);
            states.Add(q1);
            states.Add(q2);
            states.Add(q3);

            var symbols = new List<char>();
            var symbol1 = 'a';
            var symbol2 = 'b';
            symbols.Add(symbol1);
            symbols.Add(symbol2);

            var transitions = new List<Transition>();
            var transition1 = new Transition(q0, symbol1, q1);
            var transition2 = new Transition(q0, symbol2, q2);
            var transition3 = new Transition(q1, symbol1, q1);
            var transition4 = new Transition(q1, symbol2, q3);
            var transition5 = new Transition(q2, symbol1, q2);
            var transition6 = new Transition(q2, symbol2, q3);
            transitions.Add(transition1);
            transitions.Add(transition2);
            transitions.Add(transition3);
            transitions.Add(transition4);
            transitions.Add(transition5);
            transitions.Add(transition6);

            var startState = q0;
            var acceptStates = new List<State> { q3 };

            var automaton = new Automaton(states, symbols, transitions, startState, acceptStates);
            automaton.PrintTransitionTable();

            Console.WriteLine();

            automaton.ProcessInput("baaaaaba");

            Console.WriteLine();
        }
    }
}
