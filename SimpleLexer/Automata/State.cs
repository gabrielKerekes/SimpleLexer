using System.Collections.Generic;

namespace SimpleLexer.Automata
{
    public class State
    {
        public string Name { get; set; }
        public List<Transition> Transitions { get; set; }

        public State(string name)
        {
            Name = name;
        }

        public State(string name, List<Transition> transitions)
        {
            Name = name;
            Transitions = transitions;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
