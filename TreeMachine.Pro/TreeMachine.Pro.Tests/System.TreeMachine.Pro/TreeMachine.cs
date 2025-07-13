namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class TreeMachine : ITreeMachine<Node> {

        // Root
        Node? ITreeMachine<Node>.Root { get => this.Root; set => this.Root = value; }
        public Node? Root { get; private set; }

        // Constructor
        public TreeMachine() {
        }

        // AddRoot
        public void AddRoot(Node root, object? argument) {
            ITreeMachine<Node>.AddRoot( this, root, argument );
        }
        public void RemoveRoot(Node root, object? argument, Action<Node, object?>? callback) {
            ITreeMachine<Node>.RemoveRoot( this, root, argument, callback );
        }
        public void RemoveRoot(object? argument, Action<Node, object?>? callback) {
            ITreeMachine<Node>.RemoveRoot( this, argument, callback );
        }

    }
}
