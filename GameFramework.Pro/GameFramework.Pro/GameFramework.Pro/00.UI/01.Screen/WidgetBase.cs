#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : IDisposable {

        private CancellationTokenSource? m_DisposeCancellationTokenSource;
        private readonly Node<WidgetBase> m_Node;

        public bool IsDisposed { get; private set; }
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
                return ((ITreeMachine<ScreenBase>?) this.Node.Machine)?.UserData;
            }
        }
        public INode Node {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }
        protected Node<WidgetBase> NodeMutable {
            get {
                Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }

        public WidgetBase() {
            this.m_Node = new Node<WidgetBase>( this ) {
                SortDelegate = this.Sort,
            };
            this.Node.OnActivateCallback += (argument) => {
                foreach (var ancestor in this.Node.Ancestors.ToList().AsEnumerable().Reverse()) { // root-down
                    ((INode<WidgetBase>) ancestor).Widget().OnBeforeDescendantActivate( this.Node, argument );
                }
                this.OnActivate( argument );
                foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                    ((INode<WidgetBase>) ancestor).Widget().OnAfterDescendantActivate( this.Node, argument );
                }
            };
            this.Node.OnDeactivateCallback += (argument) => {
                foreach (var ancestor in this.Node.Ancestors.ToList().AsEnumerable().Reverse()) { // root-down
                    ((INode<WidgetBase>) ancestor).Widget().OnBeforeDescendantDeactivate( this.Node, argument );
                }
                this.OnDeactivate( argument );
                foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                    ((INode<WidgetBase>) ancestor).Widget().OnAfterDescendantDeactivate( this.Node, argument );
                }
            };
        }
        ~WidgetBase() {
            Assert.Operation.Valid( $"Widget '{this}' must be disposed", this.IsDisposed );
        }
        void IDisposable.Dispose() {
            // This method can only be called from node
            this.Dispose();
        }
        protected virtual void Dispose() {
            Assert.Operation.NotDisposed( $"Widget {this} must be non-disposed", !this.IsDisposed );
            this.m_DisposeCancellationTokenSource?.Cancel();
            this.IsDisposed = true;
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        protected void OnBeforeDescendantActivate(INode descendant, object? argument) {
        }
        protected void OnAfterDescendantActivate(INode descendant, object? argument) {
        }
        protected void OnBeforeDescendantDeactivate(INode descendant, object? argument) {
        }
        protected void OnAfterDescendantDeactivate(INode descendant, object? argument) {
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
