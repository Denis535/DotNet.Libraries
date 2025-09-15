#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : DisposableBase {

        public Node2<WidgetBase> Node { get; }
        protected ScreenBase? Theme => ((TreeMachine<Node2<PlayListBase>, ScreenBase>?) this.Node.Machine)?.UserData;

        public WidgetBase() {
            this.Node = new Node2<WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.Node.OnActivateCallback += this.OnActivate;
            this.Node.OnDeactivateCallback += this.OnDeactivate;
            this.Node.OnBeforeDescendantActivateCallback += this.OnBeforeDescendantActivate;
            this.Node.OnAfterDescendantActivateCallback += this.OnAfterDescendantActivate;
            this.Node.OnBeforeDescendantDeactivateCallback += this.OnBeforeDescendantDeactivate;
            this.Node.OnAfterDescendantDeactivateCallback += this.OnAfterDescendantDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Widget {this} must be inactive", this.Node.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"Widget {this} must have no children", this.Node.Children.All( i => i.Widget().IsDisposed ) );
            base.Dispose();
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        protected virtual void OnBeforeDescendantActivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(NodeBase descendant, object? argument) {
        }

        protected virtual void Sort(List<NodeBase> children) {
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        public object View { get; protected init; } = default!;

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
    public static class NodeExtensions {
        public static WidgetBase Widget(this NodeBase node) {
            return ((Node2<WidgetBase>) node).UserData;
        }
        public static T Widget<T>(this NodeBase node) where T : WidgetBase {
            return (T) ((Node2<WidgetBase>) node).UserData;
        }
    }
}
