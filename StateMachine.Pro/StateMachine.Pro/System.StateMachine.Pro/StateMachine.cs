#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class StateMachine<T, TUserData> : StateMachineBase<T> where T : class, IState {

        // State
        public new IState? State => base.State;

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public StateMachine(TUserData userData) {
            this.UserData = userData;
        }

        // SetState
        public new void SetState(T? state, object? argument, Action<T, object?>? callback) {
            base.SetState( state, argument, callback );
        }

    }
}
