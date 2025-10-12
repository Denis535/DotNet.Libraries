#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface INode<TMachineUserData, TNodeUserData> {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

        // Owner
        public object? Owner { get; }

        // Machine
        public ITreeMachine<TMachineUserData, TNodeUserData>? Machine { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public INode<TMachineUserData, TNodeUserData> Root { get; }

        // Parent
        public INode<TMachineUserData, TNodeUserData>? Parent { get; }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> Ancestors { get; }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> Children { get; }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> Descendants { get; }
        public IEnumerable<INode<TMachineUserData, TNodeUserData>> DescendantsAndSelf { get; }

        // UserData
        public TNodeUserData UserData { get; }

    }
    public partial interface INode<TMachineUserData, TNodeUserData> {

        // Dispose
        protected internal void Dispose();

        // Attach
        protected internal void Attach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument);
        protected internal void Attach(INode<TMachineUserData, TNodeUserData> parent, object? argument);

        // Detach
        protected internal void Detach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument);
        protected internal void Detach(INode<TMachineUserData, TNodeUserData> parent, object? argument);

        // Activate
        protected internal void Activate(object? argument);

        // Deactivate
        protected internal void Deactivate(object? argument);

    }
}
