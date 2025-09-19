#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class TreeMachineBase {

        // Root
        protected NodeBase? Root { get; private set; }

        // Constructor
        public TreeMachineBase() {
        }

        // SetRoot
        protected virtual void SetRoot(NodeBase? root, object? argument, Action<NodeBase, object?>? callback) {
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(NodeBase root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Machine_NoRecursive} machine", root.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Parent} parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be inactive", root.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(NodeBase root, object? argument, Action<NodeBase, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {this} machine", root.Machine_NoRecursive == this );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Parent} parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be active", root.Activity == Activity.Active );
            Assert.Operation.Valid( $"TreeMachine {this} must have {root} root", this.Root == root );
            this.Root.Detach( this, argument );
            this.Root = null;
            callback?.Invoke( root, argument );
        }

    }
}
