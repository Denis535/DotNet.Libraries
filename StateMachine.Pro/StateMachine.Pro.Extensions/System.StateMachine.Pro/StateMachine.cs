#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class StateMachine<TUserData> : IStateMachine<TUserData>, IDisposable {

        private IState? m_Root = null;

        private TUserData m_UserData = default!;

        // IsDisposed
        public bool IsDisposed { get; private set; }

        // Root
        public IState? Root {
            get {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_Root;
            }
            private set {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_Root = value;
            }
        }

        // UserData
        public TUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // Constructor
        public StateMachine() {
        }
        public StateMachine(TUserData userData) {
            this.UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                this.Root.Dispose();
            }
            (this.UserData as IDisposable)?.Dispose();
            this.IsDisposed = true;
        }

    }
    public sealed partial class StateMachine<TUserData> {

        // Root
        IState? IStateMachine.Root => this.Root;

        // UserData
        TUserData IStateMachine<TUserData>.UserData => this.UserData;

    }
    public sealed partial class StateMachine<TUserData> {

        // SetRoot
        public void SetRoot(IState? root, object? argument, Action<IState, object?>? callback) {
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", root == null || !root.IsDisposed );
            Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(IState root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Machine} machine", root.Machine == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be inactive", root.Activity == Activity.Inactive );
            Assert.Operation.NotDisposed( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"StateMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(IState root, object? argument, Action<IState, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {this} machine", root.Machine == this );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be active", root.Activity == Activity.Active );
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
