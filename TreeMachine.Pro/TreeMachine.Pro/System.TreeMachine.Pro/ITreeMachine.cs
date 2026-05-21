#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITreeMachine : IDisposable {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

    }
    public interface ITreeMachine<out T> : ITreeMachine where T : INode<T> {

        // Root
        public T? Root { get; }

    }
}
