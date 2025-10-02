#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // IsDisposed
        bool INode<TMachineUserData, TNodeUserData>.IsDisposing => this.IsDisposing;
        bool INode<TMachineUserData, TNodeUserData>.IsDisposed => this.IsDisposed;

        // UserData
        TNodeUserData INode<TMachineUserData, TNodeUserData>.UserData => this.UserData;

        // OnDispose
        event Action? INode<TMachineUserData, TNodeUserData>.OnDisposeCallback {
            add => this.OnDisposeCallback += value;
            remove => this.OnDisposeCallback -= value;
        }

        // Dispose
        void INode<TMachineUserData, TNodeUserData>.Dispose() {
            this.Dispose();
        }

    }
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // Owner
        object? INode<TMachineUserData, TNodeUserData>.Owner => this.Owner;

        // Machine
        ITreeMachine<TMachineUserData, TNodeUserData>? INode<TMachineUserData, TNodeUserData>.Machine => this.Machine;

        // Root
        [MemberNotNullWhen( false, nameof( INode<TMachineUserData, TNodeUserData>.Parent ) )] bool INode<TMachineUserData, TNodeUserData>.IsRoot => this.IsRoot;
        INode<TMachineUserData, TNodeUserData> INode<TMachineUserData, TNodeUserData>.Root => this.Root;

        // Parent
        INode<TMachineUserData, TNodeUserData>? INode<TMachineUserData, TNodeUserData>.Parent => this.Parent;
        IEnumerable<INode<TMachineUserData, TNodeUserData>> INode<TMachineUserData, TNodeUserData>.Ancestors => this.Ancestors;
        IEnumerable<INode<TMachineUserData, TNodeUserData>> INode<TMachineUserData, TNodeUserData>.AncestorsAndSelf => this.AncestorsAndSelf;

        // Activity
        Activity INode<TMachineUserData, TNodeUserData>.Activity => this.Activity;

        // Children
        IEnumerable<INode<TMachineUserData, TNodeUserData>> INode<TMachineUserData, TNodeUserData>.Children => this.Children;
        IEnumerable<INode<TMachineUserData, TNodeUserData>> INode<TMachineUserData, TNodeUserData>.Descendants => this.Descendants;
        IEnumerable<INode<TMachineUserData, TNodeUserData>> INode<TMachineUserData, TNodeUserData>.DescendantsAndSelf => this.DescendantsAndSelf;

        // OnAttach
        event Action<object?>? INode<TMachineUserData, TNodeUserData>.OnAttachCallback {
            add => this.OnAttachCallback += value;
            remove => this.OnAttachCallback -= value;
        }
        event Action<object?>? INode<TMachineUserData, TNodeUserData>.OnDetachCallback {
            add => this.OnDetachCallback += value;
            remove => this.OnDetachCallback -= value;
        }

        // OnActivate
        event Action<object?>? INode<TMachineUserData, TNodeUserData>.OnActivateCallback {
            add => this.OnActivateCallback += value;
            remove => this.OnActivateCallback -= value;
        }
        event Action<object?>? INode<TMachineUserData, TNodeUserData>.OnDeactivateCallback {
            add => this.OnDeactivateCallback += value;
            remove => this.OnDeactivateCallback -= value;
        }

    }
    public sealed partial class Node<TMachineUserData, TNodeUserData> {

        // Attach
        void INode<TMachineUserData, TNodeUserData>.Attach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void INode<TMachineUserData, TNodeUserData>.Attach(INode<TMachineUserData, TNodeUserData> parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void INode<TMachineUserData, TNodeUserData>.Detach(ITreeMachine<TMachineUserData, TNodeUserData> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void INode<TMachineUserData, TNodeUserData>.Detach(INode<TMachineUserData, TNodeUserData> parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void INode<TMachineUserData, TNodeUserData>.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void INode<TMachineUserData, TNodeUserData>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        // OnAttach
        void INode<TMachineUserData, TNodeUserData>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        // OnDetach
        void INode<TMachineUserData, TNodeUserData>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        // OnActivate
        void INode<TMachineUserData, TNodeUserData>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        // OnDeactivate
        void INode<TMachineUserData, TNodeUserData>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void INode<TMachineUserData, TNodeUserData>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
}
