#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class StateMachine<TState, TUserData> : StateMachineBase<TState> where TState : class, IState {

        // State
        public new TState? State => base.State;

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public StateMachine(TUserData userData) {
            this.UserData = userData;
        }

        // SetState
        public new void SetState(TState? state, object? argument, Action<TState, object?>? callback) {
            base.SetState( state, argument, callback );
        }

    }
}
