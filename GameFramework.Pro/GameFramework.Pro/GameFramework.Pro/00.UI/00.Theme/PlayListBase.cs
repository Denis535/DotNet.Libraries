#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : DisposableBase {

        private readonly State<PlayListBase> m_State;

        protected ThemeBase? Theme {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return ((IUserData<ThemeBase>?) this.State.Machine)?.UserData;
            }
        }
        public IState State {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }
        protected State StateMutable {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }

        public PlayListBase() {
            this.m_State = new State<PlayListBase>( this );
            this.m_State.OnActivateCallback += this.OnActivate;
            this.m_State.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
            if (!this.StateMutable.IsDisposed) {
                this.StateMutable.Dispose();
                return;
            }
            base.Dispose();
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
