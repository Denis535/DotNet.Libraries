#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // IsDisposed
        bool IStateMachine<TMachineUserData, TStateUserData>.IsDisposed => this.IsDisposed;

        // UserData
        TMachineUserData IStateMachine<TMachineUserData, TStateUserData>.UserData => this.UserData;

        // OnDispose
        event Action? IStateMachine<TMachineUserData, TStateUserData>.OnDisposeCallback {
            add => this.OnDisposeCallback += value;
            remove => this.OnDisposeCallback -= value;
        }

        // Dispose
        void IStateMachine<TMachineUserData, TStateUserData>.Dispose() {
            this.Dispose();
        }

    }
    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // Root
        IState<TMachineUserData, TStateUserData>? IStateMachine<TMachineUserData, TStateUserData>.Root => this.Root;

    }
}
