#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITreeMachine<TMachineUserData, TNodeUserData> : IDisposable {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // Root
        public INode<TMachineUserData, TNodeUserData>? Root { get; }

        // UserData
        public TMachineUserData UserData { get; }

    }
}
