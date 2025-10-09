#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

        // IsDisposed
        bool ITreeMachine<TMachineUserData, TNodeUserData>.IsDisposing => this.IsDisposing;
        bool ITreeMachine<TMachineUserData, TNodeUserData>.IsDisposed => this.IsDisposed;

        // Root
        INode<TMachineUserData, TNodeUserData>? ITreeMachine<TMachineUserData, TNodeUserData>.Root => this.Root;

        // UserData
        TMachineUserData ITreeMachine<TMachineUserData, TNodeUserData>.UserData => this.UserData;

        // OnDispose
        event Action? ITreeMachine<TMachineUserData, TNodeUserData>.OnDisposeCallback {
            add => this.OnDisposeCallback += value;
            remove => this.OnDisposeCallback -= value;
        }

    }
    public sealed partial class TreeMachine<TMachineUserData, TNodeUserData> {

        // Dispose
        void ITreeMachine<TMachineUserData, TNodeUserData>.Dispose() {
            this.Dispose();
        }

    }
}
