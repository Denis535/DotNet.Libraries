namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class WidgetBase : DisposableBase {
        internal sealed class Node_ : NodeBase2<Node_> {

            internal WidgetBase Owner { get; }

            public Node_(WidgetBase owner) {
                this.Owner = owner;
            }

            protected override void OnAttach(object? argument) {
                this.Owner.OnAttach( argument );
            }
            protected override void OnDetach(object? argument) {
                this.Owner.OnDetach( argument );
            }

            protected override void OnActivate(object? argument) {
                this.Owner.OnActivate( argument );
            }
            protected override void OnDeactivate(object? argument) {
                this.Owner.OnDeactivate( argument );
            }

            protected override void OnBeforeDescendantAttach(Node_ descendant, object? argument) {
                this.Owner.OnBeforeDescendantAttach( descendant.Owner, argument );
            }
            protected override void OnAfterDescendantAttach(Node_ descendant, object? argument) {
                this.Owner.OnAfterDescendantAttach( descendant.Owner, argument );
            }
            protected override void OnBeforeDescendantDetach(Node_ descendant, object? argument) {
                this.Owner.OnBeforeDescendantDetach( descendant.Owner, argument );
            }
            protected override void OnAfterDescendantDetach(Node_ descendant, object? argument) {
                this.Owner.OnAfterDescendantDetach( descendant.Owner, argument );
            }

            protected override void OnBeforeDescendantActivate(Node_ descendant, object? argument) {
                this.Owner.OnBeforeDescendantActivate( descendant.Owner, argument );
            }
            protected override void OnAfterDescendantActivate(Node_ descendant, object? argument) {
                this.Owner.OnAfterDescendantActivate( descendant.Owner, argument );
            }
            protected override void OnBeforeDescendantDeactivate(Node_ descendant, object? argument) {
                this.Owner.OnBeforeDescendantDeactivate( descendant.Owner, argument );
            }
            protected override void OnAfterDescendantDeactivate(Node_ descendant, object? argument) {
                this.Owner.OnAfterDescendantDeactivate( descendant.Owner, argument );
            }

            public new void AddChild(WidgetBase.Node_ child, object? argument) {
                base.AddChild( child, argument );
            }
            public new void AddChildren(IEnumerable<WidgetBase.Node_> children, object? argument) {
                base.AddChildren( children, argument );
            }

            public new void RemoveChild(WidgetBase.Node_ child, object? argument, Action<WidgetBase.Node_, object?>? callback) {
                base.RemoveChild( child, argument, callback );
            }
            public new bool RemoveChild(Func<WidgetBase.Node_, bool> predicate, object? argument, Action<WidgetBase.Node_, object?>? callback) {
                return base.RemoveChild( predicate, argument, callback );
            }
            public new int RemoveChildren(Func<WidgetBase.Node_, bool> predicate, object? argument, Action<WidgetBase.Node_, object?>? callback) {
                return base.RemoveChildren( predicate, argument, callback );
            }
            public new int RemoveChildren(object? argument, Action<WidgetBase.Node_, object?>? callback) {
                return base.RemoveChildren( argument, callback );
            }

            public new void RemoveSelf(object? argument, Action<WidgetBase.Node_, object?>? callback) {
                base.RemoveSelf( argument, callback );
            }

            protected override void Sort(List<Node_> children) {
                if (this.Owner.Comparer != null) {
                    children.Sort( (a, b) => this.Owner.Comparer.Compare( a.Owner, b.Owner ) );
                }
            }

        }

        internal Node_ Node { get; }

        internal TreeMachineBase<Node_>? Machine => this.Node.Machine;

        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => this.Node.IsRoot;
        public WidgetBase Root => this.Node.Root.Owner;

        public WidgetBase? Parent => this.Node.Parent?.Owner;
        public IEnumerable<WidgetBase> Ancestors => this.Node.Ancestors.Select( i => i.Owner );
        public IEnumerable<WidgetBase> AncestorsAndSelf => this.Node.AncestorsAndSelf.Select( i => i.Owner );

        public Activity Activity => this.Node.Activity;

        public IEnumerable<WidgetBase> Children => this.Node.Children.Select( i => i.Owner );
        public IEnumerable<WidgetBase> Descendants => this.Node.Descendants.Select( i => i.Owner );
        public IEnumerable<WidgetBase> DescendantsAndSelf => this.Node.DescendantsAndSelf.Select( i => i.Owner );

        protected IComparer<WidgetBase>? Comparer { get; init; }

        public WidgetBase() {
            this.Node = new Node_( this );
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                Assert.Operation.Valid( $"Disposable {child} must be disposed", child.Owner.IsDisposed );
            }
            Assert.Operation.Valid( $"Disposable {this} must be inactive", this.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected virtual void OnAttach(object? argument) {
        }
        protected virtual void OnDetach(object? argument) {
        }

        protected virtual void OnActivate(object? argument) {
        }
        protected virtual void OnDeactivate(object? argument) {
        }

        protected virtual void OnBeforeDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDetach(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDetach(WidgetBase descendant, object? argument) {
        }

        protected virtual void OnBeforeDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(WidgetBase descendant, object? argument) {
        }

        protected virtual void AddChild(WidgetBase child, object? argument) {
            this.Node.AddChild( child.Node, argument );
        }
        protected virtual void AddChildren(IEnumerable<WidgetBase> children, object? argument) {
            this.Node.AddChildren( children.Select( i => i.Node ), argument );
        }

        protected virtual void RemoveChild(WidgetBase child, object? argument, Action<WidgetBase, object?>? callback) {
            this.Node.RemoveChild( child.Node, argument, (child, arg) => callback?.Invoke( child.Owner, arg ) );
        }
        protected virtual bool RemoveChild(Func<WidgetBase, bool> predicate, object? argument, Action<WidgetBase, object?>? callback) {
            return this.Node.RemoveChild( child => predicate( child.Owner ), argument, (child, arg) => callback?.Invoke( child.Owner, arg ) );
        }
        protected virtual int RemoveChildren(Func<WidgetBase, bool> predicate, object? argument, Action<WidgetBase, object?>? callback) {
            return this.Node.RemoveChildren( child => predicate( child.Owner ), argument, (child, arg) => callback?.Invoke( child.Owner, arg ) );
        }
        protected virtual int RemoveChildren(object? argument, Action<WidgetBase, object?>? callback) {
            return this.Node.RemoveChildren( argument, (child, arg) => callback?.Invoke( child.Owner, arg ) );
        }

        protected virtual void RemoveSelf(object? argument, Action<WidgetBase, object?>? callback) {
            this.Node.RemoveSelf( argument, (self, arg) => callback?.Invoke( self.Owner, arg ) );
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        protected IView View { get; init; } = default!;

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : notnull, IView {

        protected new TView View { get => (TView) base.View; init => base.View = value; }

        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public interface IView {
    }
}
