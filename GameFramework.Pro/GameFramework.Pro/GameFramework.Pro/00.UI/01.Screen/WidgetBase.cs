namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : NodeBase<WidgetBase>, INode2<WidgetBase>, IDisposable {

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

        Action<WidgetBase, object?>? INode2<WidgetBase>.OnBeforeDescendantAttachCallback { get => this.OnBeforeDescendantAttachCallback; set => this.OnBeforeDescendantAttachCallback = value; }
        Action<WidgetBase, object?>? INode2<WidgetBase>.OnAfterDescendantAttachCallback { get => this.OnAfterDescendantAttachCallback; set => this.OnAfterDescendantAttachCallback = value; }
        Action<WidgetBase, object?>? INode2<WidgetBase>.OnBeforeDescendantDetachCallback { get => this.OnBeforeDescendantDetachCallback; set => this.OnBeforeDescendantDetachCallback = value; }
        Action<WidgetBase, object?>? INode2<WidgetBase>.OnAfterDescendantDetachCallback { get => this.OnAfterDescendantDetachCallback; set => this.OnAfterDescendantDetachCallback = value; }

        Action<WidgetBase, object?>? INode2<WidgetBase>.OnBeforeDescendantActivateCallback { get => this.OnBeforeDescendantActivateCallback; set => this.OnBeforeDescendantActivateCallback = value; }
        Action<WidgetBase, object?>? INode2<WidgetBase>.OnAfterDescendantActivateCallback { get => this.OnAfterDescendantActivateCallback; set => this.OnAfterDescendantActivateCallback = value; }
        Action<WidgetBase, object?>? INode2<WidgetBase>.OnBeforeDescendantDeactivateCallback { get => this.OnBeforeDescendantDeactivateCallback; set => this.OnBeforeDescendantDeactivateCallback = value; }
        Action<WidgetBase, object?>? INode2<WidgetBase>.OnAfterDescendantDeactivateCallback { get => this.OnAfterDescendantDeactivateCallback; set => this.OnAfterDescendantDeactivateCallback = value; }

        public Action<WidgetBase, object?>? OnBeforeDescendantAttachCallback { get; set; }
        public Action<WidgetBase, object?>? OnAfterDescendantAttachCallback { get; set; }
        public Action<WidgetBase, object?>? OnBeforeDescendantDetachCallback { get; set; }
        public Action<WidgetBase, object?>? OnAfterDescendantDetachCallback { get; set; }

        public Action<WidgetBase, object?>? OnBeforeDescendantActivateCallback { get; set; }
        public Action<WidgetBase, object?>? OnAfterDescendantActivateCallback { get; set; }
        public Action<WidgetBase, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
        public Action<WidgetBase, object?>? OnAfterDescendantDeactivateCallback { get; set; }

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

        void INode2<WidgetBase>.OnBeforeDescendantAttach(WidgetBase descendant, object? argument) {
            this.OnBeforeDescendantAttach( descendant, argument );
        }
        void INode2<WidgetBase>.OnAfterDescendantAttach(WidgetBase descendant, object? argument) {
            this.OnAfterDescendantAttach( descendant, argument );
        }
        void INode2<WidgetBase>.OnBeforeDescendantDetach(WidgetBase descendant, object? argument) {
            this.OnBeforeDescendantDetach( descendant, argument );
        }
        void INode2<WidgetBase>.OnAfterDescendantDetach(WidgetBase descendant, object? argument) {
            this.OnAfterDescendantDetach( descendant, argument );
        }

        void INode2<WidgetBase>.OnBeforeDescendantActivate(WidgetBase descendant, object? argument) {
            this.OnBeforeDescendantActivate( descendant, argument );
        }
        void INode2<WidgetBase>.OnAfterDescendantActivate(WidgetBase descendant, object? argument) {
            this.OnAfterDescendantActivate( descendant, argument );
        }
        void INode2<WidgetBase>.OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument) {
            this.OnBeforeDescendantDeactivate( descendant, argument );
        }
        void INode2<WidgetBase>.OnAfterDescendantDeactivate(WidgetBase descendant, object? argument) {
            this.OnAfterDescendantDeactivate( descendant, argument );
        }

        protected virtual void OnBeforeDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDetach(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDetach(WidgetBase descendant, object? argument) {
        }

        protected virtual void OnBeforeDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(WidgetBase descendant, object? argument) {
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
