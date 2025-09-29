#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITreeMachine<TMachineUserData, TNodeUserData> {

        // IsDisposed
        public bool IsDisposed { get; }

        // Root
        public INode<TMachineUserData, TNodeUserData>? Root { get; }

        // UserData
        public TMachineUserData UserData { get; }

        // Dispose
        internal void Dispose();

    }
}
