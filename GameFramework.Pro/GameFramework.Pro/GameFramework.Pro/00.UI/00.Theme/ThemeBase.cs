namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase, IStateMachine<PlayListBase> {

        PlayListBase? IStateMachine<PlayListBase>.State { get => this.State; set => this.State = value; }

        protected PlayListBase? State { get; private set; }

        public ThemeBase() {
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Theme {this} must have no {this.State} state", this.State == null );
            base.Dispose();
        }

        void IStateMachine<PlayListBase>.SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback) {
            this.SetState( state, argument, callback );
        }
        void IStateMachine<PlayListBase>.AddState(PlayListBase state, object? argument) {
            this.AddState( state, argument );
        }
        void IStateMachine<PlayListBase>.RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback) {
            this.RemoveState( state, argument, callback );
        }
        void IStateMachine<PlayListBase>.RemoveState(object? argument, Action<PlayListBase, object?>? callback) {
            this.RemoveState( argument, callback );
        }

        protected void SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback) {
            IStateMachine<PlayListBase>.SetState( this, state, argument, callback );
        }
        protected void AddState(PlayListBase state, object? argument) {
            IStateMachine<PlayListBase>.AddState( this, state, argument );
        }
        protected void RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback) {
            IStateMachine<PlayListBase>.RemoveState( this, state, argument, callback );
        }
        protected void RemoveState(object? argument, Action<PlayListBase, object?>? callback) {
            IStateMachine<PlayListBase>.SetState( this, null, argument, callback );
        }

    }
}
