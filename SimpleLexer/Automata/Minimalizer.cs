using System.Collections.Generic;
using System.Linq;

namespace SimpleLexer.Automata
{
    public class Minimalizer
    {
        public static void Minimalize(Automaton automaton)
        {
            RemoveUnreachableStates(automaton);
        }

        private static void RemoveUnreachableStates(Automaton automaton)
        {
            var unreachableStates = new List<State>();
            foreach (var state in automaton.States.Where(s => s != automaton.StartState))
            {
                if (automaton.Transitions.Any(t => t.ToState == state))
                    continue;

                unreachableStates.Add(state);
                automaton.AcceptStates.Remove(state);
            }

            automaton.States.RemoveAll(unreachableStates.Contains);
        }
    }
}
