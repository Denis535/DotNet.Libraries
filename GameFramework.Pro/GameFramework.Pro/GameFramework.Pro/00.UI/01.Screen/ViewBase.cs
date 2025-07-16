namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    public abstract class ViewBase : IDisposable {

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

        public ViewBase() {
        }
        ~ViewBase() {
            Assert.Operation.Valid( $"Disposable '{this}' must be disposed", this.IsDisposed );
        }
        public virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Disposable {this} must be non-disposed", !this.IsDisposed );
            this.disposeCancellationTokenSource?.Cancel();
            this.IsDisposed = true;
        }

    }
}
