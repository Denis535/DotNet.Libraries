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
        public event Action? OnBeforeDisposeCallback;
        public event Action? OnAfterDisposeCallback;

        // Root
        public INode<TMachineUserData, TNodeUserData>? Root { get; }

    }
    public partial interface ITreeMachine<TMachineUserData, TNodeUserData> {

        // Dispose
        protected internal void Dispose();

    }
}
