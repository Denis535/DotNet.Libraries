#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface IStateMachine<TMachineUserData, TStateUserData> {

        // IsDisposed
        public bool IsDisposed { get; }

        // UserData
        public TMachineUserData UserData { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // Dispose
        internal void Dispose();

    }
    public partial interface IStateMachine<TMachineUserData, TStateUserData> {

        // Root
        public IState<TMachineUserData, TStateUserData>? Root { get; }

    }
}
