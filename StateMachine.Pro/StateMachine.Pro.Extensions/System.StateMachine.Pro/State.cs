#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public sealed partial class State<TMachineUserData, TStateUserData> : IState<TMachineUserData, TStateUserData>, IDisposable {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private readonly TStateUserData m_UserData = default!;

        private readonly Action? m_OnDisposeCallback = null;

        private readonly Action<object?>? m_OnAttachCallback = null;
        private readonly Action<object?>? m_OnDetachCallback = null;

        private readonly Action<object?>? m_OnActivateCallback = null;
        private readonly Action<object?>? m_OnDeactivateCallback = null;

    }
    public sealed partial class State<TMachineUserData, TStateUserData> {

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
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Owner;
            }
            private set {
                if (this.Owner != null) {
                    Assert.Argument.Valid( $"Argument 'value' ({value}) must be null", value == null );
                } else {
                    Assert.Argument.Valid( $"Argument 'value' must be non-null", value != null );
                }
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                if (value != null) {
                    Assert.Operation.Valid( $"State {this} must have valid activity", this.Activity is Activity.Inactive );
                } else {
                    Assert.Operation.Valid( $"State {this} must have valid activity", this.Activity is Activity.Active or Activity.Inactive );
                }
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
                Assert.Argument.Valid( $"Argument 'value' ({value}) must be valid", value != this.Activity );
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                Assert.Operation.Valid( $"State {this} must have owner", this.Owner != null );
                this.m_Activity = value;
            }
        }

        // UserData
        public TStateUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
        }

        // OnDispose
        public Action? OnDisposeCallback {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDisposeCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback = value;
            }
        }

        // OnAttach
        public Action<object?>? OnAttachCallback {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnAttachCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAttachCallback = value;
            }
        }
        public Action<object?>? OnDetachCallback {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDetachCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDetachCallback = value;
            }
        }

        // OnActivate
        public Action<object?>? OnActivateCallback {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnActivateCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnActivateCallback = value;
            }
        }
        public Action<object?>? OnDeactivateCallback {
            get {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDeactivateCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDeactivateCallback = value;
            }
        }

    }
    public sealed partial class State<TMachineUserData, TStateUserData> {

        // Constructor
        public State(TStateUserData userData) {
            this.m_UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"State {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            if (this.Owner is IStateMachine<TMachineUserData, TStateUserData> owner_machine) {
                Assert.Operation.Valid( $"Owner {owner_machine} must be disposing", owner_machine.IsDisposing );
            }
            if (this.Owner is IState<TMachineUserData, TStateUserData> owner_parent) {
                Assert.Operation.Valid( $"Owner {owner_parent} must be disposing", owner_parent.IsDisposing );
            }
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                this.OnDisposeCallback?.Invoke();
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }

        // Utils
        public override string ToString() {
            return "State: " + this.UserData?.ToString() ?? "Null";
        }

    }
    public sealed partial class State<TMachineUserData, TStateUserData> {

        // Attach
        private void Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have no {this.Owner} owner", this.Owner == null );
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
        private void Attach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have no {this.Owner} owner", this.Owner == null );
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
        private void Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have {machine} owner", this.Owner == machine );
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
        private void Detach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.NotDisposed( $"State {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"State {this} must have {parent} owner", this.Owner == parent );
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
    public sealed partial class State<TMachineUserData, TStateUserData> {

        // Activate
        private void Activate(object? argument) {
            this.OnBeforeActivate( argument );
            {
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                this.Activity = Activity.Active;
            }
            this.OnAfterActivate( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
            {
                this.Activity = Activity.Deactivating;
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
}
