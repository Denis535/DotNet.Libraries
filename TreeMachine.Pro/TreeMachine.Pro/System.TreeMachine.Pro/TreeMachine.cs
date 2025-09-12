#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class TreeMachine<TNode, TUserData> : TreeMachineBase<TNode> where TNode : notnull, NodeBase<TNode> {

        // Root
        public new TNode? Root => base.Root;

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }

        // AddRoot
        public new void AddRoot(TNode root, object? argument) {
            base.AddRoot( root, argument );
        }
        public new void RemoveRoot(TNode root, object? argument, Action<TNode, object?>? callback) {
            base.RemoveRoot( root, argument, callback );
        }
        public new void RemoveRoot(object? argument, Action<TNode, object?>? callback) {
            base.RemoveRoot( argument, callback );
        }

    }
}
