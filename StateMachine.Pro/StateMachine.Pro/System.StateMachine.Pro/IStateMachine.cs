#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateMachine<TMachineUserData, TStateUserData> : IDisposable {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // Root
        public IState<TMachineUserData, TStateUserData>? Root { get; }

        // UserData
        public TMachineUserData UserData { get; }

    }
}
