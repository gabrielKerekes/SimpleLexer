using System;
using System.Collections.Generic;
using SimpleLexer.Automata;

namespace SimpleLexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Test1();
            DeterminisationTest();
            DeterminisationTest2();
        }

        public static void Test1()
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

            automaton.ProcessInput("baaaab");

            Console.WriteLine();
        }

        public static void DeterminisationTest()
        {
            var states = new List<State>();
            var q0 = new State("q0");
            var q1 = new State("q1");
            var q2 = new State("q2");
            var q3 = new State("q3");
            var q4 = new State("q4");
            states.Add(q0);
            states.Add(q1);
            states.Add(q2);
            states.Add(q3);
            states.Add(q4);

            var symbols = new List<char>();
            var symbol1 = 'a';
            var symbol2 = 'b';
            symbols.Add(symbol1);
            symbols.Add(symbol2);

            var transitions = new List<Transition>();
            var transition1 = new Transition(q0, symbol1, q1);
            var transition2 = new Transition(q0, symbol1, q2);
            var transition3 = new Transition(q1, symbol1, q4);
            var transition4 = new Transition(q2, symbol2, q3);
            var transition5 = new Transition(q3, symbol1, q4);
            var transition6 = new Transition(q3, symbol2, q3);
            var transition7 = new Transition(q3, symbol2, q4);
            var transition8 = new Transition(q4, symbol1, q4);
            var transition9 = new Transition(q4, symbol2, q4);
            transitions.Add(transition1);
            transitions.Add(transition2);
            transitions.Add(transition3);
            transitions.Add(transition4);
            transitions.Add(transition5);
            transitions.Add(transition6);
            transitions.Add(transition7);
            transitions.Add(transition8);
            transitions.Add(transition9);

            var startState = q0;
            var acceptStates = new List<State> { q3, q4 };

            var automaton = new Automaton(states, symbols, transitions, startState, acceptStates);
            automaton.PrintTransitionTable();

            automaton.Determinize();

            Console.WriteLine();

            automaton.PrintTransitionTable();
        }

        public static void DeterminisationTest2()
        {
            var states = new List<State>();
            var a = new State("a");
            var b = new State("b");
            var c = new State("c");
            var d = new State("d");
            var e = new State("e");
            states.Add(a);
            states.Add(b);
            states.Add(c);
            states.Add(d);
            states.Add(e);

            var symbols = new List<char>();
            var symbol1 = '0';
            var symbol2 = '1';
            symbols.Add(symbol1);
            symbols.Add(symbol2);

            var transitions = new List<Transition>();
            var transition1 = new Transition(a, symbol1, a);
            var transition2 = new Transition(a, symbol1, b);
            var transition3 = new Transition(a, symbol1, c);
            var transition4 = new Transition(a, symbol1, d);
            var transition5 = new Transition(a, symbol1, e);
            var transition6 = new Transition(a, symbol2, d);
            var transition7 = new Transition(a, symbol2, e);
            var transition8 = new Transition(b, symbol1, c);
            var transition9 = new Transition(b, symbol2, e);
            var transition10 = new Transition(c, symbol2, b);
            var transition11 = new Transition(d, symbol1, e);

            transitions.Add(transition1);
            transitions.Add(transition2);
            transitions.Add(transition3);
            transitions.Add(transition4);
            transitions.Add(transition5);
            transitions.Add(transition6);
            transitions.Add(transition7);
            transitions.Add(transition8);
            transitions.Add(transition9);
            transitions.Add(transition10);
            transitions.Add(transition11);

            var startState = a;
            var acceptStates = new List<State> { e };

            var automaton = new Automaton(states, symbols, transitions, startState, acceptStates);
            automaton.PrintTransitionTable();

            //for (int i = 0; i < 10; i++)
            //{
                automaton.Determinize();

                Console.WriteLine();

                automaton.PrintTransitionTable();
            //}
        }

    }
}
