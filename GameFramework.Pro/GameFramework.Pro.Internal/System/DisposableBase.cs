#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public abstract class DisposableBase : IDisposable {

        private bool m_IsDisposed = false;
        private CancellationTokenSource? m_DisposeCancellationTokenSource = null;

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

        public DisposableBase() {
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Disposable {this} must be non-disposed", !this.IsDisposed );
            this.m_DisposeCancellationTokenSource?.Cancel();
            this.m_IsDisposed = true;
        }

    }
}
