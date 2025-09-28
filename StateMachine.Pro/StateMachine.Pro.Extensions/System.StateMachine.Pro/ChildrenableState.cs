#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public partial class ChildrenableState : IState, IDisposable {

        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private readonly List<IState> m_Children = new List<IState>( 0 );

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
        public IStateMachine? Machine {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return (this.Owner as StateMachine) ?? (this.Owner as IState)?.Machine;
            }
        }
        internal IStateMachine? Machine_NoRecursive {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as StateMachine;
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
        public IState Root {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Parent?.Root ?? this;
            }
        }

        // Parent
        public IState? Parent {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as IState;
            }
        }
        public IEnumerable<IState> Ancestors {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<IState> AncestorsAndSelf {
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
        public IReadOnlyList<IState> Children {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Children;
            }
        }
        public IEnumerable<IState> Descendants {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<IState> DescendantsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Descendants.Prepend( this );
            }
        }

        // Sort
        public Action<List<IState>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public ChildrenableState() {
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in this.Children) {
                child.Dispose();
            }
            this.IsDisposed = true;
        }

    }
    public partial class ChildrenableState {

        // Machine
        IStateMachine? IState.Machine => this.Machine;
        IStateMachine? IState.Machine_NoRecursive => this.Machine_NoRecursive;

        // Root
        bool IState.IsRoot => this.IsRoot;
        IState IState.Root => this.Root;

        // Parent
        IState? IState.Parent => this.Parent;
        IEnumerable<IState> IState.Ancestors => this.Ancestors;
        IEnumerable<IState> IState.AncestorsAndSelf => this.AncestorsAndSelf;

        // Activity
        Activity IState.Activity => this.Activity;

        // Children
        IEnumerable<IState> IState.Children => this.Children;
        IEnumerable<IState> IState.Descendants => this.Descendants;
        IEnumerable<IState> IState.DescendantsAndSelf => this.DescendantsAndSelf;

        // Attach
        void IState.Attach(IStateMachine machine, object? argument) {
            this.Attach( machine, argument );
        }
        void IState.Attach(IState parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void IState.Detach(IStateMachine machine, object? argument) {
            this.Detach( machine, argument );
        }
        void IState.Detach(IState parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void IState.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void IState.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        // OnAttach
        void IState.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IState.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IState.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        // OnDetach
        void IState.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IState.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IState.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        // OnActivate
        void IState.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IState.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IState.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        // OnDeactivate
        void IState.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IState.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IState.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public partial class ChildrenableState {

        // Attach
        internal void Attach(IStateMachine machine, object? argument) {
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
        private void Attach(IState parent, object? argument) {
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
        internal void Detach(IStateMachine machine, object? argument) {
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
        private void Detach(IState parent, object? argument) {
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
    public partial class ChildrenableState {

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
    public partial class ChildrenableState {

        // AddChild
        public void AddChild(IState child, object? argument) {
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
        public void AddChildren(IEnumerable<IState> children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        public void RemoveChild(IState child, object? argument, Action<IState, object?>? callback) {
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
        public bool RemoveChild(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        public int RemoveChildren(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        public int RemoveChildren(object? argument, Action<IState, object?>? callback) {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }

        // Sort
        private void Sort(List<IState> children) {
            this.SortDelegate?.Invoke( children );
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
    public class ChildrenableState<TUserData> : ChildrenableState, IUserData<TUserData> {

        private TUserData m_UserData = default!;

        // UserData
        public TUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // Constructor
        public ChildrenableState(TUserData userData) {
            this.UserData = userData;
        }
        public override void Dispose() {
            base.Dispose();
            (this.m_UserData as IDisposable)?.Dispose();
        }

    }
}
