namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : DisposableBase {
        internal sealed class State_ : StateBase<State_> {

            internal PlayListBase PlayList { get; }

            public State_(PlayListBase playList) {
                this.PlayList = playList;
            }

            protected override void OnAttach(object? argument) {
                this.PlayList.OnAttach( argument );
            }
            protected override void OnDetach(object? argument) {
                this.PlayList.OnDetach( argument );
            }

            protected override void OnActivate(object? argument) {
                this.PlayList.OnActivate( argument );
            }
            protected override void OnDeactivate(object? argument) {
                this.PlayList.OnDeactivate( argument );
            }

        }

        internal State_ State { get; }

        public PlayListBase() {
            this.State = new State_( this );
        }
        public override void Dispose() {
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
