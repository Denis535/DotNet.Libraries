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
            this.Node = new Node2<WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.Node.OnActivateCallback += this.OnActivate;
            this.Node.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Widget {this} must have no children", !this.Node.Children.Any() );
            Assert.Operation.Valid( $"Widget {this} must be inactive", this.Node.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected virtual void OnActivate(object? argument) {
        }
        protected virtual void OnDeactivate(object? argument) {
        }

        protected virtual void Sort(List<Node2<WidgetBase>> children) {
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        protected object View { get; init; } = default!;

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            if (this.View is DisposableBase view) {
                Assert.Operation.Valid( $"View {view} must be disposed", view.IsDisposed );
            }
            base.Dispose();
        }

    }
    public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : notnull {

        protected new TView View { get => (TView) base.View; init => base.View = value; }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
