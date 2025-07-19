#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public partial interface INode<TThis> where TThis : class, INode<TThis> {

        // Owner
        protected object? Owner { get; set; }

        // Machine
        public sealed ITreeMachine<TThis>? Machine => (this.Owner as ITreeMachine<TThis>) ?? (this.Owner as INode<TThis>)?.Machine;
        protected internal sealed ITreeMachine<TThis>? Machine_NoRecursive => this.Owner as ITreeMachine<TThis>;

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
        public IReadOnlyList<TThis> Children { get; }
        public sealed IEnumerable<TThis> Descendants {
            get {
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public sealed IEnumerable<TThis> DescendantsAndSelf {
            get {
                return this.Descendants.Prepend( (TThis) this );
            }
        }

    }
    public partial interface INode<TThis> {

        // OnAttach
        public Action<object?>? OnBeforeAttachCallback { get; set; }
        public Action<object?>? OnAfterAttachCallback { get; set; }
        public Action<object?>? OnBeforeDetachCallback { get; set; }
        public Action<object?>? OnAfterDetachCallback { get; set; }

        // Attach
        internal sealed void Attach(ITreeMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.Owner = machine;
                foreach (var ancestor in this.Ancestors.Reverse().OfType<INode2<TThis>>()) {
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
                foreach (var ancestor in this.Ancestors.OfType<INode2<TThis>>()) {
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
                foreach (var ancestor in this.Ancestors.Reverse().OfType<INode2<TThis>>()) {
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
                foreach (var ancestor in this.Ancestors.OfType<INode2<TThis>>()) {
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
        internal sealed void Detach(ITreeMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"Node {this} must have {machine} machine", this.Machine_NoRecursive == machine );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                foreach (var ancestor in this.Ancestors.Reverse().OfType<INode2<TThis>>()) {
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
                foreach (var ancestor in this.Ancestors.OfType<INode2<TThis>>()) {
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
                foreach (var ancestor in this.Ancestors.Reverse().OfType<INode2<TThis>>()) {
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
                foreach (var ancestor in this.Ancestors.OfType<INode2<TThis>>()) {
                    ancestor.OnAfterDescendantDetach( (TThis) this, argument );
                    ancestor.OnAfterDescendantDetachCallback?.Invoke( (TThis) this, argument );
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
    public partial interface INode<TThis> {

        // OnActivate
        public Action<object?>? OnBeforeActivateCallback { get; set; }
        public Action<object?>? OnAfterActivateCallback { get; set; }
        public Action<object?>? OnBeforeDeactivateCallback { get; set; }
        public Action<object?>? OnAfterDeactivateCallback { get; set; }

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            foreach (var ancestor in this.Ancestors.Reverse().OfType<INode2<TThis>>()) {
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
            foreach (var ancestor in this.Ancestors.OfType<INode2<TThis>>()) {
                ancestor.OnAfterDescendantActivate( (TThis) this, argument );
                ancestor.OnAfterDescendantActivateCallback?.Invoke( (TThis) this, argument );
            }
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            foreach (var ancestor in this.Ancestors.Reverse().OfType<INode2<TThis>>()) {
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
            foreach (var ancestor in this.Ancestors.OfType<INode2<TThis>>()) {
                ancestor.OnAfterDescendantDeactivate( (TThis) this, argument );
                ancestor.OnAfterDescendantDeactivateCallback?.Invoke( (TThis) this, argument );
            }
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
    public partial interface INode<TThis> {

        // AddChild
        protected void AddChild(TThis child, object? argument);
        protected void AddChildren(IEnumerable<TThis> children, object? argument);

        // RemoveChild
        protected void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
        protected int RemoveChildren(object? argument, Action<TThis, object?>? callback);

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

        // Sort
        protected void Sort(List<TThis> children);

        // Helpers
        protected static void AddChild(TThis node, TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"Node {node} must have no {child} child", !node.Children.Contains( child ) );
            ((IList<TThis>) node.Children).Add( child );
            node.Sort( (List<TThis>) node.Children );
            child.Attach( node, argument );
        }
        protected static void AddChildren(TThis node, IEnumerable<TThis> children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            foreach (var child in children) {
                node.AddChild( child, argument );
            }
        }
        // Helpers
        protected static void RemoveChild(TThis node, TThis child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {node} parent", child.Parent == node );
            if (node.Activity == Activity.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            }
            Assert.Operation.Valid( $"Node {node} must have {child} child", node.Children.Contains( child ) );
            child.Detach( node, argument );
            ((IList<TThis>) node.Children).Remove( child );
            callback?.Invoke( child, argument );
        }
        protected static bool RemoveChild(TThis node, Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            var child = node.Children.LastOrDefault( predicate );
            if (child != null) {
                node.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected static int RemoveChildren(TThis node, Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            var children = node.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                node.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected static int RemoveChildren(TThis node, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            var children = node.Children.Reverse().ToList();
            foreach (var child in children) {
                node.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        // Helpers
        protected static void RemoveSelf(TThis node, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'node' must be non-null", node != null );
            if (node.Parent != null) {
                node.Parent.RemoveChild( node, argument, callback );
            } else {
                Assert.Operation.Valid( $"Node {node} must have machine", node.Machine_NoRecursive != null );
                node.Machine_NoRecursive.RemoveRoot( node, argument, callback );
            }
        }

    }
}
