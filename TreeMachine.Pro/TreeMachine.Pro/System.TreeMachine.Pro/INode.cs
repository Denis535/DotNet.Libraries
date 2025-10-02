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

        // UserData
        public TNodeUserData UserData { get; }

        // OnDispose
        public event Action? OnDisposeCallback;

        // Dispose
        internal void Dispose();

    }
    public partial interface INode<TMachineUserData, TNodeUserData> {

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

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

    }
    public partial interface INode<TMachineUserData, TNodeUserData> {

        // Attach
        internal void Attach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument);
        internal void Attach(INode<TMachineUserData, TNodeUserData> parent, object? argument);

        // Detach
        internal void Detach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument);
        internal void Detach(INode<TMachineUserData, TNodeUserData> parent, object? argument);

        // Activate
        internal void Activate(object? argument);

        // Deactivate
        internal void Deactivate(object? argument);

        // OnAttach
        internal void OnAttach(object? argument);
        internal void OnBeforeAttach(object? argument);
        internal void OnAfterAttach(object? argument);

        // OnDetach
        internal void OnDetach(object? argument);
        internal void OnBeforeDetach(object? argument);
        internal void OnAfterDetach(object? argument);

        // OnActivate
        internal void OnActivate(object? argument);
        internal void OnBeforeActivate(object? argument);
        internal void OnAfterActivate(object? argument);

        // OnDeactivate
        internal void OnDeactivate(object? argument);
        internal void OnBeforeDeactivate(object? argument);
        internal void OnAfterDeactivate(object? argument);

    }
}
