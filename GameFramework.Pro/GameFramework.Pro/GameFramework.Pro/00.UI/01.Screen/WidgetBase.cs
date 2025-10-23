#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase {

        private readonly Node<ScreenBase, WidgetBase> m_Node;

        private readonly Action<INode<ScreenBase, WidgetBase>, object?>? m_OnBeforeDescendantActivateCallback;
        private readonly Action<INode<ScreenBase, WidgetBase>, object?>? m_OnAfterDescendantActivateCallback;
        private readonly Action<INode<ScreenBase, WidgetBase>, object?>? m_OnBeforeDescendantDeactivateCallback;
        private readonly Action<INode<ScreenBase, WidgetBase>, object?>? m_OnAfterDescendantDeactivateCallback;

        public bool IsDisposing {
            get {
                return this.m_Node.IsDisposing;
            }
        }
        public bool IsDisposed {
            get {
                return this.m_Node.IsDisposed;
            }
        }

        protected ScreenBase? Screen {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node.Machine?.UserData;
            }
        }
        public INode<ScreenBase, WidgetBase> Node {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }
        protected Node<ScreenBase, WidgetBase> NodeMutable {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_Node;
            }
        }

        public Action<INode<ScreenBase, WidgetBase>, object?>? OnBeforeDescendantActivateCallback {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnBeforeDescendantActivateCallback;
            }
            init {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                this.m_OnBeforeDescendantActivateCallback = value;
            }
        }
        public Action<INode<ScreenBase, WidgetBase>, object?>? OnAfterDescendantActivateCallback {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnAfterDescendantActivateCallback;
            }
            init {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAfterDescendantActivateCallback = value;
            }
        }
        public Action<INode<ScreenBase, WidgetBase>, object?>? OnBeforeDescendantDeactivateCallback {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnBeforeDescendantDeactivateCallback;
            }
            init {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                this.m_OnBeforeDescendantDeactivateCallback = value;
            }
        }
        public Action<INode<ScreenBase, WidgetBase>, object?>? OnAfterDescendantDeactivateCallback {
            get {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_OnAfterDescendantDeactivateCallback;
            }
            init {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                this.m_OnAfterDescendantDeactivateCallback = value;
            }
        }

        public WidgetBase() {
            this.m_Node = new Node<ScreenBase, WidgetBase>( this ) {
                SortDelegate = this.Sort,
                OnDisposeCallback = this.OnDispose,
                OnActivateCallback = (argument) => {
                    foreach (var ancestor in this.Node.Ancestors.Reverse()) { // root-down
                        ancestor.UserData.OnBeforeDescendantActivateCallback?.Invoke( this.Node, argument );
                        ancestor.UserData.OnBeforeDescendantActivate( this.Node, argument );
                    }
                    this.OnActivate( argument );
                    foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                        ancestor.UserData.OnAfterDescendantActivate( this.Node, argument );
                        ancestor.UserData.OnAfterDescendantActivateCallback?.Invoke( this.Node, argument );
                    }
                },
                OnDeactivateCallback = (argument) => {
                    foreach (var ancestor in this.Node.Ancestors.Reverse()) { // root-down
                        ancestor.UserData.OnBeforeDescendantDeactivateCallback?.Invoke( this.Node, argument );
                        ancestor.UserData.OnBeforeDescendantDeactivate( this.Node, argument );
                    }
                    this.OnDeactivate( argument );
                    foreach (var ancestor in this.Node.Ancestors.ToList()) { // down-root
                        ancestor.UserData.OnAfterDescendantDeactivate( this.Node, argument );
                        ancestor.UserData.OnAfterDescendantDeactivateCallback?.Invoke( this.Node, argument );
                    }
                },
            };
        }
        protected abstract void OnDispose();

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
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                return this.m_View;
            }
            protected init {
                Assert.Operation.Valid( $"Widget {this} must be non-disposed", !this.IsDisposed );
                this.m_View = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }

        public ViewableWidgetBase() {
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

    }
}
