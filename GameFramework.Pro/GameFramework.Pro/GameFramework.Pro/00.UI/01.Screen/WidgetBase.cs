#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : DisposableBase {

        private readonly Node2<WidgetBase> m_Node;

        protected ScreenBase? Screen {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return ((IUserData<ScreenBase>?) this.Node.Machine)?.UserData;
            }
        }
        public INode2 Node {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }
        public Node2 NodeMutable {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }

        public WidgetBase() {
            this.m_Node = new Node2<WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.m_Node.OnActivateCallback += this.OnActivate;
            this.m_Node.OnDeactivateCallback += this.OnDeactivate;
            this.m_Node.OnBeforeDescendantActivateCallback += this.OnBeforeDescendantActivate;
            this.m_Node.OnAfterDescendantActivateCallback += this.OnAfterDescendantActivate;
            this.m_Node.OnBeforeDescendantDeactivateCallback += this.OnBeforeDescendantDeactivate;
            this.m_Node.OnAfterDescendantDeactivateCallback += this.OnAfterDescendantDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Widget {this} must be inactive", this.Node.Activity == Activity.Inactive );
            Assert.Operation.Valid( $"Widget {this} must have no children", this.Node.Children.All( i => i.Widget().IsDisposed ) );
            base.Dispose();
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        protected virtual void OnBeforeDescendantActivate(INode2 descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(INode2 descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(INode2 descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(INode2 descendant, object? argument) {
        }

        protected virtual void Sort(List<INode> children) {
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        private object m_View = default!;

        public object View {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_View;
            }
            protected init {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                this.m_View = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }

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

        protected new TView View {
            get => (TView) base.View;
            init => base.View = value;
        }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
