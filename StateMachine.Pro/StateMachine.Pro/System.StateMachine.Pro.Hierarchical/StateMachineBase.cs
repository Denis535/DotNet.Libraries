#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateMachineBase<T> where T : notnull, StateBase<T> {

        // State
        protected T? State { get; private set; }

        // Constructor
        public StateMachineBase() {
        }

        // SetState
        protected virtual void SetState(T? state, object? argument, Action<T, object?>? callback) {
            if (this.State != null) {
                this.RemoveState( this.State, argument, callback );
            }
            if (state != null) {
                this.AddState( state, argument );
            }
        }
        protected virtual void AddState(T state, object? argument) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Machine_NoRecursive} machine", state.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Parent} parent", state.Parent == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be inactive", state.Activity == Activity.Inactive );
            Assert.Argument.Valid( $"Machine {this} must have no {this.State} state", this.State == null );
            this.State = state;
            this.State.Attach( this, argument );
        }
        protected internal virtual void RemoveState(T state, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have {this} machine", state.Machine_NoRecursive == this );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Parent} parent", state.Parent == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be active", state.Activity == Activity.Active );
            Assert.Argument.Valid( $"Machine {this} must have {state} state", this.State == state );
            this.State.Detach( this, argument );
            this.State = null;
            callback?.Invoke( state, argument );
        }
        protected void RemoveState(object? argument, Action<T, object?>? callback) {
            Assert.Operation.Valid( $"Machine {this} must have state", this.State != null );
            this.RemoveState( this.State, argument, callback );
        }

    }
}
