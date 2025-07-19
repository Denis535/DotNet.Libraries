#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> : INode<TThis> where TThis : notnull, NodeBase<TThis> {

        object? INode<TThis>.Owner => this.Owner;

        ITreeMachine<TThis>? INode<TThis>.Machine => this.Machine;
        ITreeMachine<TThis>? INode<TThis>.Machine_NoRecursive => this.Machine_NoRecursive;

        bool INode<TThis>.IsRoot => this.IsRoot;
        TThis INode<TThis>.Root => this.Root;

        TThis? INode<TThis>.Parent => this.Parent;
        IEnumerable<TThis> INode<TThis>.Ancestors => this.Ancestors;
        IEnumerable<TThis> INode<TThis>.AncestorsAndSelf => this.AncestorsAndSelf;

        Activity INode<TThis>.Activity => this.Activity;

        IReadOnlyList<TThis> INode<TThis>.Children => this.Children;
        IEnumerable<TThis> INode<TThis>.Descendants => this.Descendants;
        IEnumerable<TThis> INode<TThis>.DescendantsAndSelf => this.DescendantsAndSelf;

    }
    public abstract partial class NodeBase<TThis> {

        event Action<object?>? INode<TThis>.OnBeforeAttachCallback {
            add {
                this.OnBeforeAttachCallback += value;
            }
            remove {
                this.OnBeforeAttachCallback -= value;
            }
        }
        event Action<object?>? INode<TThis>.OnAfterAttachCallback {
            add {
                this.OnAfterAttachCallback += value;
            }
            remove {
                this.OnAfterAttachCallback -= value;
            }
        }
        event Action<object?>? INode<TThis>.OnBeforeDetachCallback {
            add {
                this.OnBeforeDetachCallback += value;
            }
            remove {
                this.OnBeforeDetachCallback -= value;
            }
        }
        event Action<object?>? INode<TThis>.OnAfterDetachCallback {
            add {
                this.OnAfterDetachCallback += value;
            }
            remove {
                this.OnAfterDetachCallback -= value;
            }
        }

        void INode<TThis>.Attach(ITreeMachine<TThis> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void INode<TThis>.Attach(TThis parent, object? argument) {
            this.Attach( parent, argument );
        }
        void INode<TThis>.Detach(ITreeMachine<TThis> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void INode<TThis>.Detach(TThis parent, object? argument) {
            this.Detach( parent, argument );
        }

        void INode<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void INode<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void INode<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void INode<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void INode<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void INode<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

    }
    public abstract partial class NodeBase<TThis> {

        event Action<object?>? INode<TThis>.OnBeforeActivateCallback {
            add {
                this.OnBeforeActivateCallback += value;
            }
            remove {
                this.OnBeforeActivateCallback -= value;
            }
        }
        event Action<object?>? INode<TThis>.OnAfterActivateCallback {
            add {
                this.OnAfterActivateCallback += value;
            }
            remove {
                this.OnAfterActivateCallback -= value;
            }
        }
        event Action<object?>? INode<TThis>.OnBeforeDeactivateCallback {
            add {
                this.OnBeforeDeactivateCallback += value;
            }
            remove {
                this.OnBeforeDeactivateCallback -= value;
            }
        }
        event Action<object?>? INode<TThis>.OnAfterDeactivateCallback {
            add {
                this.OnAfterDeactivateCallback += value;
            }
            remove {
                this.OnAfterDeactivateCallback -= value;
            }
        }

        void INode<TThis>.Activate(object? argument) {
            this.Activate( argument );
        }
        void INode<TThis>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        void INode<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void INode<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void INode<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void INode<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void INode<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void INode<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class NodeBase<TThis> {

        void INode<TThis>.AddChild(TThis child, object? argument) {
            this.AddChild( child, argument );
        }
        void INode<TThis>.AddChildren(IEnumerable<TThis> children, object? argument) {
            this.AddChildren( children, argument );
        }
        void INode<TThis>.RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            this.RemoveChild( child, argument, callback );
        }
        bool INode<TThis>.RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChild( predicate, argument, callback );
        }
        int INode<TThis>.RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChildren( predicate, argument, callback );
        }
        int INode<TThis>.RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChildren( argument, callback );
        }
        void INode<TThis>.RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            this.RemoveSelf( argument, callback );
        }

    }
    public abstract partial class NodeBase<TThis> {

        private readonly List<TThis> children = new List<TThis>( 0 );

        // Owner
        private object? Owner { get; set; }

        // Machine
        public ITreeMachine<TThis>? Machine => (this.Owner as ITreeMachine<TThis>) ?? (this.Owner as NodeBase<TThis>)?.Machine;
        internal ITreeMachine<TThis>? Machine_NoRecursive => this.Owner as ITreeMachine<TThis>;

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
        public IReadOnlyList<TThis> Children => this.children;
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
        internal void Attach(ITreeMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.Owner = machine;
                this.OnBeforeAttachCallback?.Invoke( argument );
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
                this.OnAfterAttachCallback?.Invoke( argument );
            }
            {
                this.Activate( argument );
            }
        }
        internal void Attach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttachCallback?.Invoke( argument );
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
                this.OnAfterAttachCallback?.Invoke( argument );
            }
            if (parent.Activity == Activity.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal void Detach(ITreeMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"Node {this} must have {machine} machine", this.Machine_NoRecursive == machine );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetachCallback?.Invoke( argument );
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.OnAfterDetachCallback?.Invoke( argument );
                this.Owner = null;
            }
        }
        internal void Detach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"Node {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity == Activity.Active) {
                Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            }
            {
                this.OnBeforeDetachCallback?.Invoke( argument );
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.OnAfterDetachCallback?.Invoke( argument );
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
            this.OnBeforeActivateCallback?.Invoke( argument );
            this.OnBeforeActivate( argument );
            this.Activity = Activity.Activating;
            {
                this.OnActivate( argument );
                foreach (var child in this.Children) {
                    child.Activate( argument );
                }
            }
            this.Activity = Activity.Active;
            this.OnAfterActivate( argument );
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            this.OnBeforeDeactivateCallback?.Invoke( argument );
            this.OnBeforeDeactivate( argument );
            this.Activity = Activity.Deactivating;
            {
                foreach (var child in this.Children.Reverse()) {
                    child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
            }
            this.Activity = Activity.Inactive;
            this.OnAfterDeactivate( argument );
            this.OnAfterDeactivateCallback?.Invoke( argument );
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
            this.children.Add( child );
            this.Sort( this.children );
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
            this.children.Remove( child );
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
}
