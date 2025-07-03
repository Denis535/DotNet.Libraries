namespace System.StateMachine.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StateMachine : IStateMachine<State> {

        // State
        State? IStateMachine<State>.State { get => this.State; set => this.State = value; }
        public State? State { get; private set; }

        // Constructor
        public StateMachine() {
        }

        // SetState
        public void SetState(State? state, object? argument, Action<State, object?>? callback) {
            IStateMachine<State>.SetState( this, state, argument, callback );
        }
        public void AddState(State state, object? argument) {
            IStateMachine<State>.AddState( this, state, argument );
        }
        public void RemoveState(State state, object? argument, Action<State, object?>? callback) {
            IStateMachine<State>.RemoveState( this, state, argument, callback );
        }
        public void RemoveState(object? argument, Action<State, object?>? callback) {
            IStateMachine<State>.RemoveState( this, argument, callback );
        }

    }
}
