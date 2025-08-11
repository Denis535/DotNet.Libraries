#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class TreeMachine<T, TUserData> : TreeMachineBase<T> where T : notnull, NodeBase<T> {

        // Root
        public new T? Root => base.Root;

        // UserData
        public TUserData UserData { get; private set; }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }

        // AddRoot
        public new void AddRoot(T root, object? argument) {
            base.AddRoot( root, argument );
        }
        public new void RemoveRoot(T root, object? argument, Action<T, object?>? callback) {
            base.RemoveRoot( root, argument, callback );
        }
        public new void RemoveRoot(object? argument, Action<T, object?>? callback) {
            base.RemoveRoot( argument, callback );
        }

    }
}
