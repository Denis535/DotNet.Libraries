#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public partial interface IState<TThis> where TThis : class, IState<TThis> {

        // Owner
        protected object? Owner { get; set; }

        // Machine
        public sealed IStateMachine<TThis>? Machine => (this.Owner as IStateMachine<TThis>) ?? (this.Owner as IState<TThis>)?.Machine;
        protected internal sealed IStateMachine<TThis>? Machine_NoRecursive => this.Owner as IStateMachine<TThis>;

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public sealed bool IsRoot => this.Parent == null;
        public sealed TThis Root => this.Parent?.Root ?? (TThis) this;

        // Parent
        public sealed TThis? Parent {
            get {
                return this.Owner as TThis;
            }
        }
        public sealed IEnumerable<TThis> Ancestors {
            get {
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public sealed IEnumerable<TThis> AncestorsAndSelf {
            get {
                return this.Ancestors.Prepend( (TThis) this );
            }
        }

        // Activity
        public Activity Activity { get; protected set; }

        // Children
        public TThis? Child { get; protected set; }
        public sealed IEnumerable<TThis> Descendants {
            get {
                if (this.Child != null) {
                    yield return this.Child;
                    foreach (var i in this.Child.Descendants) yield return i;
                }
            }
        }
        public sealed IEnumerable<TThis> DescendantsAndSelf {
            get {
                return this.Descendants.Prepend( (TThis) this );
            }
        }

    }
    public partial interface IState<TThis> {

        // OnAttach
        public Action<object?>? OnBeforeAttachCallback { get; set; }
        public Action<object?>? OnAfterAttachCallback { get; set; }
        public Action<object?>? OnBeforeDetachCallback { get; set; }
        public Action<object?>? OnAfterDetachCallback { get; set; }

        // Attach
        internal sealed void Attach(IStateMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            {
                this.Owner = machine;
                {
                    this.OnBeforeAttachCallback?.Invoke( argument );
                    this.OnBeforeAttach( argument );
                }
                this.OnAttach( argument );
                {
                    this.OnAfterAttach( argument );
                    this.OnAfterAttachCallback?.Invoke( argument );
                }
            }
            {
                this.Activate( argument );
            }
        }
        private void Attach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            {
                this.Owner = parent;
                {
                    this.OnBeforeAttachCallback?.Invoke( argument );
                    this.OnBeforeAttach( argument );
                }
                this.OnAttach( argument );
                {
                    this.OnAfterAttach( argument );
                    this.OnAfterAttachCallback?.Invoke( argument );
                }
            }
            if (parent.Activity is Activity.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal sealed void Detach(IStateMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have {machine} machine", this.Machine_NoRecursive == machine );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                {
                    this.OnBeforeDetachCallback?.Invoke( argument );
                    this.OnBeforeDetach( argument );
                }
                this.OnDetach( argument );
                {
                    this.OnAfterDetach( argument );
                    this.OnAfterDetachCallback?.Invoke( argument );
                }
                this.Owner = null;
            }
        }
        private void Detach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"State {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity is Activity.Active) {
                Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            }
            {
                {
                    this.OnBeforeDetachCallback?.Invoke( argument );
                    this.OnBeforeDetach( argument );
                }
                this.OnDetach( argument );
                {
                    this.OnAfterDetach( argument );
                    this.OnAfterDetachCallback?.Invoke( argument );
                }
                this.Owner = null;
            }
        }

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
        public Action<object?>? OnBeforeActivateCallback { get; set; }
        public Action<object?>? OnAfterActivateCallback { get; set; }
        public Action<object?>? OnBeforeDeactivateCallback { get; set; }
        public Action<object?>? OnAfterDeactivateCallback { get; set; }

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            this.OnBeforeActivateCallback?.Invoke( argument );
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
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            this.OnBeforeDeactivateCallback?.Invoke( argument );
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
            this.OnAfterDeactivateCallback?.Invoke( argument );
        }

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

        // Helpers
        internal static void SetChild(TThis state, TThis? child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            if (state.Child != null) {
                state.RemoveChild( state.Child, argument, callback );
            }
            if (child != null) {
                state.AddChild( child, argument );
            }
        }
        // Helpers
        internal static void AddChild(TThis state, TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"State {state} must have no {state.Child} child", state.Child == null );
            state.Child = child;
            state.Child.Attach( state, argument );
        }
        // Helpers
        internal static void RemoveChild(TThis state, TThis child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {state} parent", child.Parent == state );
            if (state.Activity == Activity.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            }
            Assert.Operation.Valid( $"State {state} must have {child} child", state.Child == child );
            state.Child.Detach( state, argument );
            state.Child = null;
            callback?.Invoke( child, argument );
        }
        internal static void RemoveChild_(TThis state, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Operation.Valid( $"State {state} must have child", state.Child != null );
            state.RemoveChild( state.Child, argument, callback );
        }
        // Helpers
        internal static void RemoveSelf(TThis state, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            if (state.Parent != null) {
                state.Parent.RemoveChild( state, argument, callback );
            } else {
                Assert.Operation.Valid( $"State {state} must have machine", state.Machine_NoRecursive != null );
                state.Machine_NoRecursive.RemoveState( state, argument, callback );
            }
        }

    }
}
