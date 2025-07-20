#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class State<TThis, TUserData> : StateBase<TThis> where TThis : notnull, StateBase<TThis> {

        // UserData
        public TUserData UserData { get; private set; }

        // Constructor
        public State(TUserData userData) {
            this.UserData = userData;
        }

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
}
