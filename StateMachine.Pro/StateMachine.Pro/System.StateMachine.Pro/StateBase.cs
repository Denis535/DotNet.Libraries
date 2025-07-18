#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> : IStateBase<TThis> where TThis : notnull, StateBase<TThis> {

        IStateMachine<TThis>? IStateBase<TThis>.Owner => this.Owner;

        void IStateBase<TThis>.Attach(IStateMachine<TThis> machine, object? argument) {
            this.Attach( machine, argument );
        }

        void IStateBase<TThis>.Detach(IStateMachine<TThis> machine, object? argument) {
            this.Detach( machine, argument );
        }

        void IStateBase<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IStateBase<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IStateBase<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void IStateBase<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IStateBase<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IStateBase<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        void IStateBase<TThis>.Activate(object? argument) {
            this.Activate( argument );
        }

        void IStateBase<TThis>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        void IStateBase<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IStateBase<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IStateBase<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void IStateBase<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IStateBase<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IStateBase<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // Owner
        private IStateMachine<TThis>? Owner { get; set; }

        // Machine
        public IStateMachine<TThis>? Machine => this.Owner;

        // Activity
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Constructor
        public StateBase() {
        }

    }
    public abstract partial class StateBase<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Attach
        internal void Attach(IStateMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine} machine", this.Machine == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            {
                this.Owner = machine;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            {
                this.Activate( argument );
            }
        }

        // Detach
        internal void Detach(IStateMachine<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have {machine} machine", this.Machine == machine );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
            this.OnBeforeAttachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            this.OnAfterAttachCallback?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            this.OnBeforeDetachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            this.OnAfterDetachCallback?.Invoke( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have machine", this.Machine != null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            this.OnBeforeActivate( argument );
            this.Activity = Activity.Activating;
            {
                this.OnActivate( argument );
            }
            this.Activity = Activity.Active;
            this.OnAfterActivate( argument );
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have machine", this.Machine != null );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            this.OnBeforeDeactivate( argument );
            this.Activity = Activity.Deactivating;
            {
                this.OnDeactivate( argument );
            }
            this.Activity = Activity.Inactive;
            this.OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            this.OnBeforeActivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateCallback?.Invoke( argument );
        }

    }
}
