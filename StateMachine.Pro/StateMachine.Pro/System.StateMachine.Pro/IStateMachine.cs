#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface IStateMachine<TMachineUserData, TStateUserData> {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // Root
        public IState<TMachineUserData, TStateUserData>? Root { get; }

        // UserData
        public TMachineUserData UserData { get; }

    }
    public partial interface IStateMachine<TMachineUserData, TStateUserData> {

        // Dispose
        protected internal void Dispose();

    }
}
