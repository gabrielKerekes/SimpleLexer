using System.Collections.Generic;
using System.Linq;

namespace SimpleLexer.Automata
{
    public class Determinizer
    {
        public static void Determinize(Automaton automaton)
        {
            for (var statesIndex = 0; statesIndex < automaton.States.Count; statesIndex++)
            {
                foreach (var currentSymbol in automaton.Symbols)
                {
                    var transitions = automaton.Transitions.Where(t => t.FromState == automaton.States[statesIndex] && t.Symbol == currentSymbol).ToList();
                    if (transitions.Count == 0)
                        continue;

                    var toStates = transitions.Select(t => t.ToState).Distinct().ToList();
                    if (toStates.Count < 2)
                        continue;

                    var newState = CreateJoinedState(automaton, toStates);
                    if (newState == null)
                        continue;

                    if (!automaton.States.Contains(newState))
                        automaton.States.Add(newState);

                    if (automaton.AcceptStates.Any(toStates.Contains))
                        automaton.AcceptStates.Add(newState);

                    automaton.Transitions.Add(new Transition(automaton.States[statesIndex], currentSymbol, newState));

                    automaton.Transitions.RemoveAll(transitions.Contains);

                    statesIndex = -1;
                    break;
                }
            }

            Minimalizer.Minimalize(automaton);
        }

        private static State CreateJoinedState(Automaton automaton, List<State> states)
        {
            var newState = new JoinedState(states);

            foreach (var state in states)
            {
                var stateTransitions = automaton.Transitions.Where(t => t.FromState == state).ToList();

                foreach (var transition in stateTransitions)
                {
                    if (automaton.Transitions.Any(t =>
                            t.ToState == transition.ToState
                            && t.Symbol == transition.Symbol
                            && t.FromState.Equals(newState)))
                    {
                        continue;
                    }

                    automaton.Transitions.Add(new Transition(newState, transition.Symbol, transition.ToState));
                }
            }

            return newState;
        }
    }
}
