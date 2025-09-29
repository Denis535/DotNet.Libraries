#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public sealed partial class ChildableState<TMachineUserData, TStateUserData> : IState<TMachineUserData, TStateUserData>, IDisposable {

        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private IState<TMachineUserData, TStateUserData>? m_Child = null;

        private Action<object?>? m_OnAttachCallback = null;
        private Action<object?>? m_OnDetachCallback = null;

        private Action<object?>? m_OnActivateCallback = null;
        private Action<object?>? m_OnDeactivateCallback = null;

        private TStateUserData m_UserData = default!;

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
        public IState<TMachineUserData, TStateUserData>? Child {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Child;
            }
            private set {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_Child = value;
            }
        }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> Descendants {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                if (this.Child != null) {
                    yield return this.Child;
                    foreach (var i in this.Child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<IState<TMachineUserData, TStateUserData>> DescendantsAndSelf {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Descendants.Prepend( this );
            }
        }

        // OnAttach
        public event Action<object?>? OnAttachCallback {
            add {
                this.m_OnAttachCallback += value;
            }
            remove {
                this.m_OnAttachCallback -= value;
            }
        }
        public event Action<object?>? OnDetachCallback {
            add {
                this.m_OnDetachCallback += value;
            }
            remove {
                this.m_OnDetachCallback -= value;
            }
        }

        // OnActivate
        public event Action<object?>? OnActivateCallback {
            add {
                this.m_OnActivateCallback += value;
            }
            remove {
                this.m_OnActivateCallback -= value;
            }
        }
        public event Action<object?>? OnDeactivateCallback {
            add {
                this.m_OnDeactivateCallback += value;
            }
            remove {
                this.m_OnDeactivateCallback -= value;
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

        // Constructor
        public ChildableState() {
        }
        public ChildableState(TStateUserData userData) {
            this.UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            if (this.Child is IState<TMachineUserData, TStateUserData> child) {
                child.Dispose();
            }
            (this.UserData as IDisposable)?.Dispose();
            this.IsDisposed = true;
        }

    }
    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

        // Machine
        IStateMachine<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Machine => this.Machine;
        IStateMachine<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Machine_NoRecursive => this.Machine_NoRecursive;

        // Root
        bool IState<TMachineUserData, TStateUserData>.IsRoot => this.IsRoot;
        IState<TMachineUserData, TStateUserData> IState<TMachineUserData, TStateUserData>.Root => this.Root;

        // Parent
        IState<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Parent => this.Parent;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Ancestors => this.Ancestors;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.AncestorsAndSelf => this.AncestorsAndSelf;

        // Activity
        Activity IState<TMachineUserData, TStateUserData>.Activity => this.Activity;

        // Children
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Children {
            get {
                if (this.Child != null) {
                    return new[] { this.Child };
                } else {
                    return Enumerable.Empty<IState<TMachineUserData, TStateUserData>>();
                }
            }
        }
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Descendants => this.Descendants;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.DescendantsAndSelf => this.DescendantsAndSelf;

        // OnAttach
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnAttachCallback {
            add => this.OnAttachCallback += value;
            remove => this.OnAttachCallback -= value;
        }
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnDetachCallback {
            add => this.OnDetachCallback += value;
            remove => this.OnDetachCallback -= value;
        }

        // OnActivate
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnActivateCallback {
            add => this.OnActivateCallback += value;
            remove => this.OnActivateCallback -= value;
        }
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnDeactivateCallback {
            add => this.OnDeactivateCallback += value;
            remove => this.OnDeactivateCallback -= value;
        }

        // UserData
        TStateUserData IState<TMachineUserData, TStateUserData>.UserData => this.UserData;

        // Attach
        void IState<TMachineUserData, TStateUserData>.Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void IState<TMachineUserData, TStateUserData>.Attach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void IState<TMachineUserData, TStateUserData>.Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void IState<TMachineUserData, TStateUserData>.Detach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void IState<TMachineUserData, TStateUserData>.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void IState<TMachineUserData, TStateUserData>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        // OnAttach
        void IState<TMachineUserData, TStateUserData>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        // OnDetach
        void IState<TMachineUserData, TStateUserData>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        // OnActivate
        void IState<TMachineUserData, TStateUserData>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        // OnDeactivate
        void IState<TMachineUserData, TStateUserData>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

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
    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

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
                if (this.Child != null) {
                    this.Child.Activate( argument );
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
                if (this.Child != null) {
                    this.Child.Deactivate( argument );
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
    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

        // SetChild
        public void SetChild(IState<TMachineUserData, TStateUserData>? child, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback) {
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", child == null || !child.IsDisposed );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            if (this.Child != null) {
                this.RemoveChild( this.Child, argument, callback );
            }
            if (child != null) {
                this.AddChild( child, argument );
            }
        }

        // AddChild
        private void AddChild(IState<TMachineUserData, TStateUserData> child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have no {this.Child} child", this.Child == null );
            this.Child = child;
            this.Child.Attach( this, argument );
        }

        // RemoveChild
        private void RemoveChild(IState<TMachineUserData, TStateUserData> child, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity.Inactive );
            }
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have {child} child", this.Child == child );
            this.Child.Detach( this, argument );
            this.Child = null;
            if (callback != null) {
                callback.Invoke( child, argument );
            } else {
                child.Dispose();
            }
        }

    }
}
