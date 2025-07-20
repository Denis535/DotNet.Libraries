#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

        // Owner
        private object? Owner { get; set; }

        // Machine
        public TreeMachineBase<TThis>? Machine => (this.Owner as TreeMachineBase<TThis>) ?? (this.Owner as NodeBase<TThis>)?.Machine;
        internal TreeMachineBase<TThis>? Machine_NoRecursive => this.Owner as TreeMachineBase<TThis>;

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

        // Children
        private List<TThis> Children_Mutable { get; } = new List<TThis>( 0 );
        public IReadOnlyList<TThis> Children => this.Children_Mutable;
        public IEnumerable<TThis> Descendants {
            get {
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<TThis> DescendantsAndSelf => this.Descendants.Prepend( (TThis) this );

        // Constructor
        public NodeBase() {
        }

    }
    public abstract partial class NodeBase<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Attach
        internal void Attach(TreeMachineBase<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.Owner = machine;
                foreach (var ancestor in this.Ancestors.Reverse().OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnBeforeDescendantAttachCallback?.Invoke( (TThis) this, argument );
                    ancestor.OnBeforeDescendantAttach( (TThis) this, argument );
                }
                {
                    this.OnBeforeAttachCallback?.Invoke( argument );
                    this.OnBeforeAttach( argument );
                }
                this.OnAttach( argument );
                {
                    this.OnAfterAttach( argument );
                    this.OnAfterAttachCallback?.Invoke( argument );
                }
                foreach (var ancestor in this.Ancestors.OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnAfterDescendantAttach( (TThis) this, argument );
                    ancestor.OnAfterDescendantAttachCallback?.Invoke( (TThis) this, argument );
                }
            }
            {
                this.Activate( argument );
            }
        }
        private void Attach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.Owner = parent;
                foreach (var ancestor in this.Ancestors.Reverse().OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnBeforeDescendantAttachCallback?.Invoke( (TThis) this, argument );
                    ancestor.OnBeforeDescendantAttach( (TThis) this, argument );
                }
                {
                    this.OnBeforeAttachCallback?.Invoke( argument );
                    this.OnBeforeAttach( argument );
                }
                this.OnAttach( argument );
                {
                    this.OnAfterAttach( argument );
                    this.OnAfterAttachCallback?.Invoke( argument );
                }
                foreach (var ancestor in this.Ancestors.OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnAfterDescendantAttach( (TThis) this, argument );
                    ancestor.OnAfterDescendantAttachCallback?.Invoke( (TThis) this, argument );
                }
            }
            if (parent.Activity == Activity.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal void Detach(TreeMachineBase<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"Node {this} must have {machine} machine", this.Machine_NoRecursive == machine );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                foreach (var ancestor in this.Ancestors.Reverse().OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnBeforeDescendantDetachCallback?.Invoke( (TThis) this, argument );
                    ancestor.OnBeforeDescendantDetach( (TThis) this, argument );
                }
                {
                    this.OnBeforeDetachCallback?.Invoke( argument );
                    this.OnBeforeDetach( argument );
                }
                this.OnDetach( argument );
                {
                    this.OnAfterDetach( argument );
                    this.OnAfterDetachCallback?.Invoke( argument );
                }
                foreach (var ancestor in this.Ancestors.OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnAfterDescendantDetach( (TThis) this, argument );
                    ancestor.OnAfterDescendantDetachCallback?.Invoke( (TThis) this, argument );
                }
                this.Owner = null;
            }
        }
        private void Detach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"Node {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity == Activity.Active) {
                Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            }
            {
                foreach (var ancestor in this.Ancestors.Reverse().OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnBeforeDescendantDetachCallback?.Invoke( (TThis) this, argument );
                    ancestor.OnBeforeDescendantDetach( (TThis) this, argument );
                }
                {
                    this.OnBeforeDetachCallback?.Invoke( argument );
                    this.OnBeforeDetach( argument );
                }
                this.OnDetach( argument );
                {
                    this.OnAfterDetach( argument );
                    this.OnAfterDetachCallback?.Invoke( argument );
                }
                foreach (var ancestor in this.Ancestors.OfType<IDescendantNodeListener<TThis>>()) {
                    ancestor.OnAfterDescendantDetach( (TThis) this, argument );
                    ancestor.OnAfterDescendantDetachCallback?.Invoke( (TThis) this, argument );
                }
                this.Owner = null;
            }
        }

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
    public abstract partial class NodeBase<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            foreach (var ancestor in this.Ancestors.Reverse().OfType<IDescendantNodeListener<TThis>>()) {
                ancestor.OnBeforeDescendantActivateCallback?.Invoke( (TThis) this, argument );
                ancestor.OnBeforeDescendantActivate( (TThis) this, argument );
            }
            {
                this.OnBeforeActivateCallback?.Invoke( argument );
                this.OnBeforeActivate( argument );
            }
            {
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                foreach (var child in this.Children) {
                    child.Activate( argument );
                }
                this.Activity = Activity.Active;
            }
            {
                this.OnAfterActivate( argument );
                this.OnAfterActivateCallback?.Invoke( argument );
            }
            foreach (var ancestor in this.Ancestors.OfType<IDescendantNodeListener<TThis>>()) {
                ancestor.OnAfterDescendantActivate( (TThis) this, argument );
                ancestor.OnAfterDescendantActivateCallback?.Invoke( (TThis) this, argument );
            }
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            foreach (var ancestor in this.Ancestors.Reverse().OfType<IDescendantNodeListener<TThis>>()) {
                ancestor.OnBeforeDescendantDeactivateCallback?.Invoke( (TThis) this, argument );
                ancestor.OnBeforeDescendantDeactivate( (TThis) this, argument );
            }
            {
                this.OnBeforeDeactivateCallback?.Invoke( argument );
                this.OnBeforeDeactivate( argument );
            }
            {
                this.Activity = Activity.Deactivating;
                foreach (var child in this.Children.Reverse()) {
                    child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
                this.Activity = Activity.Inactive;
            }
            {
                this.OnAfterDeactivate( argument );
                this.OnAfterDeactivateCallback?.Invoke( argument );
            }
            foreach (var ancestor in this.Ancestors.OfType<IDescendantNodeListener<TThis>>()) {
                ancestor.OnAfterDescendantDeactivate( (TThis) this, argument );
                ancestor.OnAfterDescendantDeactivateCallback?.Invoke( (TThis) this, argument );
            }
        }

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
    public abstract partial class NodeBase<TThis> {

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"Node {this} must have no {child} child", !this.Children.Contains( child ) );
            this.Children_Mutable.Add( child );
            this.Sort( this.Children_Mutable );
            child.Attach( (TThis) this, argument );
        }
        protected void AddChildren(IEnumerable<TThis> children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
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
            Assert.Operation.Valid( $"Node {this} must have {child} child", this.Children.Contains( child ) );
            child.Detach( (TThis) this, argument );
            this.Children_Mutable.Remove( child );
            callback?.Invoke( child, argument );
        }
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected int RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            if (this.Parent != null) {
                this.Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Assert.Operation.Valid( $"Node {this} must have machine", this.Machine_NoRecursive != null );
                this.Machine_NoRecursive.RemoveRoot( (TThis) this, argument, callback );
            }
        }

        // Sort
        protected virtual void Sort(List<TThis> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
    public partial interface IDescendantNodeListener<TThis> where TThis : notnull, NodeBase<TThis> {

        // OnDescendantAttach
        public Action<TThis, object?>? OnBeforeDescendantAttachCallback { get; }
        public Action<TThis, object?>? OnAfterDescendantAttachCallback { get; }
        public Action<TThis, object?>? OnBeforeDescendantDetachCallback { get; }
        public Action<TThis, object?>? OnAfterDescendantDetachCallback { get; }

        // OnDescendantAttach
        protected internal void OnBeforeDescendantAttach(TThis descendant, object? argument);
        protected internal void OnAfterDescendantAttach(TThis descendant, object? argument);
        protected internal void OnBeforeDescendantDetach(TThis descendant, object? argument);
        protected internal void OnAfterDescendantDetach(TThis descendant, object? argument);

    }
    public partial interface IDescendantNodeListener<TThis> {

        // OnDescendantActivate
        public Action<TThis, object?>? OnBeforeDescendantActivateCallback { get; }
        public Action<TThis, object?>? OnAfterDescendantActivateCallback { get; }
        public Action<TThis, object?>? OnBeforeDescendantDeactivateCallback { get; }
        public Action<TThis, object?>? OnAfterDescendantDeactivateCallback { get; }

        // OnDescendantActivate
        protected internal void OnBeforeDescendantActivate(TThis descendant, object? argument);
        protected internal void OnAfterDescendantActivate(TThis descendant, object? argument);
        protected internal void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
        protected internal void OnAfterDescendantDeactivate(TThis descendant, object? argument);

    }
}
