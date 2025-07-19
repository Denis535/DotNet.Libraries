#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateMachine<T> where T : class, IState<T> {

        // State
        protected T? State { get; set; }

        // SetState
        protected void SetState(T? state, object? argument, Action<T, object?>? callback);
        protected void AddState(T state, object? argument);
        protected internal void RemoveState(T state, object? argument, Action<T, object?>? callback);
        protected void RemoveState(object? argument, Action<T, object?>? callback);

        // Helpers
        protected static void SetState(IStateMachine<T> machine, T? state, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            if (machine.State != null) {
                machine.RemoveState( machine.State, argument, callback );
            }
            if (state != null) {
                machine.AddState( state, argument );
            }
        }
        protected static void AddState(IStateMachine<T> machine, T state, object? argument) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Argument.Valid( $"Argument 'machine' ({machine}) must have no {machine.State} state", machine.State == null );
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Machine_NoRecursive} machine", state.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Parent} parent", state.Parent == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be inactive", state.Activity == Activity.Inactive );
            machine.State = state;
            machine.State.Attach( machine, argument );
        }
        protected static void RemoveState(IStateMachine<T> machine, T state, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Argument.Valid( $"Argument 'machine' ({machine}) must have {state} state", machine.State == state );
            Assert.Argument.NotNull( $"Argument 'state' must be non-null", state != null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have {machine} machine", state.Machine_NoRecursive == machine );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must have no {state.Parent} parent", state.Parent == null );
            Assert.Argument.Valid( $"Argument 'state' ({state}) must be active", state.Activity == Activity.Active );
            machine.State.Detach( machine, argument );
            machine.State = null;
            callback?.Invoke( state, argument );
        }
        protected static void RemoveState(IStateMachine<T> machine, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Assert.Argument.Valid( $"Argument 'machine' ({machine}) must have state", machine.State != null );
            machine.RemoveState( machine.State, argument, callback );
        }

    }
}
