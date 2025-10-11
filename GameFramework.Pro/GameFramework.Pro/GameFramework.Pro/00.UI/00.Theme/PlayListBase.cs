#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase {

        private readonly State<ThemeBase, PlayListBase> m_State;

        protected ThemeBase? Theme {
            get {
                Assert.Operation.Valid( $"State '{this.State}' must be disposed", this.State.IsDisposed );
                return this.State.Machine?.UserData;
            }
        }
        public IState<ThemeBase, PlayListBase> State {
            get {
                Assert.Operation.Valid( $"State '{this.State}' must be disposed", this.State.IsDisposed );
                return this.m_State;
            }
        }
        protected State<ThemeBase, PlayListBase> StateMutable {
            get {
                Assert.Operation.Valid( $"State '{this.State}' must be disposed", this.State.IsDisposed );
                return this.m_State;
            }
        }

        public PlayListBase() {
            this.m_State = new State<ThemeBase, PlayListBase>( this );
            this.State.OnBeforeDisposeCallback += this.OnDispose;
            this.State.OnActivateCallback += this.OnActivate;
            this.State.OnDeactivateCallback += this.OnDeactivate;
        }
        ~PlayListBase() {
            Trace.Assert( this.State.IsDisposed, $"State '{this.State}' must be disposed" );
        }
        protected virtual void OnDispose() {
            Assert.Operation.NotDisposed( $"State {this.State} must be disposing", this.State.IsDisposing ); // This method must only be called by IState.OnDisposeCallback
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
