#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateMachine<TMachineUserData, TStateUserData> {

        // IsDisposed
        public bool IsDisposed { get; }

        // Root
        public IState<TMachineUserData, TStateUserData>? Root { get; }

        // UserData
        public TMachineUserData UserData { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // Dispose
        internal void Dispose();

    }
}
