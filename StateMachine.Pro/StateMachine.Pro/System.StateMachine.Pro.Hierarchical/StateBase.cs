#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public abstract partial class StateBase<TThis> : IState<TThis> where TThis : notnull, StateBase<TThis> {

        object? IState<TThis>.Owner { get => this.Owner; set => this.Owner = value; }

        Activity IState<TThis>.Activity { get => this.Activity; set => this.Activity = value; }

        TThis? IState<TThis>.Child { get => this.Child; set => this.Child = value; }

    }
    public abstract partial class StateBase<TThis> {

        Action<object?>? IState<TThis>.OnBeforeAttachCallback {
            get => this.OnBeforeAttachCallback;
            set => this.OnBeforeAttachCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterAttachCallback {
            get => this.OnAfterAttachCallback;
            set => this.OnAfterAttachCallback = value;
        }
        Action<object?>? IState<TThis>.OnBeforeDetachCallback {
            get => this.OnBeforeDetachCallback;
            set => this.OnBeforeDetachCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterDetachCallback {
            get => this.OnAfterDetachCallback;
            set => this.OnAfterDetachCallback = value;
        }

        void IState<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IState<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IState<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void IState<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IState<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IState<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        Action<object?>? IState<TThis>.OnBeforeActivateCallback {
            get => this.OnBeforeActivateCallback;
            set => this.OnBeforeActivateCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterActivateCallback {
            get => this.OnAfterActivateCallback;
            set => this.OnAfterActivateCallback = value;
        }
        Action<object?>? IState<TThis>.OnBeforeDeactivateCallback {
            get => this.OnBeforeDeactivateCallback;
            set => this.OnBeforeDeactivateCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterDeactivateCallback {
            get => this.OnAfterDeactivateCallback;
            set => this.OnAfterDeactivateCallback = value;
        }

        void IState<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IState<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IState<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void IState<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IState<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IState<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        void IState<TThis>.SetChild(TThis? child, object? argument, Action<TThis, object?>? callback) {
            this.SetChild( child, argument, callback );
        }
        void IState<TThis>.AddChild(TThis child, object? argument) {
            this.AddChild( child, argument );
        }
        void IState<TThis>.RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            this.RemoveChild( child, argument, callback );
        }
        void IState<TThis>.RemoveChild(object? argument, Action<TThis, object?>? callback) {
            this.RemoveChild( argument, callback );
        }
        void IState<TThis>.RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            this.RemoveSelf( argument, callback );
        }

    }
    public abstract partial class StateBase<TThis> {

        // Owner
        private object? Owner { get; set; }

        // Machine
        public IStateMachine<TThis>? Machine => ((IState<TThis>) this).Machine;
        private IStateMachine<TThis>? Machine_NoRecursive => ((IState<TThis>) this).Machine_NoRecursive;

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => ((IState<TThis>) this).IsRoot;
        public TThis Root => ((IState<TThis>) this).Root;

        // Parent
        public TThis? Parent => ((IState<TThis>) this).Parent;
        public IEnumerable<TThis> Ancestors => ((IState<TThis>) this).Ancestors;
        public IEnumerable<TThis> AncestorsAndSelf => ((IState<TThis>) this).AncestorsAndSelf;

        // Activity
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Child
        public TThis? Child { get; private set; }
        public IEnumerable<TThis> Descendants => ((IState<TThis>) this).Descendants;
        public IEnumerable<TThis> DescendantsAndSelf => ((IState<TThis>) this).DescendantsAndSelf;

        // Constructor
        public StateBase() {
        }

    }
    public abstract partial class StateBase<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
        }
        protected virtual void OnAfterAttach(object? argument) {
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
        }
        protected virtual void OnAfterDetach(object? argument) {
        }

    }
    public abstract partial class StateBase<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
        }
        protected virtual void OnAfterActivate(object? argument) {
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
        }
        protected virtual void OnAfterDeactivate(object? argument) {
        }

    }
    public abstract partial class StateBase<TThis> {

        // SetChild
        protected virtual void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback) {
            IState<TThis>.SetChild( (TThis) this, child, argument, callback );
        }

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            IState<TThis>.AddChild( (TThis) this, child, argument );
        }

        // RemoveChild
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            IState<TThis>.RemoveChild( (TThis) this, child, argument, callback );
        }
        protected void RemoveChild(object? argument, Action<TThis, object?>? callback) {
            IState<TThis>.RemoveChild_( (TThis) this, argument, callback );
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            IState<TThis>.RemoveSelf( (TThis) this, argument, callback );
        }

    }
}
