#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TreeMachine : TreeMachineBase {

        // Root
        public new NodeBase? Root => base.Root;

        // Constructor
        public TreeMachine() {
        }

        // SetRoot
        public new void SetRoot(NodeBase? root, object? argument, Action<NodeBase, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
    public class TreeMachine<TUserData> : TreeMachine {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }

    }
}
