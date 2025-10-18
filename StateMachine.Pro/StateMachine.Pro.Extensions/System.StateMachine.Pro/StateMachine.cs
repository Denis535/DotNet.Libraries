#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class StateMachine<TMachineUserData, TStateUserData> : IStateMachine<TMachineUserData, TStateUserData>, IDisposable {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private IState<TMachineUserData, TStateUserData>? m_Root = null;
        private readonly TMachineUserData m_UserData = default!;

        private readonly Action? m_OnDisposeCallback = null;

    }
    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

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

        // Root
        public IState<TMachineUserData, TStateUserData>? Root {
            get {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_Root;
            }
            private set {
                if (this.m_Root != null) {
                    Assert.Argument.Valid( $"Argument 'value' ({value}) must be null", value == null );
                } else {
                    Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                }
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_Root = value;
            }
        }

        // UserData
        public TMachineUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
        }

        // OnDispose
        public Action? OnDisposeCallback {
            get {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnDisposeCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback = value;
            }
        }

    }
    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // Constructor
        public StateMachine(TMachineUserData userData) {
            this.m_UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"StateMachine {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                this.OnDisposeCallback?.Invoke();
                Assert.Operation.Valid( $"StateMachine {this} must have no {this.Root} root", this.Root == null || this.Root.IsDisposed );
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }

        // Utils
        public override string ToString() {
            return "StateMachine: " + this.UserData?.ToString() ?? "Null";
        }

    }
    public sealed partial class StateMachine<TMachineUserData, TStateUserData> {

        // SetRoot
        public void SetRoot(IState<TMachineUserData, TStateUserData>? root, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback = null) {
            Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(IState<TMachineUserData, TStateUserData> root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Owner} owner", root.Owner == null );
            Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"StateMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(IState<TMachineUserData, TStateUserData> root, object? argument, Action<IState<TMachineUserData, TStateUserData>, object?>? callback = null) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {this} owner", root.Owner == this );
            Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"StateMachine {this} must have {root} root", this.Root == root );
            this.Root.Detach( this, argument );
            this.Root = null;
            if (callback != null) {
                callback.Invoke( root, argument );
            } else {
                root.Dispose();
            }
        }

    }
}
