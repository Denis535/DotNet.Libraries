#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class ChildrenableState<TMachineUserData, TStateUserData> {

        // IsDisposed
        bool IState<TMachineUserData, TStateUserData>.IsDisposing => this.IsDisposing;
        bool IState<TMachineUserData, TStateUserData>.IsDisposed => this.IsDisposed;

        // Owner
        object? IState<TMachineUserData, TStateUserData>.Owner => this.Owner;

        // Machine
        IStateMachine<TMachineUserData, TStateUserData>? IState<TMachineUserData, TStateUserData>.Machine => this.Machine;

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
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Children => this.Children;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.Descendants => this.Descendants;
        IEnumerable<IState<TMachineUserData, TStateUserData>> IState<TMachineUserData, TStateUserData>.DescendantsAndSelf => this.DescendantsAndSelf;

        // UserData
        TStateUserData IState<TMachineUserData, TStateUserData>.UserData => this.UserData;

        // OnDispose
        event Action? IState<TMachineUserData, TStateUserData>.OnBeforeDisposeCallback {
            add => this.OnBeforeDisposeCallback += value;
            remove => this.OnBeforeDisposeCallback -= value;
        }
        event Action? IState<TMachineUserData, TStateUserData>.OnAfterDisposeCallback {
            add => this.OnAfterDisposeCallback += value;
            remove => this.OnAfterDisposeCallback -= value;
        }

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
    public sealed partial class ChildrenableState<TMachineUserData, TStateUserData> {

        // Dispose
        void IState<TMachineUserData, TStateUserData>.Dispose() {
            this.Dispose();
        }

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

    }
}
