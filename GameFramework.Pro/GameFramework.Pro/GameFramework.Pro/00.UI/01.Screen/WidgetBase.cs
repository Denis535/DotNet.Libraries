#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase {

        private readonly Node<ScreenBase, WidgetBase> m_Node;

        private Action<INode<ScreenBase, WidgetBase>, object?>? m_OnBeforeDescendantActivateCallback;
        private Action<INode<ScreenBase, WidgetBase>, object?>? m_OnAfterDescendantActivateCallback;
        private Action<INode<ScreenBase, WidgetBase>, object?>? m_OnBeforeDescendantDeactivateCallback;
        private Action<INode<ScreenBase, WidgetBase>, object?>? m_OnAfterDescendantDeactivateCallback;

        protected ScreenBase? Screen {
            get {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                return this.Node.Machine?.UserData;
            }
        }
        public INode<ScreenBase, WidgetBase> Node {
            get {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                return this.m_Node;
            }
        }
        protected Node<ScreenBase, WidgetBase> NodeMutable {
            get {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                return this.m_Node;
            }
        }

        public event Action<INode<ScreenBase, WidgetBase>, object?>? OnBeforeDescendantActivateCallback {
            add {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnBeforeDescendantActivateCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnBeforeDescendantActivateCallback -= value;
            }
        }
        public event Action<INode<ScreenBase, WidgetBase>, object?>? OnAfterDescendantActivateCallback {
            add {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnAfterDescendantActivateCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnAfterDescendantActivateCallback -= value;
            }
        }
        public event Action<INode<ScreenBase, WidgetBase>, object?>? OnBeforeDescendantDeactivateCallback {
            add {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnBeforeDescendantDeactivateCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnBeforeDescendantDeactivateCallback -= value;
            }
        }
        public event Action<INode<ScreenBase, WidgetBase>, object?>? OnAfterDescendantDeactivateCallback {
            add {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnAfterDescendantDeactivateCallback += value;
            }
            remove {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_OnAfterDescendantDeactivateCallback -= value;
            }
        }

        public WidgetBase() {
            this.m_Node = new Node<ScreenBase, WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.Node.OnBeforeDisposeCallback += this.OnBeforeDispose;
            this.Node.OnAfterDisposeCallback += this.OnAfterDispose;
            this.Node.OnActivateCallback += (argument) => {
                foreach (var ancestor in this.Node.Ancestors.ToList().AsEnumerable().Reverse()) { // root-down
                    ancestor.Widget().m_OnBeforeDescendantActivateCallback?.Invoke( this.Node, argument );
                    ancestor.Widget().OnBeforeDescendantActivate( this.Node, argument );
                }
                this.OnActivate( argument );
                foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                    ancestor.Widget().OnAfterDescendantActivate( this.Node, argument );
                    ancestor.Widget().m_OnAfterDescendantActivateCallback?.Invoke( this.Node, argument );
                }
            };
            this.Node.OnDeactivateCallback += (argument) => {
                foreach (var ancestor in this.Node.Ancestors.ToList().AsEnumerable().Reverse()) { // root-down
                    ancestor.Widget().m_OnBeforeDescendantDeactivateCallback?.Invoke( this.Node, argument );
                    ancestor.Widget().OnBeforeDescendantDeactivate( this.Node, argument );
                }
                this.OnDeactivate( argument );
                foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                    ancestor.Widget().OnAfterDescendantDeactivate( this.Node, argument );
                    ancestor.Widget().m_OnAfterDescendantDeactivateCallback?.Invoke( this.Node, argument );
                }
            };
        }
        ~WidgetBase() {
            Trace.Assert( this.Node.IsDisposed, $"Node '{this.Node}' must be disposed" );
        }
        protected virtual void OnBeforeDispose() {
            Assert.Operation.NotDisposed( $"Node {this.Node} must be disposing", this.Node.IsDisposing ); // This method must only be called by INode.OnDisposeCallback
        }
        protected virtual void OnAfterDispose() {
            Assert.Operation.NotDisposed( $"Node {this.Node} must be disposing", this.Node.IsDisposing ); // This method must only be called by INode.OnDisposeCallback
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        protected virtual void OnBeforeDescendantActivate(INode<ScreenBase, WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(INode<ScreenBase, WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(INode<ScreenBase, WidgetBase> descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(INode<ScreenBase, WidgetBase> descendant, object? argument) {
        }

        protected virtual void Sort(List<INode<ScreenBase, WidgetBase>> children) {
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        private object m_View = default!;

        public object View {
            get {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                return this.m_View;
            }
            protected init {
                Assert.Operation.NotDisposed( $"Node {this.Node} must be non-disposed", !this.Node.IsDisposed );
                this.m_View = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }

        public ViewableWidgetBase() {
        }
        protected override void OnBeforeDispose() {
            base.OnBeforeDispose();
        }
        protected override void OnAfterDispose() {
            base.OnAfterDispose();
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
        protected override void OnBeforeDispose() {
            base.OnBeforeDispose();
        }
        protected override void OnAfterDispose() {
            base.OnAfterDispose();
        }

    }
}
