#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : IDisposable {

        private readonly State<PlayListBase> m_State;

        public bool IsDisposed { get; private set; }

        protected ThemeBase? Theme {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return ((IStateMachine<ThemeBase>?) this.State.Machine)?.UserData;
            }
        }
        public IState State {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }
        protected State<PlayListBase> StateMutable {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }

        public PlayListBase() {
            this.m_State = new State<PlayListBase>( this );
            this.State.OnActivateCallback += this.OnActivate;
            this.State.OnDeactivateCallback += this.OnDeactivate;
        }
        ~PlayListBase() {
            Assert.Operation.Valid( $"PlayList '{this}' must be disposed", this.IsDisposed );
        }
        void IDisposable.Dispose() {
            // This method can only be called from state
            this.Dispose();
        }
        protected virtual void Dispose() {
            Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
            this.IsDisposed = true;
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
