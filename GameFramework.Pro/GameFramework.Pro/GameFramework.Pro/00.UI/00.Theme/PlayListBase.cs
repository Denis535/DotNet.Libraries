namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : DisposableBase {
        internal sealed class State_ : StateBase<State_> {

            internal PlayListBase Owner { get; }

            public State_(PlayListBase owner) {
                this.Owner = owner;
            }

            protected override void OnAttach(object? argument) {
                this.Owner.OnAttach( argument );
            }
            protected override void OnDetach(object? argument) {
                this.Owner.OnDetach( argument );
            }

            protected override void OnActivate(object? argument) {
                this.Owner.OnActivate( argument );
            }
            protected override void OnDeactivate(object? argument) {
                this.Owner.OnDeactivate( argument );
            }

        }

        internal State_ State { get; }

        internal StateMachineBase<State_>? Machine => this.State.Machine;

        public Activity Activity => this.State.Activity;

        public PlayListBase() {
            this.State = new State_( this );
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Disposable {this} must be inactive", this.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected virtual void OnAttach(object? argument) {
        }
        protected virtual void OnDetach(object? argument) {
        }

        protected virtual void OnActivate(object? argument) {
        }
        protected virtual void OnDeactivate(object? argument) {
        }

    }
}
