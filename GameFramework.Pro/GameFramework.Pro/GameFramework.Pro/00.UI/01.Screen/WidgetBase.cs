#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : DisposableBase {

        public Node2<WidgetBase> Node { get; }

        public WidgetBase() {
            this.Node = new Node2<WidgetBase>( this );
            this.Node.OnAttachCallback += this.OnAttach;
            this.Node.OnDetachCallback += this.OnDetach;
            this.Node.OnActivateCallback += this.OnActivate;
            this.Node.OnDeactivateCallback += this.OnDeactivate;
            this.Node.OnBeforeDescendantAttachCallback += this.OnBeforeDescendantAttach;
            this.Node.OnAfterDescendantAttachCallback += this.OnAfterDescendantAttach;
            this.Node.OnBeforeDescendantDetachCallback += this.OnBeforeDescendantDetach;
            this.Node.OnAfterDescendantDetachCallback += this.OnAfterDescendantDetach;
            this.Node.OnBeforeDescendantActivateCallback += this.OnBeforeDescendantActivate;
            this.Node.OnAfterDescendantActivateCallback += this.OnAfterDescendantActivate;
            this.Node.OnBeforeDescendantDeactivateCallback += this.OnBeforeDescendantDeactivate;
            this.Node.OnAfterDescendantDeactivateCallback += this.OnAfterDescendantDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Widget {this} must have no children", !this.Node.Children.Any() );
            Assert.Operation.Valid( $"Widget {this} must be inactive", this.Node.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected virtual void OnAttach(object? argument) {
        }
        protected virtual void OnDetach(object? argument) {
        }

        protected virtual void OnActivate(object? argument) {
        }
        protected virtual void OnDeactivate(object? argument) {
        }

        protected virtual void OnBeforeDescendantAttach(Node2<WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantAttach(Node2<WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDetach(Node2<WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDetach(Node2<WidgetBase> descendant, object? argument) {
        }

        protected virtual void OnBeforeDescendantActivate(Node2<WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(Node2<WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(Node2<WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(Node2<WidgetBase> descendant, object? argument) {
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        protected IView View { get; init; } = default!;

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            if (this.View is DisposableBase view) {
                Assert.Operation.Valid( $"View {view} must be disposed", view.IsDisposed );
            }
            base.Dispose();
        }

    }
    public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : notnull, IView {

        protected new TView View { get => (TView) base.View; init => base.View = value; }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public interface IView {
    }
}
