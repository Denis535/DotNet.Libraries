namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.TreeMachine;

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

        protected virtual void ShowSelf() {
            Assert.Operation.Valid( $"Widget {this} must have parent", this.Parent != null );
            this.Parent.ShowWidgetRecursive( this );
        }
        protected virtual void HideSelf() {
            Assert.Operation.Valid( $"Widget {this} must have parent", this.Parent != null );
            this.Parent.HideWidgetRecursive( this );
        }

        protected void ShowWidget(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            var wasShown = this.TryShowWidget( widget );
            Assert.Operation.Valid( $"Can not show {widget} widget", wasShown );
        }
        protected void ShowWidgetRecursive(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            var wasShown = this.TryShowWidgetRecursive( widget );
            Assert.Operation.Valid( $"Can not show {widget} widget", wasShown );
        }
        protected void HideWidget(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            var wasHidden = this.TryHideWidget( widget );
            Assert.Operation.Valid( $"Can not hide {widget} widget", wasHidden );
        }
        protected void HideWidgetRecursive(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            var wasHidden = this.TryHideWidgetRecursive( widget );
            Assert.Operation.Valid( $"Can not hide {widget} widget", wasHidden );
        }

        protected virtual bool TryShowWidget(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            return false; // You can override this method to show widget
        }
        protected bool TryShowWidgetRecursive(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            if (this.TryShowWidget( widget )) {
                return true;
            }
            if (this.Parent != null) {
                return this.Parent.TryShowWidgetRecursive( widget );
            }
            return false;
        }
        protected virtual bool TryHideWidget(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            return false; // You can override this method to hide widget
        }
        protected bool TryHideWidgetRecursive(WidgetBase widget) {
            Assert.Argument.NotNull( $"Argument 'widget' must be non-null", widget != null );
            if (this.TryHideWidget( widget )) {
                return true;
            }
            if (this.Parent != null) {
                return this.Parent.TryHideWidgetRecursive( widget );
            }
            return false;
        }

    }
}
