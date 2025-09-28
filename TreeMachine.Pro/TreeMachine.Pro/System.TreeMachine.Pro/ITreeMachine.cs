#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITreeMachine {

        // IsDisposed
        public bool IsDisposed { get; }

        // Root
        public INode? Root { get; }

        // Dispose
        internal void Dispose();

    }
    public interface ITreeMachine<out TUserData> : ITreeMachine {

        public TUserData UserData { get; }

    }
}
