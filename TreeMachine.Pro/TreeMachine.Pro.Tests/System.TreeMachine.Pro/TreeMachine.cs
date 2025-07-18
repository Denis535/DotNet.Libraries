namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class TreeMachine : TreeMachineBase<Node> {

        // Root
        public new Node? Root => base.Root;

        // Constructor
        public TreeMachine() {
        }

        // AddRoot
        public new void AddRoot(Node root, object? argument) {
            base.AddRoot( root, argument );
        }
        public new void RemoveRoot(Node root, object? argument, Action<Node, object?>? callback) {
            base.RemoveRoot( root, argument, callback );
        }
        public new void RemoveRoot(object? argument, Action<Node, object?>? callback) {
            base.RemoveRoot( argument, callback );
        }

    }
}
