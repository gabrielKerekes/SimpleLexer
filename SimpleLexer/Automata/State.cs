using System.Collections.Generic;
using System.Linq;

namespace SimpleLexer.Automata
{
    public class State
    {
        public string Name { get; set; }

        public State(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static string BuildStateName(char c, int wordId, int i)
        {
            return (c + "") + (wordId + "") + (i + "");
        }
    }

    public class JoinedState : State
    {
        public List<State> States;

        public JoinedState(List<State> states) : base("")
        {
            States = states;

            Name = ToString();
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", States) + "]";
        }

        //public override bool Equals(object obj)
        //{
        //    var secondJoinedState = obj as JoinedState;
        //    if (secondJoinedState == null)
        //        return false;

        //    return States.All(secondJoinedState.States.Contains)
        //           && secondJoinedState.States.All(States.Contains);
        //}
    }
}
