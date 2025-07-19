#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class StateBase<TThis> : IStateBase<TThis> where TThis : notnull, StateBase<TThis> {

        object? IStateBase<TThis>.Owner => this.Owner;

        IStateMachine<TThis>? IStateBase<TThis>.Machine_NoRecursive => this.Machine_NoRecursive;

        void IStateBase<TThis>.Attach(IStateMachine<TThis> machine, object? argument) {
            this.Attach( machine, argument );
        }

        void IStateBase<TThis>.Detach(IStateMachine<TThis> machine, object? argument) {
            this.Detach( machine, argument );
        }

        void IStateBase<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IStateBase<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IStateBase<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void IStateBase<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IStateBase<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IStateBase<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        void IStateBase<TThis>.Activate(object? argument) {
            this.Activate( argument );
        }

        void IStateBase<TThis>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        void IStateBase<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IStateBase<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IStateBase<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void IStateBase<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IStateBase<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IStateBase<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // Owner
        private object? Owner { get; set; }

        // Machine
        public IStateMachine<TThis>? Machine => (this.Owner as IStateMachine<TThis>) ?? (this.Owner as StateBase<TThis>)?.Machine;
        private IStateMachine<TThis>? Machine_NoRecursive => this.Owner as IStateMachine<TThis>;

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => this.Parent == null;
        public TThis Root => this.Parent?.Root ?? (TThis) this;

        // Parent
        public TThis? Parent => this.Owner as TThis;
        public IEnumerable<TThis> Ancestors {
            get {
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<TThis> AncestorsAndSelf => this.Ancestors.Prepend( (TThis) this );

        // Activity
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Child
        public TThis? Child { get; private set; }
        public IEnumerable<TThis> Descendants {
            get {
                if (this.Child != null) {
                    yield return this.Child;
                    foreach (var i in this.Child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<TThis> DescendantsAndSelf => this.Descendants.Prepend( (TThis) this );

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

        // Attach
        internal void Attach(IStateMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            {
                this.Owner = machine;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            {
                this.Activate( argument );
            }
        }
        internal void Attach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (parent.Activity is Activity.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal void Detach(IStateMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have {machine} machine", this.Machine_NoRecursive == machine );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }
        internal void Detach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"State {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity is Activity.Active) {
                Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
            this.OnBeforeAttachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            this.OnAfterAttachCallback?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            this.OnBeforeDetachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            this.OnAfterDetachCallback?.Invoke( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            this.OnBeforeActivate( argument );
            this.Activity = Activity.Activating;
            {
                this.OnActivate( argument );
                if (this.Child != null) {
                    this.Child.Activate( argument );
                }
            }
            this.Activity = Activity.Active;
            this.OnAfterActivate( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            this.OnBeforeDeactivate( argument );
            this.Activity = Activity.Deactivating;
            {
                if (this.Child != null) {
                    this.Child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
            }
            this.Activity = Activity.Inactive;
            this.OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            this.OnBeforeActivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateCallback?.Invoke( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // SetChild
        protected virtual void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback) {
            if (this.Child != null) {
                this.RemoveChild( this.Child, argument, callback );
            }
            if (child != null) {
                this.AddChild( child, argument );
            }
        }

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"State {this} must have no {this.Child} child", this.Child == null );
            this.Child = child;
            this.Child.Attach( (TThis) this, argument );
        }

        // RemoveChild
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            }
            Assert.Operation.Valid( $"State {this} must have {child} child", this.Child == child );
            this.Child.Detach( (TThis) this, argument );
            this.Child = null;
            callback?.Invoke( child, argument );
        }
        protected void RemoveChild(object? argument, Action<TThis, object?>? callback) {
            Assert.Operation.Valid( $"State {this} must have child", this.Child != null );
            this.RemoveChild( this.Child, argument, callback );
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            if (this.Parent != null) {
                this.Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Assert.Operation.Valid( $"State {this} must have machine", this.Machine_NoRecursive != null );
                this.Machine_NoRecursive.RemoveState( (TThis) this, argument, callback );
            }
        }

    }
}
