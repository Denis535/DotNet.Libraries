#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public abstract class DisposableBase : IDisposable {

        private CancellationTokenSource? m_DisposeCancellationTokenSource;

        public bool IsDisposed { get; private set; }
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
        ~DisposableBase() {
            Assert.Operation.Valid( $"Disposable '{this}' must be disposed", this.IsDisposed );
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Disposable {this} must be non-disposed", !this.IsDisposed );
            this.m_DisposeCancellationTokenSource?.Cancel();
            this.IsDisposed = true;
        }

    }
}
