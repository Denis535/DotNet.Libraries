#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : DisposableBase {

        protected ScreenBase? Screen => ((TreeMachine<ScreenBase>?) this.Node.Machine)?.UserData;
        public INode Node => this.NodeMutable;
        protected internal Node2<WidgetBase> NodeMutable { get; }

        public WidgetBase() {
            this.NodeMutable = new Node2<WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.NodeMutable.OnActivateCallback += this.OnActivate;
            this.NodeMutable.OnDeactivateCallback += this.OnDeactivate;
            this.NodeMutable.OnBeforeDescendantActivateCallback += this.OnBeforeDescendantActivate;
            this.NodeMutable.OnAfterDescendantActivateCallback += this.OnAfterDescendantActivate;
            this.NodeMutable.OnBeforeDescendantDeactivateCallback += this.OnBeforeDescendantDeactivate;
            this.NodeMutable.OnAfterDescendantDeactivateCallback += this.OnAfterDescendantDeactivate;
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

        protected virtual void Sort(List<INode> children) {
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
    public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase
        where TView : notnull {

        protected new TView View { get => (TView) base.View; init => base.View = value; }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
