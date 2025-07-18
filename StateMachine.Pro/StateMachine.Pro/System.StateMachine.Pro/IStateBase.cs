#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface IStateBase<TThis> where TThis : class, IStateBase<TThis> {

        // Owner
        protected IStateMachine<TThis>? Owner { get; }

        // Machine
        public IStateMachine<TThis>? Machine { get; }

        // Activity
        public Activity Activity { get; }

    }
    public partial interface IStateBase<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Attach
        protected internal void Attach(IStateMachine<TThis> machine, object? argument);

        // Detach
        protected internal void Detach(IStateMachine<TThis> machine, object? argument);

        // OnAttach
        protected void OnAttach(object? argument);
        protected void OnBeforeAttach(object? argument);
        protected void OnAfterAttach(object? argument);

        // OnDetach
        protected void OnDetach(object? argument);
        protected void OnBeforeDetach(object? argument);
        protected void OnAfterDetach(object? argument);

    }
    public partial interface IStateBase<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Activate
        protected void Activate(object? argument);

        // Deactivate
        protected void Deactivate(object? argument);

        // OnActivate
        protected void OnActivate(object? argument);
        protected void OnBeforeActivate(object? argument);
        protected void OnAfterActivate(object? argument);

        // OnDeactivate
        protected void OnDeactivate(object? argument);
        protected void OnBeforeDeactivate(object? argument);
        protected void OnAfterDeactivate(object? argument);

    }
}
