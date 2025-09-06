#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateMachineBase {

        // State
        protected IState? State { get; private set; }

        // Constructor
        public StateMachineBase() {
        }

        // SetState
        protected virtual void SetState(IState? state, object? argument, Action<IState, object?>? callback) {
            if (this.State != null) {
                this.RemoveState( this.State, argument, callback );
            }
            if (state != null) {
                this.AddState( state, argument );
            }
        }
        protected virtual void AddState(IState state, object? argument) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Machine} machine", state.Machine == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be inactive", state.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"StateMachine {this} must have no {this.State} state", this.State == null );
            this.State = state;
            this.State.Attach( this, argument );
        }
        protected virtual void RemoveState(IState state, object? argument, Action<IState, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have {this} machine", state.Machine == this );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be active", state.Activity == Activity.Active );
            Assert.Operation.Valid( $"StateMachine {this} must have {state} state", this.State == state );
            this.State.Detach( this, argument );
            this.State = null;
            callback?.Invoke( state, argument );
        }
        protected void RemoveState(object? argument, Action<IState, object?>? callback) {
            Assert.Operation.Valid( $"StateMachine {this} must have state", this.State != null );
            this.RemoveState( this.State, argument, callback );
        }

    }
}
