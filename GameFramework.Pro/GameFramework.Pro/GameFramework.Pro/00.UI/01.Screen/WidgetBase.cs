#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
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
        protected Node2<WidgetBase> NodeMutable {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }

        public WidgetBase() {
            this.m_Node = new Node2<WidgetBase>( this ) {
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
            Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
            if (!this.NodeMutable.IsDisposed && this.NodeMutable.UserData != null) {
                this.NodeMutable.UserData = null!;
                this.NodeMutable.Dispose();
                return;
            }
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
