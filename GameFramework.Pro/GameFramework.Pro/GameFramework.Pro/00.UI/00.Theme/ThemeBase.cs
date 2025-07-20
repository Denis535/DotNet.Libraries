namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {
        private sealed class StateMachine : StateMachineBase<PlayListBase.State_>, IDisposable {

            public new PlayListBase.State_? State => base.State;

            public StateMachine() {
            }
            public void Dispose() {
                Assert.Operation.Valid( $"StateMachine {this} must have no {this.State} state", this.State == null );
            }

            public new void SetState(PlayListBase.State_? state, object? argument, Action<PlayListBase.State_, object?>? callback) {
                base.SetState( state, argument, callback );
            }
            public new void AddState(PlayListBase.State_ state, object? argument) {
                base.AddState( state, argument );
            }
            public new void RemoveState(PlayListBase.State_ state, object? argument, Action<PlayListBase.State_, object?>? callback) {
                base.RemoveState( state, argument, callback );
            }
            public new void RemoveState(object? argument, Action<PlayListBase.State_, object?>? callback) {
                base.RemoveState( argument, callback );
            }

        }

        private StateMachine Machine { get; }

        protected PlayListBase? State => this.Machine.State?.Owner;

        public ThemeBase() {
            this.Machine = new StateMachine();
        }
        public override void Dispose() {
            this.Machine.Dispose();
            base.Dispose();
        }

        protected virtual void SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback) {
            this.Machine.SetState( state?.State, argument, (state, arg) => callback?.Invoke( state.Owner, arg ) );
        }
        protected virtual void AddState(PlayListBase state, object? argument) {
            this.Machine.AddState( state.State, argument );
        }
        protected virtual void RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback) {
            this.Machine.RemoveState( state.State, argument, (state, arg) => callback?.Invoke( state.Owner, arg ) );
        }
        protected virtual void RemoveState(object? argument, Action<PlayListBase, object?>? callback) {
            this.Machine.RemoveState( argument, (state, arg) => callback?.Invoke( state.Owner, arg ) );
        }

    }
}
