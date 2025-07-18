#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateMachineBase<T> : IStateMachine<T> where T : class, IStateBase<T> {

        // State
        T? IStateMachine<T>.State { get => this.State; set => this.State = value; }

        // State
        protected T? State { get; private set; }

        // Constructor
        public StateMachineBase() {
        }

        // SetState
        void IStateMachine<T>.SetState(T? state, object? argument, Action<T, object?>? callback) {
            this.SetState( state, argument, callback );
        }
        void IStateMachine<T>.AddState(T state, object? argument) {
            this.AddState( state, argument );
        }
        void IStateMachine<T>.RemoveState(T state, object? argument, Action<T, object?>? callback) {
            this.RemoveState( state, argument, callback );
        }
        void IStateMachine<T>.RemoveState(object? argument, Action<T, object?>? callback) {
            this.RemoveState( argument, callback );
        }

        // SetState
        protected virtual void SetState(T? state, object? argument, Action<T, object?>? callback) {
            IStateMachine<T>.SetState( this, state, argument, callback );
        }
        protected virtual void AddState(T state, object? argument) {
            IStateMachine<T>.AddState( this, state, argument );
        }
        protected virtual void RemoveState(T state, object? argument, Action<T, object?>? callback) {
            IStateMachine<T>.RemoveState( this, state, argument, callback );
        }
        protected virtual void RemoveState(object? argument, Action<T, object?>? callback) {
            IStateMachine<T>.RemoveState( this, argument, callback );
        }

    }
}
