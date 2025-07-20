#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {

        // Owner
        private StateMachineBase<TThis>? Owner { get; set; }

        // Machine
        public StateMachineBase<TThis>? Machine => this.Owner;

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
        internal void Attach(StateMachineBase<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have no {this.Machine} machine", this.Machine == null );
            Assert.Operation.Valid( $"State {this} must be inactive", this.Activity is Activity.Inactive );
            {
                this.Owner = machine;
                {
                    this.OnBeforeAttachCallback?.Invoke( argument );
                    this.OnBeforeAttach( argument );
                }
                this.OnAttach( argument );
                {
                    this.OnAfterAttach( argument );
                    this.OnAfterAttachCallback?.Invoke( argument );
                }
            }
            {
                this.Activate( argument );
            }
        }

        // Detach
        internal void Detach(StateMachineBase<TThis> machine, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Operation.Valid( $"State {this} must have {machine} machine", this.Machine == machine );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                {
                    this.OnBeforeDetachCallback?.Invoke( argument );
                    this.OnBeforeDetach( argument );
                }
                this.OnDetach( argument );
                {
                    this.OnAfterDetach( argument );
                    this.OnAfterDetachCallback?.Invoke( argument );
                }
                this.Owner = null;
            }
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
        }
        protected virtual void OnAfterAttach(object? argument) {
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
        }
        protected virtual void OnAfterDetach(object? argument) {
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
            {
                this.OnBeforeActivateCallback?.Invoke( argument );
                this.OnBeforeActivate( argument );
            }
            {
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                this.Activity = Activity.Active;
            }
            {
                this.OnAfterActivate( argument );
                this.OnAfterActivateCallback?.Invoke( argument );
            }
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"State {this} must have machine", this.Machine != null );
            Assert.Operation.Valid( $"State {this} must be active", this.Activity is Activity.Active );
            {
                this.OnBeforeDeactivateCallback?.Invoke( argument );
                this.OnBeforeDeactivate( argument );
            }
            {
                this.Activity = Activity.Deactivating;
                this.OnDeactivate( argument );
                this.Activity = Activity.Inactive;
            }
            {
                this.OnAfterDeactivate( argument );
                this.OnAfterDeactivateCallback?.Invoke( argument );
            }
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
        }
        protected virtual void OnAfterActivate(object? argument) {
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
        }
        protected virtual void OnAfterDeactivate(object? argument) {
        }

    }
}
