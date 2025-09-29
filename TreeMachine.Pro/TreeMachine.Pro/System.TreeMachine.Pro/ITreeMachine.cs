#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface ITreeMachine<TMachineUserData, TNodeUserData> {

        // IsDisposed
        public bool IsDisposed { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // UserData
        public TMachineUserData UserData { get; }

        // Dispose
        internal void Dispose();

    }
    public partial interface ITreeMachine<TMachineUserData, TNodeUserData> {

        // Root
        public INode<TMachineUserData, TNodeUserData>? Root { get; }

    }
}
