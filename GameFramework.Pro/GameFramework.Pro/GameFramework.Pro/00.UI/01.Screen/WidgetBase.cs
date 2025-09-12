#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : DisposableBase {

        internal WidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class WidgetBase<TThis> : WidgetBase where TThis : WidgetBase {

        public Node2<TThis> Node { get; }

        public WidgetBase() {
            this.Node = new Node2<TThis>( (TThis) (object) this ) {
                SortDelegate = this.Sort,
            };
            this.Node.OnActivateCallback += this.OnActivate;
            this.Node.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Widget {this} must be inactive", this.Node.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"Widget {this} must have no children", !this.Node.Children.Any() );
            base.Dispose();
        }

        protected virtual void OnActivate(object? argument) {
        }
        protected virtual void OnDeactivate(object? argument) {
        }

        protected virtual void Sort(List<NodeBase> children) {
        }

    }
    public abstract class ViewableWidgetBase<TThis> : WidgetBase<TThis> where TThis : WidgetBase {

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
    public abstract class ViewableWidgetBase<TThis, TView> : ViewableWidgetBase<TThis> where TThis : WidgetBase where TView : notnull {

        protected new TView View { get => (TView) base.View; init => base.View = value; }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
