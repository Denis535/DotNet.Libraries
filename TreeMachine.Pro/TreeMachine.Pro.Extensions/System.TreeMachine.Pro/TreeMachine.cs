#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> : ITreeMachine<TMachineUserData, TNodeUserData>, IDisposable {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private INode<TMachineUserData, TNodeUserData>? m_Root = null;
        private readonly TMachineUserData m_UserData = default!;

        private Action? m_OnBeforeDisposeCallback = null;
        private Action? m_OnAfterDisposeCallback = null;

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

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
        public INode<TMachineUserData, TNodeUserData>? Root {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_Root;
            }
            private set {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_Root = value;
            }
        }

        // UserData
        public TMachineUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
        }

        // OnDispose
        public Action? OnBeforeDisposeCallback {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnBeforeDisposeCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_OnBeforeDisposeCallback = value;
            }
        }
        public Action? OnAfterDisposeCallback {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnAfterDisposeCallback;
            }
            init {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAfterDisposeCallback = value;
            }
        }

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

        // Constructor
        public TreeMachine(TMachineUserData userData) {
            this.m_UserData = userData;
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                this.OnBeforeDisposeCallback?.Invoke();
                this.Root?.Dispose();
                this.OnAfterDisposeCallback?.Invoke();
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

        // SetRoot
        public void SetRoot(INode<TMachineUserData, TNodeUserData>? root, object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback = null) {
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", root == null || !root.IsDisposed );
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(INode<TMachineUserData, TNodeUserData> root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Owner} owner", root.Owner == null );
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(INode<TMachineUserData, TNodeUserData> root, object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback = null) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {this} owner", root.Owner == this );
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"TreeMachine {this} must have {root} root", this.Root == root );
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
