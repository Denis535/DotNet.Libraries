#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface IState<TMachineUserData, TStateUserData> {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // UserData
        public TStateUserData UserData { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // Dispose
        protected internal void Dispose();

    }
    public partial interface IState<TMachineUserData, TStateUserData> {

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
        protected internal void Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument);
        protected internal void Attach(IState<TMachineUserData, TStateUserData> parent, object? argument);

        // Detach
        protected internal void Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument);
        protected internal void Detach(IState<TMachineUserData, TStateUserData> parent, object? argument);

        // Activate
        protected internal void Activate(object? argument);

        // Deactivate
        protected internal void Deactivate(object? argument);

        // OnAttach
        protected internal void OnAttach(object? argument);
        protected internal void OnBeforeAttach(object? argument);
        protected internal void OnAfterAttach(object? argument);

        // OnDetach
        protected internal void OnDetach(object? argument);
        protected internal void OnBeforeDetach(object? argument);
        protected internal void OnAfterDetach(object? argument);

        // OnActivate
        protected internal void OnActivate(object? argument);
        protected internal void OnBeforeActivate(object? argument);
        protected internal void OnAfterActivate(object? argument);

        // OnDeactivate
        protected internal void OnDeactivate(object? argument);
        protected internal void OnBeforeDeactivate(object? argument);
        protected internal void OnAfterDeactivate(object? argument);

    }
}
