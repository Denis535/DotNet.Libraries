#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface IState<TThis> where TThis : class, IState<TThis> {

        // Owner
        protected object? Owner { get; }

        // Machine
        public IStateMachine<TThis>? Machine { get; }
        protected internal IStateMachine<TThis>? Machine_NoRecursive { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public TThis Root { get; }

        // Parent
        public TThis? Parent { get; }
        public IEnumerable<TThis> Ancestors { get; }
        public IEnumerable<TThis> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public TThis? Child { get; }
        public IEnumerable<TThis> Descendants { get; }
        public IEnumerable<TThis> DescendantsAndSelf { get; }

    }
    public partial interface IState<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Attach
        protected internal void Attach(IStateMachine<TThis> machine, object? argument);
        protected void Attach(TThis parent, object? argument);

        // Detach
        protected internal void Detach(IStateMachine<TThis> machine, object? argument);
        protected void Detach(TThis parent, object? argument);

        // OnAttach
        protected void OnAttach(object? argument);
        protected void OnBeforeAttach(object? argument);
        protected void OnAfterAttach(object? argument);

        // OnDetach
        protected void OnDetach(object? argument);
        protected void OnBeforeDetach(object? argument);
        protected void OnAfterDetach(object? argument);

    }
    public partial interface IState<TThis> {

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
    public partial interface IState<TThis> {

        // SetChild
        protected void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback);

        // AddChild
        protected void AddChild(TThis child, object? argument);

        // RemoveChild
        protected void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
        protected void RemoveChild(object? argument, Action<TThis, object?>? callback);

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

    }
}
