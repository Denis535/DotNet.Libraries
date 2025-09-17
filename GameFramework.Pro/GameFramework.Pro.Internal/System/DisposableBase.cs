#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public abstract class DisposableBase : IDisposable {

        private CancellationTokenSource? disposeCancellationTokenSource;

        public bool IsDisposed { get; private set; }
        public CancellationToken DisposeCancellationToken {
            get {
                if (this.disposeCancellationTokenSource == null) {
                    this.disposeCancellationTokenSource = new CancellationTokenSource();
                    if (this.IsDisposed) this.disposeCancellationTokenSource.Cancel();
                }
                return this.disposeCancellationTokenSource.Token;
            }
        }

        public DisposableBase() {
        }
        ~DisposableBase() {
            Assert.Operation.Valid( $"Disposable '{this}' must be disposed", this.IsDisposed );
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Disposable {this} must be non-disposed", !this.IsDisposed );
            this.disposeCancellationTokenSource?.Cancel();
            this.IsDisposed = true;
        }

    }
}
