#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> : ITreeMachine<TMachineUserData, TNodeUserData>, IDisposable {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;

        private INode<TMachineUserData, TNodeUserData>? m_Root = null;

        private TMachineUserData m_UserData = default!;

        private Action? m_OnDisposeCallback = null;

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

        // IsDisposed
        public bool IsDisposing {
            get {
                return this.m_Lifecycle == Lifecycle.Disposing;
            }
            private set {
                Assert.Operation.Valid( $"TreeMachine {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
                this.m_Lifecycle = Lifecycle.Disposing;
            }
        }
        public bool IsDisposed {
            get {
                return this.m_Lifecycle == Lifecycle.Disposed;
            }
            private set {
                Assert.Operation.Valid( $"TreeMachine {this} must be disposing", this.m_Lifecycle == Lifecycle.Disposing );
                this.m_Lifecycle = Lifecycle.Disposed;
            }
        }

        // UserData
        public TMachineUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // OnDispose
        public event Action? OnDisposeCallback {
            add {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_OnDisposeCallback -= value;
            }
        }

        // Constructor
        public TreeMachine() {
        }
        public TreeMachine(TMachineUserData userData) {
            this.UserData = userData;
        }
        public void Dispose() {
            this.IsDisposing = true;
            if (this.Root != null) {
                this.Root.Dispose();
            }
            this.m_OnDisposeCallback?.Invoke();
            this.IsDisposed = true;
        }

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

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

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

        // SetRoot
        public void SetRoot(INode<TMachineUserData, TNodeUserData>? root, object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback) {
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
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Machine_NoRecursive} machine", root.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Parent} parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be inactive", root.Activity == Activity.Inactive );
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(INode<TMachineUserData, TNodeUserData> root, object? argument, Action<INode<TMachineUserData, TNodeUserData>, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {this} machine", root.Machine_NoRecursive == this );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Parent} parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be active", root.Activity == Activity.Active );
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
