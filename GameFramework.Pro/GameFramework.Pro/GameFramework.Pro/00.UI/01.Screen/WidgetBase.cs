#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase {
        public sealed class Node2 : Node<Node2> {

            private readonly WidgetBase m_Widget;

            public WidgetBase Widget {
                get {
                    Check.Operation.Alive( $"Node {this} must be alive", !this.IsDisposed );
                    return this.m_Widget;
                }
            }

            public Node2(WidgetBase widget) {
                this.m_Widget = widget;
            }
            protected override void OnDispose() {
                this.Widget.OnDispose();
                this.Widget.OnDisposeInternal();
            }

            protected override void OnActivate(object? argument) {
                // top-down
                foreach (var ancestor in this.Ancestors.ToList().AsEnumerable().Reverse()) {
                    ancestor.Widget.OnBeforeDescendantActivate( this, argument );
                }
                this.Widget.OnActivate( argument );
                // down-top
                foreach (var ancestor in this.Ancestors.ToList()) {
                    ancestor.Widget.OnAfterDescendantActivate( this, argument );
                }
            }
            protected override void OnDeactivate(object? argument) {
                // top-down
                foreach (var ancestor in this.Ancestors.ToList().AsEnumerable().Reverse()) {
                    ancestor.Widget.OnBeforeDescendantDeactivate( this, argument );
                }
                this.Widget.OnDeactivate( argument );
                // down-top
                foreach (var ancestor in this.Ancestors.ToList()) {
                    ancestor.Widget.OnAfterDescendantDeactivate( this, argument );
                }
            }

            protected override void Sort(List<Node2> children) {
                this.Widget.Sort( children );
            }

        }

        private readonly Node2 m_Node;

        public Node2 Node => this.m_Node;

        public WidgetBase() {
            this.m_Node = new Node2( this );
        }
        protected internal abstract void OnDispose();
        private protected virtual void OnDisposeInternal() {
        }

        protected internal abstract void OnActivate(object? argument);
        protected internal abstract void OnDeactivate(object? argument);

        protected internal virtual void OnBeforeDescendantActivate(Node2 descendant, object? argument) {
        }
        protected internal virtual void OnAfterDescendantActivate(Node2 descendant, object? argument) {
        }
        protected internal virtual void OnBeforeDescendantDeactivate(Node2 descendant, object? argument) {
        }
        protected internal virtual void OnAfterDescendantDeactivate(Node2 descendant, object? argument) {
        }

        protected internal virtual void Sort(List<Node2> children) {
        }

    }

    public abstract class ViewableWidgetBase : WidgetBase {

        private readonly object m_View = default!;

        public object View {
            get {
                Check.Operation.Alive( $"Widget {this} must be alive", !this.Node.IsDisposed );
                return this.m_View;
            }
            protected init {
                Check.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                Check.Operation.Alive( $"Widget {this} must be alive", !this.Node.IsDisposed );
                this.m_View = value;
            }
        }

        internal ViewableWidgetBase() {
        }
        private protected override void OnDisposeInternal() {
            if (this.View is IDisposable view) {
                view.Dispose();
            }
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
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
