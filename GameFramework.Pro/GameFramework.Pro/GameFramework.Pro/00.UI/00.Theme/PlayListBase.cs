#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase {

        private readonly State<ThemeBase, PlayListBase> m_State;

        public bool IsDisposing {
            get {
                return this.m_State.IsDisposing;
            }
        }
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
            this.m_State = new State<ThemeBase, PlayListBase>( this ) {
                OnDisposeCallback = this.OnDispose,
                OnActivateCallback = this.OnActivate,
                OnDeactivateCallback = this.OnDeactivate
            };
        }
        protected abstract void OnDispose();

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
