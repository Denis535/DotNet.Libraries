#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase {

        private bool m_IsDisposed;
        private CancellationTokenSource? m_DisposeCancellationTokenSource;
        private readonly Node<ScreenBase, WidgetBase> m_Node;

        public bool IsDisposed {
            get {
                return this.m_IsDisposed;
            }
        }
        public CancellationToken DisposeCancellationToken {
            get {
                if (this.m_DisposeCancellationTokenSource == null) {
                    this.m_DisposeCancellationTokenSource = new CancellationTokenSource();
                    if (this.IsDisposed) this.m_DisposeCancellationTokenSource.Cancel();
                }
                return this.m_DisposeCancellationTokenSource.Token;
            }
        }

        protected ScreenBase? Screen {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.Node.Machine?.UserData;
            }
        }
        public INode<ScreenBase, WidgetBase> Node {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }
        protected Node<ScreenBase, WidgetBase> NodeMutable {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }

        public WidgetBase() {
            this.m_Node = new Node<ScreenBase, WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.Node.OnActivateCallback += (argument) => {
                foreach (var ancestor in this.Node.Ancestors.ToList().AsEnumerable().Reverse()) { // root-down
                    ancestor.Widget().OnBeforeDescendantActivate( this.Node, argument );
                }
                this.OnActivate( argument );
                foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                    ancestor.Widget().OnAfterDescendantActivate( this.Node, argument );
                }
            };
            this.Node.OnDeactivateCallback += (argument) => {
                foreach (var ancestor in this.Node.Ancestors.ToList().AsEnumerable().Reverse()) { // root-down
                    ancestor.Widget().OnBeforeDescendantDeactivate( this.Node, argument );
                }
                this.OnDeactivate( argument );
                foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                    ancestor.Widget().OnAfterDescendantDeactivate( this.Node, argument );
                }
            };
            this.Node.OnDisposeCallback += this.Dispose;
        }
        ~WidgetBase() {
            Trace.Assert( this.IsDisposed, $"Widget '{this}' must be disposed" );
        }
        protected virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
            Assert.Operation.NotDisposed( $"Node {this.Node} must be disposing", this.Node.IsDisposing ); // This method must only be called from node
            this.m_DisposeCancellationTokenSource?.Cancel();
            this.m_IsDisposed = true;
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
        protected override void Dispose() {
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
        protected override void Dispose() {
            base.Dispose();
        }

    }
}
