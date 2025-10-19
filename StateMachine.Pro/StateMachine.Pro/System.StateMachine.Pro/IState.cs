#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface IState<TMachineUserData, TStateUserData> : IDisposable {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // Owner
        public object? Owner { get; }

        // Machine
        public IStateMachine<TMachineUserData, TStateUserData>? Machine { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public IState<TMachineUserData, TStateUserData> Root { get; }

        // Parent
        public IState<TMachineUserData, TStateUserData>? Parent { get; }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Ancestors { get; }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // UserData
        public TStateUserData UserData { get; }

    }
    public partial interface IState<TMachineUserData, TStateUserData> {

        // Attach
        protected internal void Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument);
        protected internal void Attach(IState<TMachineUserData, TStateUserData> parent, object? argument);

        // Detach
        protected internal void Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument);
        protected internal void Detach(IState<TMachineUserData, TStateUserData> parent, object? argument);

        // Activate
        protected internal void Activate(object? argument);

        // Deactivate
        protected internal void Deactivate(object? argument);

    }
}
