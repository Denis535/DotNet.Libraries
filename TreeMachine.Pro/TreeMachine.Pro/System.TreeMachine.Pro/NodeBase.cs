#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> : INodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

        object? INodeBase<TThis>.Owner => this.Owner;

        ITreeMachine<TThis>? INodeBase<TThis>.Machine_NoRecursive => this.Machine_NoRecursive;

        void INodeBase<TThis>.Attach(ITreeMachine<TThis> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void INodeBase<TThis>.Attach(TThis parent, object? argument) {
            this.Attach( parent, argument );
        }

        void INodeBase<TThis>.Detach(ITreeMachine<TThis> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void INodeBase<TThis>.Detach(TThis parent, object? argument) {
            this.Detach( parent, argument );
        }

        void INodeBase<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void INodeBase<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void INodeBase<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void INodeBase<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void INodeBase<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void INodeBase<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        void INodeBase<TThis>.Activate(object? argument) {
            this.Activate( argument );
        }

        void INodeBase<TThis>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        void INodeBase<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void INodeBase<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void INodeBase<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void INodeBase<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void INodeBase<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void INodeBase<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

        void INodeBase<TThis>.AddChild(TThis child, object? argument) {
            this.AddChild( child, argument );
        }
        void INodeBase<TThis>.AddChildren(TThis[] children, object? argument) {
            this.AddChildren( children, argument );
        }

        void INodeBase<TThis>.RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            this.RemoveChild( child, argument, callback );
        }
        bool INodeBase<TThis>.RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChild( predicate, argument, callback );
        }

        int INodeBase<TThis>.RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChildren( predicate, argument, callback );
        }
        int INodeBase<TThis>.RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChildren( argument, callback );
        }

        void INodeBase<TThis>.RemoveSelf(object? argument, Action<TThis, object?>? callback) {
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
            Assert.Operation.Valid( $"Node {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
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
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
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
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
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
        protected void AddChildren(TThis[] children, object? argument) {
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
