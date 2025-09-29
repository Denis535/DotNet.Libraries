#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public sealed partial class ChildrenableState<TMachineUserData, TStateUserData> : IState<TMachineUserData, TStateUserData>, IDisposable {

        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private readonly List<IState<TMachineUserData, TStateUserData>> m_Children = new List<IState<TMachineUserData, TStateUserData>>( 0 );

        private TStateUserData m_UserData = default!;

        private readonly Action<List<IState<TMachineUserData, TStateUserData>>>? m_SortDelegate = null;

        private Action<object?>? m_OnAttachCallback = null;
        private Action<object?>? m_OnDetachCallback = null;

        private Action<object?>? m_OnActivateCallback = null;
        private Action<object?>? m_OnDeactivateCallback = null;

        private Action? m_OnDisposeCallback = null;

        // IsDisposed
        public bool IsDisposed { get; private set; }

        // Owner
        private object? Owner {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Owner;
            }
            set {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_Owner = value;
            }
        }

        // Machine
        public IStateMachine<TMachineUserData, TStateUserData>? Machine {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return (this.Owner as IStateMachine<TMachineUserData, TStateUserData>) ?? (this.Owner as IState<TMachineUserData, TStateUserData>)?.Machine;
            }
        }
        internal IStateMachine<TMachineUserData, TStateUserData>? Machine_NoRecursive {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as IStateMachine<TMachineUserData, TStateUserData>;
            }
        }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )]
        public bool IsRoot {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Parent == null;
            }
        }
        public IState<TMachineUserData, TStateUserData> Root {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Parent?.Root ?? this;
            }
        }

        // Parent
        public IState<TMachineUserData, TStateUserData>? Parent {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as IState<TMachineUserData, TStateUserData>;
            }
        }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Ancestors {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> AncestorsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Ancestors.Prepend( this );
            }
        }

        // Activity
        public Activity Activity {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Activity;
            }
            private set {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_Activity = value;
            }
        }

        // Children
        public IReadOnlyList<IState<TMachineUserData, TStateUserData>> Children {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Children;
            }
        }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Descendants {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> DescendantsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Descendants.Prepend( this );
            }
        }

        // UserData
        public TStateUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // Sort
        public Action<List<IState<TMachineUserData, TStateUserData>>>? SortDelegate {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_SortDelegate;
            }
            init {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_SortDelegate = value;
            }
        }

        // OnAttach
        public event Action<object?>? OnAttachCallback {
            add {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAttachCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAttachCallback -= value;
            }
        }
        public event Action<object?>? OnDetachCallback {
            add {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDetachCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDetachCallback -= value;
            }
        }

        // OnActivate
        public event Action<object?>? OnActivateCallback {
            add {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnActivateCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnActivateCallback -= value;
            }
        }
        public event Action<object?>? OnDeactivateCallback {
            add {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDeactivateCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDeactivateCallback -= value;
            }
        }

        // OnDispose
        public event Action? OnDisposeCallback {
            add {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback -= value;
            }
        }

        // Constructor
        public ChildrenableState() {
        }
        public ChildrenableState(TStateUserData userData) {
            this.UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in this.Children) {
                child.Dispose();
            }
            this.m_OnDisposeCallback?.Invoke();
            this.IsDisposed = true;
        }

    }
    public sealed partial class ChildrenableState<TMachineUserData, TStateUserData> {

        // Attach
        internal void Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity == Activity.Inactive );
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
        private void Attach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine_NoRecursive} machine", this.Machine_NoRecursive == null );
            Assert.Operation.Valid( $"State {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity == Activity.Inactive );
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
        internal void Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have {machine} machine", this.Machine_NoRecursive == machine );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity == Activity.Active );
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
        private void Detach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity == Activity.Active) {
                Assert.Operation.Valid( $"State {this} must be active", this.Activity == Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"State {this} must be inactive", this.Activity == Activity.Inactive );
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
            this.m_OnAttachCallback?.Invoke( argument );
        }
        private void OnBeforeAttach(object? argument) {
        }
        private void OnAfterAttach(object? argument) {
        }

        // OnDetach
        private void OnDetach(object? argument) {
            this.m_OnDetachCallback?.Invoke( argument );
        }
        private void OnBeforeDetach(object? argument) {
        }
        private void OnAfterDetach(object? argument) {
        }

    }
    public sealed partial class ChildrenableState<TMachineUserData, TStateUserData> {

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity == Activity.Inactive );
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
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have owner", this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"State {this} must have owner with valid activity", this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity == Activity.Active );
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
            this.m_OnActivateCallback?.Invoke( argument );
        }
        private void OnBeforeActivate(object? argument) {
        }
        private void OnAfterActivate(object? argument) {
        }

        // OnDeactivate
        private void OnDeactivate(object? argument) {
            this.m_OnDeactivateCallback?.Invoke( argument );
        }
        private void OnBeforeDeactivate(object? argument) {
        }
        private void OnAfterDeactivate(object? argument) {
        }

    }
    public sealed partial class ChildrenableState<TMachineUserData, TStateUserData> {

        // AddChild
        public void AddChild(IState<TMachineUserData, TStateUserData> child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have no {child} child", !this.Children.Contains( child ) );
            this.m_Children.Add( child );
            this.Sort( this.m_Children );
            child.Attach( this, argument );
        }
        public void AddChildren(IEnumerable<IState<TMachineUserData, TStateUserData>> children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        public void RemoveChild(IState<TMachineUserData, TStateUserData> child, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            }
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have {child} child", this.Children.Contains( child ) );
            child.Detach( this, argument );
            _ = this.m_Children.Remove( child );
            if (callback != null) {
                callback.Invoke( child, argument );
            } else {
                child.Dispose();
            }
        }
        public bool RemoveChild(Func<IState<TMachineUserData, TStateUserData>, bool> predicate, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        public int RemoveChildren(Func<IState<TMachineUserData, TStateUserData>, bool> predicate, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        public int RemoveChildren(object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }

        // Sort
        private void Sort(List<IState<TMachineUserData, TStateUserData>> children) {
            this.SortDelegate?.Invoke( children );
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}
