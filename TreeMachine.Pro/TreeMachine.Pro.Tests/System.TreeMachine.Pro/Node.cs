namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public abstract class Node : NodeBase<Node>, IDescendantNodeListener<Node> {

        //public bool IsDisposed { get; private set; }

        // OnDescendantAttach
        public Action<Node, object?>? OnBeforeDescendantAttachCallback { get; set; }
        public Action<Node, object?>? OnAfterDescendantAttachCallback { get; set; }
        public Action<Node, object?>? OnBeforeDescendantDetachCallback { get; set; }
        public Action<Node, object?>? OnAfterDescendantDetachCallback { get; set; }

        // OnDescendantActivate
        public Action<Node, object?>? OnBeforeDescendantActivateCallback { get; set; }
        public Action<Node, object?>? OnAfterDescendantActivateCallback { get; set; }
        public Action<Node, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
        public Action<Node, object?>? OnAfterDescendantDeactivateCallback { get; set; }

        public Node() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"Node {this} must have no machine" ).Valid( Machine == null );
        //    foreach (var child in Children) {
        //        child.Dispose();
        //    }
        //    IsDisposed = true;
        //}

        // OnAttach
        protected override void OnAttach(object? argument) {
            //if (argument != null) {
            //    Trace.WriteLine( "OnAttach: " + this.GetType().Name + $" ({argument})" );
            //} else {
            //    Trace.WriteLine( "OnAttach: " + this.GetType().Name );
            //}
        }
        protected override void OnDetach(object? argument) {
            //if (argument != null) {
            //    Trace.WriteLine( "OnDetach: " + this.GetType().Name + $" ({argument})" );
            //} else {
            //    Trace.WriteLine( "OnDetach: " + this.GetType().Name );
            //}
        }

        // OnDescendantAttach
        void IDescendantNodeListener<Node>.OnBeforeDescendantAttach(Node descendant, object? argument) {
        }
        void IDescendantNodeListener<Node>.OnAfterDescendantAttach(Node descendant, object? argument) {
        }
        void IDescendantNodeListener<Node>.OnBeforeDescendantDetach(Node descendant, object? argument) {
        }
        void IDescendantNodeListener<Node>.OnAfterDescendantDetach(Node descendant, object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            if (argument != null) {
                Trace.WriteLine( "OnActivate: " + this.GetType().Name + $" ({argument})" );
            } else {
                Trace.WriteLine( "OnActivate: " + this.GetType().Name );
            }
        }
        protected override void OnDeactivate(object? argument) {
            if (argument != null) {
                Trace.WriteLine( "OnDeactivate: " + this.GetType().Name + $" ({argument})" );
            } else {
                Trace.WriteLine( "OnDeactivate: " + this.GetType().Name );
            }
        }

        // OnDescendantActivate
        void IDescendantNodeListener<Node>.OnBeforeDescendantActivate(Node descendant, object? argument) {
        }
        void IDescendantNodeListener<Node>.OnAfterDescendantActivate(Node descendant, object? argument) {
        }
        void IDescendantNodeListener<Node>.OnBeforeDescendantDeactivate(Node descendant, object? argument) {
        }
        void IDescendantNodeListener<Node>.OnAfterDescendantDeactivate(Node descendant, object? argument) {
        }

        // AddChild
        public new void AddChild(Node child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<Node> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(Node child, object? argument, Action<Node, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<Node, bool> predicate, object? argument, Action<Node, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<Node, bool> predicate, object? argument, Action<Node, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<Node, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<Node, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // Sort
        protected override void Sort(List<Node> children) {
            base.Sort( children );
        }

    }
    public sealed class Root : Node {
    }
    public sealed class A : Node {
    }
    public sealed class B : Node {
    }
}
