#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface IStateMachine<TMachineUserData, TStateUserData> {

        // IsDisposed
        public bool IsDisposed { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // UserData
        public TMachineUserData UserData { get; }

        // Dispose
        internal void Dispose();

    }
    public partial interface IStateMachine<TMachineUserData, TStateUserData> {

        // Root
        public IState<TMachineUserData, TStateUserData>? Root { get; }

    }
}
