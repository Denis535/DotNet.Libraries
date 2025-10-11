#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // IsDisposed
        bool IStateMachine<TMachineUserData, TStateUserData>.IsDisposing => this.IsDisposing;
        bool IStateMachine<TMachineUserData, TStateUserData>.IsDisposed => this.IsDisposed;

        // Root
        IState<TMachineUserData, TStateUserData>? IStateMachine<TMachineUserData, TStateUserData>.Root => this.Root;

        // UserData
        TMachineUserData IStateMachine<TMachineUserData, TStateUserData>.UserData => this.UserData;

        // OnDispose
        event Action? IStateMachine<TMachineUserData, TStateUserData>.OnBeforeDisposeCallback {
            add => this.OnBeforeDisposeCallback += value;
            remove => this.OnBeforeDisposeCallback -= value;
        }
        event Action? IStateMachine<TMachineUserData, TStateUserData>.OnAfterDisposeCallback {
            add => this.OnAfterDisposeCallback += value;
            remove => this.OnAfterDisposeCallback -= value;
        }

    }
    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // Dispose
        void IStateMachine<TMachineUserData, TStateUserData>.Dispose() {
            this.Dispose();
        }

    }
}
