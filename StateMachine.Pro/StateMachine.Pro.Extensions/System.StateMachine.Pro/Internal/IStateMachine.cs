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

    }
    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // Dispose
        void IStateMachine<TMachineUserData, TStateUserData>.Dispose() {
            this.Dispose();
        }

    }
}
