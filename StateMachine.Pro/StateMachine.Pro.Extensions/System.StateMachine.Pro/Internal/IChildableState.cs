#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

        // IsDisposed
        bool IState<TMachineUserData, TStateUserData>.IsDisposed => this.IsDisposed;

        // UserData
        TStateUserData IState<TMachineUserData, TStateUserData>.UserData => this.UserData;

        // OnDispose
        event Action? IState<TMachineUserData, TStateUserData>.OnDisposeCallback {
            add => this.OnDisposeCallback += value;
            remove => this.OnDisposeCallback -= value;
        }

        // Dispose
        void IState<TMachineUserData, TStateUserData>.Dispose() {
            this.Dispose();
        }

    }
    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

        // Machine
        IStateMachine<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Machine => this.Machine;
        IStateMachine<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Machine_NoRecursive => this.Machine_NoRecursive;

        // Root
        bool IState<TMachineUserData, TStateUserData>.IsRoot => this.IsRoot;
        IState<TMachineUserData, TStateUserData> IState<TMachineUserData, TStateUserData>.Root => this.Root;

        // Parent
        IState<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Parent => this.Parent;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Ancestors => this.Ancestors;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.AncestorsAndSelf => this.AncestorsAndSelf;

        // Activity
        Activity IState<TMachineUserData, TStateUserData>.Activity => this.Activity;

        // Children
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Children {
            get {
                if (this.Child != null) {
                    return new[] { this.Child };
                } else {
                    return Enumerable.Empty<IState<TMachineUserData, TStateUserData>>();
                }
            }
        }
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Descendants => this.Descendants;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.DescendantsAndSelf => this.DescendantsAndSelf;

        // OnAttach
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnAttachCallback {
            add => this.OnAttachCallback += value;
            remove => this.OnAttachCallback -= value;
        }
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnDetachCallback {
            add => this.OnDetachCallback += value;
            remove => this.OnDetachCallback -= value;
        }

        // OnActivate
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnActivateCallback {
            add => this.OnActivateCallback += value;
            remove => this.OnActivateCallback -= value;
        }
        event Action<object?>? IState<TMachineUserData, TStateUserData>.OnDeactivateCallback {
            add => this.OnDeactivateCallback += value;
            remove => this.OnDeactivateCallback -= value;
        }

    }
    public sealed partial class ChildableState<TMachineUserData, TStateUserData> {

        // Attach
        void IState<TMachineUserData, TStateUserData>.Attach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void IState<TMachineUserData, TStateUserData>.Attach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void IState<TMachineUserData, TStateUserData>.Detach(IStateMachine<TMachineUserData, TStateUserData> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void IState<TMachineUserData, TStateUserData>.Detach(IState<TMachineUserData, TStateUserData> parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void IState<TMachineUserData, TStateUserData>.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void IState<TMachineUserData, TStateUserData>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        // OnAttach
        void IState<TMachineUserData, TStateUserData>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        // OnDetach
        void IState<TMachineUserData, TStateUserData>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        // OnActivate
        void IState<TMachineUserData, TStateUserData>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        // OnDeactivate
        void IState<TMachineUserData, TStateUserData>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IState<TMachineUserData, TStateUserData>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
}
