#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public partial class Node : INode, IDisposable {

        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private readonly List<INode> m_Children = new List<INode>( 0 );

        // IsDisposed
        public bool IsDisposed { get; private set; }

        // Owner
        private object? Owner {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_Owner;
            }
            set {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_Owner = value;
            }
        }

        // Machine
        public ITreeMachine? Machine {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return (this.Owner as ITreeMachine) ?? (this.Owner as INode)?.Machine;
            }
        }
        internal ITreeMachine? Machine_NoRecursive {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as ITreeMachine;
            }
        }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )]
        public bool IsRoot {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Parent == null;
            }
        }
        public INode Root {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Parent?.Root ?? this;
            }
        }

        // Parent
        public INode? Parent {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as INode;
            }
        }
        public IEnumerable<INode> Ancestors {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<INode> AncestorsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Ancestors.Prepend( this );
            }
        }

        // Activity
        public Activity Activity {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_Activity;
            }
            private set {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_Activity = value;
            }
        }

        // Children
        public IReadOnlyList<INode> Children {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_Children;
            }
        }
        public IEnumerable<INode> Descendants {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<INode> DescendantsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Descendants.Prepend( this );
            }
        }

        // Sort
        public Action<List<INode>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public Node() {
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in this.Children) {
                child.Dispose();
            }
            this.IsDisposed = true;
        }

    }
    public partial class Node {

        // Machine
        ITreeMachine? INode.Machine => this.Machine;
        ITreeMachine? INode.Machine_NoRecursive => this.Machine_NoRecursive;

        // Root
        [MemberNotNullWhen( false, nameof( INode.Parent ) )] bool INode.IsRoot => this.IsRoot;
        INode INode.Root => this.Root;

        // Parent
        INode? INode.Parent => this.Parent;
        IEnumerable<INode> INode.Ancestors => this.Ancestors;
        IEnumerable<INode> INode.AncestorsAndSelf => this.AncestorsAndSelf;

        // Activity
        Activity INode.Activity => this.Activity;

        // Children
        IEnumerable<INode> INode.Children => this.Children;
        IEnumerable<INode> INode.Descendants => this.Descendants;
        IEnumerable<INode> INode.DescendantsAndSelf => this.DescendantsAndSelf;

        // Attach
        void INode.Attach(ITreeMachine machine, object? argument) {
            this.Attach( machine, argument );
        }
        void INode.Attach(INode parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void INode.Detach(ITreeMachine machine, object? argument) {
            this.Detach( machine, argument );
        }
        void INode.Detach(INode parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void INode.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void INode.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        // OnAttach
        void INode.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void INode.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void INode.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        // OnDetach
        void INode.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void INode.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void INode.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        // OnActivate
        void INode.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void INode.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void INode.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        // OnDeactivate
        void INode.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void INode.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void INode.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public partial class Node {

        // Attach
        internal void Attach(ITreeMachine machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
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
        private void Attach(INode parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
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
            }
        }

        // Detach
        internal void Detach(ITreeMachine machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
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
        private void Detach(INode parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
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
        private void OnAttach(object? argument) {
            this.OnAttachCallback?.Invoke( argument );
        }
        private void OnBeforeAttach(object? argument) {
        }
        private void OnAfterAttach(object? argument) {
        }

        // OnDetach
        private void OnDetach(object? argument) {
            this.OnDetachCallback?.Invoke( argument );
        }
        private void OnBeforeDetach(object? argument) {
        }
        private void OnAfterDetach(object? argument) {
        }

    }
    public partial class Node {

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity.Inactive );
            {
                this.OnBeforeActivate( argument );
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                foreach (var child in this.Children) {
                    child.Activate( argument );
                }
                this.Activity = Activity.Active;
                this.OnAfterActivate( argument );
            }
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity.Active );
            {
                this.OnBeforeDeactivate( argument );
                this.Activity = Activity.Deactivating;
                foreach (var child in this.Children.Reverse()) {
                    child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
                this.Activity = Activity.Inactive;
                this.OnAfterDeactivate( argument );
            }
        }

        // OnActivate
        private void OnActivate(object? argument) {
            this.OnActivateCallback?.Invoke( argument );
        }
        private void OnBeforeActivate(object? argument) {
        }
        private void OnAfterActivate(object? argument) {
        }

        // OnDeactivate
        private void OnDeactivate(object? argument) {
            this.OnDeactivateCallback?.Invoke( argument );
        }
        private void OnBeforeDeactivate(object? argument) {
        }
        private void OnAfterDeactivate(object? argument) {
        }

    }
    public partial class Node {

        // AddChild
        public void AddChild(INode child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have no {child} child", !this.Children.Contains( child ) );
            this.m_Children.Add( child );
            this.Sort( this.m_Children );
            child.Attach( this, argument );
        }
        public void AddChildren(IEnumerable<INode> children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        public void RemoveChild(INode child, object? argument, Action<INode, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            }
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have {child} child", this.Children.Contains( child ) );
            child.Detach( this, argument );
            _ = this.m_Children.Remove( child );
            if (callback != null) {
                callback.Invoke( child, argument );
            } else {
                child.Dispose();
            }
        }
        public bool RemoveChild(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        public int RemoveChildren(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        public int RemoveChildren(object? argument, Action<INode, object?>? callback) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }

        // RemoveSelf
        public void RemoveSelf(object? argument, Action<INode, object?>? callback) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have parent", this.Parent != null );
            ((Node) this.Parent).RemoveChild( this, argument, callback );
        }

        // Sort
        private void Sort(List<INode> children) {
            this.SortDelegate?.Invoke( children );
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
    public sealed class Node<TUserData> : Node, IUserData<TUserData> {

        private TUserData m_UserData = default!;

        // UserData
        public TUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // Constructor
        public Node(TUserData userData) {
            this.UserData = userData;
        }
        public override void Dispose() {
            base.Dispose();
            (this.m_UserData as IDisposable)?.Dispose();
        }

    }
}
