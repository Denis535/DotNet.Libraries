namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : NodeBase2<WidgetBase>, IDisposable {

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

        public ScreenBase? Screen => (ScreenBase?) base.Machine;

        public WidgetBase() {
        }
        ~WidgetBase() {
            Assert.Operation.Valid( $"Disposable '{this}' must be disposed", this.IsDisposed );
        }
        public virtual void Dispose() {
            foreach (var child in base.Children) {
                Assert.Operation.Valid( $"Child {child} must be disposed", child.IsDisposed );
            }
            Assert.Operation.NotDisposed( $"Disposable {this} must be non-disposed", !this.IsDisposed );
            this.disposeCancellationTokenSource?.Cancel();
            this.IsDisposed = true;
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        protected ViewBase View { get; init; } = default!;

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"View {this.View} must be non-null", this.View != null );
            Assert.Operation.Valid( $"View {this.View} must be disposed", this.View.IsDisposed );
            base.Dispose();
        }

    }
    public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : ViewBase {

        protected new TView View { get => (TView) base.View; init => base.View = value; }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
