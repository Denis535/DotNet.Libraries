namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class StateMachine : StateMachineBase<State> {

        // State
        public new State? State => base.State;

        // Constructor
        public StateMachine() {
        }

        // SetState
        public new void SetState(State? state, object? argument, Action<State, object?>? callback) {
            base.SetState( state, argument, callback );
        }
        public new void AddState(State state, object? argument) {
            base.AddState( state, argument );
        }
        public new void RemoveState(State state, object? argument, Action<State, object?>? callback) {
            base.RemoveState( state, argument, callback );
        }
        public new void RemoveState(object? argument, Action<State, object?>? callback) {
            base.RemoveState( argument, callback );
        }

    }
}
