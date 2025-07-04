#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITreeMachine<T> where T : notnull, NodeBase<T> {

        // Root
        protected T? Root { get; set; }

        // AddRoot
        protected void AddRoot(T root, object? argument);
        protected internal void RemoveRoot(T root, object? argument, Action<T, object?>? callback);
        protected void RemoveRoot(object? argument, Action<T, object?>? callback);

        // Helpers
        protected static void AddRoot(ITreeMachine<T> machine, T root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Argument.Valid( $"Argument 'machine' ({machine}) must have no {machine.Root} root", machine.Root == null );
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no machine", root.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be inactive", root.Activity == NodeBase<T>.Activity_.Inactive );
            machine.Root = root;
            machine.Root.Attach( machine, argument );
        }
        protected static void RemoveRoot(ITreeMachine<T> machine, T root, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Argument.Valid( $"Argument 'machine' ({machine}) must have {root} root", machine.Root == root );
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {machine} machine", root.Machine_NoRecursive == machine );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be active", root.Activity == NodeBase<T>.Activity_.Active );
            machine.Root.Detach( machine, argument );
            machine.Root = null;
            callback?.Invoke( root, argument );
        }
        protected static void RemoveRoot(ITreeMachine<T> machine, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Argument.Valid( $"Argument 'machine' ({machine}) must have root", machine.Root != null );
            machine.RemoveRoot( machine.Root, argument, callback );
        }

    }
}
