#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class TreeMachineBase<T> : ITreeMachine<T> where T : class, INode<T> {

        // Root
        T? ITreeMachine<T>.Root { get => this.Root; set => this.Root = value; }

        // Root
        protected T? Root { get; private set; }

        // Constructor
        public TreeMachineBase() {
        }

        // AddRoot
        void ITreeMachine<T>.AddRoot(T root, object? argument) {
            this.AddRoot( root, argument );
        }
        void ITreeMachine<T>.RemoveRoot(T root, object? argument, Action<T, object?>? callback) {
            this.RemoveRoot( root, argument, callback );
        }
        void ITreeMachine<T>.RemoveRoot(object? argument, Action<T, object?>? callback) {
            this.RemoveRoot( argument, callback );
        }

        // AddRoot
        protected virtual void AddRoot(T root, object? argument) {
            ITreeMachine<T>.AddRoot( this, root, argument );
        }
        protected virtual void RemoveRoot(T root, object? argument, Action<T, object?>? callback) {
            ITreeMachine<T>.RemoveRoot( this, root, argument, callback );
        }
        protected virtual void RemoveRoot(object? argument, Action<T, object?>? callback) {
            ITreeMachine<T>.RemoveRoot( this, argument, callback );
        }

    }
}
