#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase {

        private readonly State<ThemeBase, PlayListBase> m_State;

        public bool IsDisposed {
            get {
                return this.m_State.IsDisposed;
            }
        }

        protected ThemeBase? Theme {
            get {
                Assert.Operation.Valid( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State.Machine?.UserData;
            }
        }
        public IState<ThemeBase, PlayListBase> State {
            get {
                Assert.Operation.Valid( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }
        protected State<ThemeBase, PlayListBase> StateMutable {
            get {
                Assert.Operation.Valid( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }

        public PlayListBase() {
            this.m_State = new State<ThemeBase, PlayListBase>( this );
            this.m_State.OnBeforeDisposeCallback += this.OnDispose;
            this.m_State.OnActivateCallback += this.OnActivate;
            this.m_State.OnDeactivateCallback += this.OnDeactivate;
        }
        protected virtual void OnDispose() {
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
