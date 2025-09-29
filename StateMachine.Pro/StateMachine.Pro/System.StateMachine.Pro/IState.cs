#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface IState<TMachineUserData, TStateUserData> {

        // IsDisposed
        public bool IsDisposed { get; }

        // UserData
        public TStateUserData UserData { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // Dispose
        internal void Dispose();

    }
    public partial interface IState<TMachineUserData, TStateUserData> {

        // Machine
        public IStateMachine<TMachineUserData, TStateUserData>? Machine { get; }
        internal IStateMachine<TMachineUserData, TStateUserData>? Machine_NoRecursive { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public IState<TMachineUserData, TStateUserData> Root { get; }

        // Parent
        public IState<TMachineUserData, TStateUserData>? Parent { get; }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Ancestors { get; }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Children { get; }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Descendants { get; }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> DescendantsAndSelf { get; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

    }
    public partial interface IState<TMachineUserData, TStateUserData> {

        // Attach
        internal void Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument);
        internal void Attach(IState<TMachineUserData, TStateUserData> parent, object? argument);

        // Detach
        internal void Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument);
        internal void Detach(IState<TMachineUserData, TStateUserData> parent, object? argument);

        // Activate
        internal void Activate(object? argument);

        // Deactivate
        internal void Deactivate(object? argument);

        // OnAttach
        internal void OnAttach(object? argument);
        internal void OnBeforeAttach(object? argument);
        internal void OnAfterAttach(object? argument);

        // OnDetach
        internal void OnDetach(object? argument);
        internal void OnBeforeDetach(object? argument);
        internal void OnAfterDetach(object? argument);

        // OnActivate
        internal void OnActivate(object? argument);
        internal void OnBeforeActivate(object? argument);
        internal void OnAfterActivate(object? argument);

        // OnDeactivate
        internal void OnDeactivate(object? argument);
        internal void OnBeforeDeactivate(object? argument);
        internal void OnAfterDeactivate(object? argument);

    }
}
