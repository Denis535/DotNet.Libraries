#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class TreeMachine<TRoot, TNode, TUserData> : TreeMachineBase<TRoot, TNode> where TRoot : TNode where TNode : notnull, NodeBase<TNode> {

        // Root
        public new TRoot? Root => base.Root;

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }

        // SetRoot
        public new void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
}
