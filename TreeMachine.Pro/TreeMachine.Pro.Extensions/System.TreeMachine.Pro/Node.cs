#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public sealed partial class Node<TMachineUserData, TNodeUserData> : INode<TMachineUserData, TNodeUserData>, IDisposable {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private readonly List<INode<TMachineUserData, TNodeUserData>> m_Children = new List<INode<TMachineUserData, TNodeUserData>>( 0 );
        private readonly Action<List<INode<TMachineUserData, TNodeUserData>>>? m_SortDelegate = null;
        private readonly TNodeUserData m_UserData = default!;

        private readonly Action? m_OnDisposeCallback = null;

        private readonly Action<object?>? m_OnAttachCallback = null;
        private readonly Action<object?>? m_OnDetachCallback = null;

        private readonly Action<object?>? m_OnActivateCallback = null;
        private readonly Action<object?>? m_OnDeactivateCallback = null;

    }
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // IsDisposed
        public bool IsDisposing {
            get {
                return this.m_Lifecycle == Lifecycle.Disposing;
            }
        }
        public bool IsDisposed {
            get {
                return this.m_Lifecycle == Lifecycle.Disposed;
            }
        }

        // Owner
        public object? Owner {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_Owner;
            }
            private set {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                if (value != null) {
                    Assert.Operation.Valid( $"Node {this} must have no {this.m_Owner} owner", this.m_Owner == null );
                    Assert.Operation.Valid( $"Node {this} must have valid activity", this.Activity is Activity.Inactive );
                } else {
                    Assert.Operation.Valid( $"Node {this} must have owner", this.m_Owner != null );
                    Assert.Operation.Valid( $"Node {this} must have valid activity", this.Activity is Activity.Active or Activity.Inactive );
                }
                this.m_Owner = value;
            }
        }

        // Machine
        public ITreeMachine<TMachineUserData, TNodeUserData>? Machine {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return (this.Owner as ITreeMachine<TMachineUserData, TNodeUserData>) ?? (this.Owner as INode<TMachineUserData, TNodeUserData>)?.Machine;
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
        public INode<TMachineUserData, TNodeUserData> Root {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Parent?.Root ?? this;
            }
        }

        // Parent
        public INode<TMachineUserData, TNodeUserData>? Parent {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as INode<TMachineUserData, TNodeUserData>;
            }
        }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> Ancestors {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> AncestorsAndSelf {
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
                Assert.Operation.Valid( $"Node {this} must have owner", this.Owner != null );
                Assert.Operation.Valid( $"Node {this} must have valid activity", this.m_Activity != value );
                this.m_Activity = value;
            }
        }

        // Children
        public IReadOnlyList<INode<TMachineUserData, TNodeUserData>> Children {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_Children;
            }
        }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> Descendants {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> DescendantsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.Descendants.Prepend( this );
            }
        }

        // Sort
        public Action<List<INode<TMachineUserData, TNodeUserData>>>? SortDelegate {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_SortDelegate;
            }
            init {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_SortDelegate = value;
            }
        }

        // UserData
        public TNodeUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
        }

        // OnDispose
        public Action? OnDisposeCallback {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDisposeCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback = value;
            }
        }

        // OnAttach
        public Action<object?>? OnAttachCallback {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnAttachCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAttachCallback = value;
            }
        }
        public Action<object?>? OnDetachCallback {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDetachCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDetachCallback = value;
            }
        }

        // OnActivate
        public Action<object?>? OnActivateCallback {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnActivateCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_OnActivateCallback = value;
            }
        }
        public Action<object?>? OnDeactivateCallback {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDeactivateCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDeactivateCallback = value;
            }
        }

    }
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // Constructor
        public Node(TNodeUserData userData) {
            this.m_UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"Node {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            if (this.Owner is ITreeMachine<TMachineUserData, TNodeUserData> owner_machine) {
                Assert.Operation.Valid( $"Owner {owner_machine} must be disposing", owner_machine.IsDisposing );
            }
            if (this.Owner is INode<TMachineUserData, TNodeUserData> owner_parent) {
                Assert.Operation.Valid( $"Owner {owner_parent} must be disposing", owner_parent.IsDisposing );
            }
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                this.OnDisposeCallback?.Invoke();
                Assert.Operation.Valid( $"Node {this} must have no {this.Children.Count( i => !i.IsDisposed )} children", this.Children.All( i => i.IsDisposed ) );
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }

        // Utils
        public override string ToString() {
            return this.UserData?.ToString() ?? base.ToString();
        }

    }
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // Attach
        private void Attach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have no {this.Owner} owner", this.Owner == null );
            {
                this.Owner = machine;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (true) {
                this.Activate( argument );
            }
        }
        private void Attach(INode<TMachineUserData, TNodeUserData> parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have no {this.Owner} owner", this.Owner == null );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (this.Parent!.Activity == Activity.Active) {
                this.Activate( argument );
            }
        }

        // Detach
        private void Detach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have {machine} owner", this.Owner == machine );
            if (true) {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }
        private void Detach(INode<TMachineUserData, TNodeUserData> parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have {parent} owner", this.Owner == parent );
            if (this.Activity == Activity.Active) {
                this.Deactivate( argument );
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
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // Activate
        private void Activate(object? argument) {
            this.OnBeforeActivate( argument );
            {
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                foreach (var child in this.Children.ToList()) {
                    child.Activate( argument );
                }
                this.Activity = Activity.Active;
            }
            this.OnAfterActivate( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
            {
                this.Activity = Activity.Deactivating;
                foreach (var child in this.Children.Reverse()) {
                    child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
                this.Activity = Activity.Inactive;
            }
            this.OnAfterDeactivate( argument );
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
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // AddChild
        public void AddChild(INode<TMachineUserData, TNodeUserData> child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Owner} owner", child.Owner == null );
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have no {child} child", !this.Children.Contains( child ) );
            this.m_Children.Add( child );
            this.Sort( this.m_Children );
            child.Attach( this, argument );
        }
        public void AddChildren(IEnumerable<INode<TMachineUserData, TNodeUserData>> children, object? argument) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        public void RemoveChild(INode<TMachineUserData, TNodeUserData> child, object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback = null) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} owner", child.Owner == this );
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
        public int RemoveChildren(Func<INode<TMachineUserData, TNodeUserData>, bool> predicate, object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback = null) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            var count = 0;
            foreach (var child in this.Children.Reverse().Where( predicate )) {
                this.RemoveChild( child, argument, callback );
                count++;
            }
            return count;
        }

        // RemoveSelf
        public void RemoveSelf(object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback = null) {
            Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"Node {this} must have owner", this.Owner != null );
            if (this.Owner is Node<TMachineUserData, TNodeUserData> parent) {
                parent.RemoveChild( this, argument, callback );
            } else {
                ((TreeMachine<TMachineUserData, TNodeUserData>) this.Owner).SetRoot( null, argument, callback );
            }
        }

        // Sort
        private void Sort(List<INode<TMachineUserData, TNodeUserData>> children) {
            this.SortDelegate?.Invoke( children );
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}
