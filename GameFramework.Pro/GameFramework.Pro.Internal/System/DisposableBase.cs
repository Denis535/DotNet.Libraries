#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public abstract class DisposableBase : IDisposable {
        private enum Lifecycle {
            Alive,
            Disposing,
            Disposed,
        }

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private CancellationTokenSource? m_DisposeCancellationTokenSource = null;

        public bool IsDisposing {
            get {
                return this.m_Lifecycle == Lifecycle.Disposing;
            }
        }
        public bool IsDisposed {
            get {
                return this.m_Lifecycle == Lifecycle.Disposed;
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

        public DisposableBase() {
        }
        public void Dispose() {
            Assert.Operation.NotDisposed( $"Disposable {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                this.OnDispose();
                this.m_DisposeCancellationTokenSource?.Cancel();
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }
        protected abstract void OnDispose();

    }
}
