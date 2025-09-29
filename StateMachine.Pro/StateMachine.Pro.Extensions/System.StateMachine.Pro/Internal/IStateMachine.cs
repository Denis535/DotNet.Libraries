#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // Root
        IState<TMachineUserData, TStateUserData>? IStateMachine<TMachineUserData, TStateUserData>.Root => this.Root;

        // UserData
        TMachineUserData IStateMachine<TMachineUserData, TStateUserData>.UserData => this.UserData;

    }
}
