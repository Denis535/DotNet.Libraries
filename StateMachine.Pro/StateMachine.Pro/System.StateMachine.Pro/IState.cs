#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface IState<TThis> where TThis : class, IState<TThis> {

        // Owner
        protected IStateMachine<TThis>? Owner { get; set; }

        // Machine
        public sealed IStateMachine<TThis>? Machine => this.Owner;

        // Activity
        public Activity Activity { get; protected set; }

    }
    public partial interface IState<TThis> {

        // OnAttach
        public Action<object?>? OnBeforeAttachCallback { get; set; }
        public Action<object?>? OnAfterAttachCallback { get; set; }
        public Action<object?>? OnBeforeDetachCallback { get; set; }
        public Action<object?>? OnAfterDetachCallback { get; set; }

        // Attach
        internal sealed void Attach(IStateMachine<TThis> machine, object? argument) {
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
        internal sealed void Detach(IStateMachine<TThis> machine, object? argument) {
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
        protected void OnAttach(object? argument);
        protected void OnBeforeAttach(object? argument);
        protected void OnAfterAttach(object? argument);

        // OnDetach
        protected void OnDetach(object? argument);
        protected void OnBeforeDetach(object? argument);
        protected void OnAfterDetach(object? argument);

    }
    public partial interface IState<TThis> {

        // OnActivate
        public Action<object?>? OnBeforeActivateCallback { get; set; }
        public Action<object?>? OnAfterActivateCallback { get; set; }
        public Action<object?>? OnBeforeDeactivateCallback { get; set; }
        public Action<object?>? OnAfterDeactivateCallback { get; set; }

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
        protected void OnActivate(object? argument);
        protected void OnBeforeActivate(object? argument);
        protected void OnAfterActivate(object? argument);

        // OnDeactivate
        protected void OnDeactivate(object? argument);
        protected void OnBeforeDeactivate(object? argument);
        protected void OnAfterDeactivate(object? argument);

    }
}
