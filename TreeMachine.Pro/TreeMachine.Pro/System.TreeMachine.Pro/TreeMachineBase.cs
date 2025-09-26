#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class TreeMachineBase : IDisposable {

        private INode? m_Root = null;

        // IsDisposed
        public bool IsDisposed { get; private set; }

        // Root
        protected INode? Root {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_Root;
            }
            private set {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_Root = value;
            }
        }

        // Constructor
        public TreeMachineBase() {
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                Assert.Operation.NotDisposed( $"Root {this.Root} must be disposed", this.Root.IsDisposed );
            }
            this.IsDisposed = true;
        }

        // SetRoot
        protected virtual void SetRoot(INode? root, object? argument, Action<INode, object?>? callback) {
            Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(INode root, object? argument) {
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
        private void RemoveRoot(INode root, object? argument, Action<INode, object?>? callback) {
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
