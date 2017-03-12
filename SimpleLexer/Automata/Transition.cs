namespace SimpleLexer.Automata
{
    public class Transition
    {
        public State FromState { get; set; }
        public char Symbol { get; set; }
        public State ToState { get; set; }

        public Transition(State fromState, char symbol, State toState)
        {
            FromState = fromState;
            Symbol = symbol;
            ToState = toState;
        }
    }
}
