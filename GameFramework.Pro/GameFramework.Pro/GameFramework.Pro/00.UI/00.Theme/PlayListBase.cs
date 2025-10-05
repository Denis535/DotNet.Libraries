#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.StateMachine.Pro;
    using System.Text;
    using System.Threading;

    public abstract class PlayListBase {

        private bool m_IsDisposed;
        private CancellationTokenSource? m_DisposeCancellationTokenSource;
        private readonly State<ThemeBase, PlayListBase> m_State;

        public bool IsDisposed {
            get {
                return this.m_IsDisposed;
            }
        }
        public CancellationToken DisposeCancellationToken {
            get {
                if (this.m_DisposeCancellationTokenSource == null) {
                    this.m_DisposeCancellationTokenSource = new CancellationTokenSource();
                    if (this.IsDisposed) this.m_DisposeCancellationTokenSource.Cancel();
                }
                return this.m_DisposeCancellationTokenSource.Token;
            }
        }

        protected ThemeBase? Theme {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.State.Machine?.UserData;
            }
        }
        public IState<ThemeBase, PlayListBase> State {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }
        protected State<ThemeBase, PlayListBase> StateMutable {
            get {
                Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
                return this.m_State;
            }
        }

        public PlayListBase() {
            this.m_State = new State<ThemeBase, PlayListBase>( this );
            this.State.OnActivateCallback += this.OnActivate;
            this.State.OnDeactivateCallback += this.OnDeactivate;
            this.State.OnDisposeCallback += this.Dispose;
        }
        ~PlayListBase() {
            Trace.Assert( this.IsDisposed, $"PlayList '{this}' must be disposed" );
        }
        protected virtual void Dispose() {
            Assert.Operation.NotDisposed( $"PlayList {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.NotDisposed( $"State {this.State} must be disposing", this.State.IsDisposing ); // This method must only be called by IState.OnDisposeCallback
            this.m_DisposeCancellationTokenSource?.Cancel();
            this.m_IsDisposed = true;
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
