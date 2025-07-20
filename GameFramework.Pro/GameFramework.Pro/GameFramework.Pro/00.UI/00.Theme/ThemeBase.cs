namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {
        private sealed class StateMachine_ : StateMachineBase<PlayListBase.State_>, IDisposable {

            public new PlayListBase? State => base.State?.PlayList;

            public StateMachine_() {
            }
            public void Dispose() {
                Assert.Operation.Valid( $"StateMachine {this} must have no {this.State} state", this.State == null );
            }

            public void SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback) {
                base.SetState( state?.State, argument, (state, arg) => callback?.Invoke( state.PlayList, arg ) );
            }
            public void AddState(PlayListBase state, object? argument) {
                base.AddState( state.State, argument );
            }
            public void RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback) {
                base.RemoveState( state.State, argument, (state, arg) => callback?.Invoke( state.PlayList, arg ) );
            }
            public void RemoveState(object? argument, Action<PlayListBase, object?>? callback) {
                base.RemoveState( argument, (state, arg) => callback?.Invoke( state.PlayList, arg ) );
            }

        }

        private StateMachine_ StateMachine { get; }

        protected PlayListBase? State => this.StateMachine.State;

        public ThemeBase() {
            this.StateMachine = new StateMachine_();
        }
        public override void Dispose() {
            this.StateMachine.Dispose();
            base.Dispose();
        }

        protected virtual void SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback) {
            this.StateMachine.SetState( state, argument, callback );
        }
        protected virtual void AddState(PlayListBase state, object? argument) {
            this.StateMachine.AddState( state, argument );
        }
        protected virtual void RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback) {
            this.StateMachine.RemoveState( state, argument, callback );
        }
        protected virtual void RemoveState(object? argument, Action<PlayListBase, object?>? callback) {
            this.StateMachine.RemoveState( argument, callback );
        }

    }
}
