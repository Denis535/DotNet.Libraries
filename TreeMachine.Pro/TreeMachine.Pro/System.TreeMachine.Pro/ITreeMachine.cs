#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface ITreeMachine<TMachineUserData, TNodeUserData> {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // UserData
        public TMachineUserData UserData { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // Dispose
        protected internal void Dispose();

    }
    public partial interface ITreeMachine<TMachineUserData, TNodeUserData> {

        // Root
        public INode<TMachineUserData, TNodeUserData>? Root { get; }

    }
}
