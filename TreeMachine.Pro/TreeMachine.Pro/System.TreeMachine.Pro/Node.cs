#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class Node<TUserData> : NodeBase<Node<TUserData>> {

        // UserData
        public TUserData UserData { get; private set; }

        // Constructor
        public Node(TUserData userData) {
            this.UserData = userData;
        }

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

        // AddChild
        public new void AddChild(Node<TUserData> child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<Node<TUserData>> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(Node<TUserData> child, object? argument, Action<Node<TUserData>, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<Node<TUserData>, bool> predicate, object? argument, Action<Node<TUserData>, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<Node<TUserData>, bool> predicate, object? argument, Action<Node<TUserData>, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<Node<TUserData>, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<Node<TUserData>, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

    }
    public sealed class Node2<TUserData> : NodeBase<Node2<TUserData>>, IDescendantNodeListener<Node2<TUserData>> {

        // UserData
        public TUserData UserData { get; private set; }

        // OnDescendantAttach
        public Action<Node2<TUserData>, object?>? OnBeforeDescendantAttachCallback { get; set; }
        public Action<Node2<TUserData>, object?>? OnAfterDescendantAttachCallback { get; set; }
        public Action<Node2<TUserData>, object?>? OnBeforeDescendantDetachCallback { get; set; }
        public Action<Node2<TUserData>, object?>? OnAfterDescendantDetachCallback { get; set; }

        // OnDescendantActivate
        public Action<Node2<TUserData>, object?>? OnBeforeDescendantActivateCallback { get; set; }
        public Action<Node2<TUserData>, object?>? OnAfterDescendantActivateCallback { get; set; }
        public Action<Node2<TUserData>, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
        public Action<Node2<TUserData>, object?>? OnAfterDescendantDeactivateCallback { get; set; }

        // Constructor
        public Node2(TUserData userData) {
            this.UserData = userData;
        }

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

        // AddChild
        public new void AddChild(Node2<TUserData> child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<Node2<TUserData>> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(Node2<TUserData> child, object? argument, Action<Node2<TUserData>, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<Node2<TUserData>, bool> predicate, object? argument, Action<Node2<TUserData>, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<Node2<TUserData>, bool> predicate, object? argument, Action<Node2<TUserData>, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<Node2<TUserData>, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<Node2<TUserData>, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // OnDescendantAttach
        void IDescendantNodeListener<Node2<TUserData>>.OnBeforeDescendantAttach(Node2<TUserData> descendant, object? argument) {
        }
        void IDescendantNodeListener<Node2<TUserData>>.OnAfterDescendantAttach(Node2<TUserData> descendant, object? argument) {
        }
        void IDescendantNodeListener<Node2<TUserData>>.OnBeforeDescendantDetach(Node2<TUserData> descendant, object? argument) {
        }
        void IDescendantNodeListener<Node2<TUserData>>.OnAfterDescendantDetach(Node2<TUserData> descendant, object? argument) {
        }

        // OnDescendantActivate
        void IDescendantNodeListener<Node2<TUserData>>.OnBeforeDescendantActivate(Node2<TUserData> descendant, object? argument) {
        }
        void IDescendantNodeListener<Node2<TUserData>>.OnAfterDescendantActivate(Node2<TUserData> descendant, object? argument) {
        }
        void IDescendantNodeListener<Node2<TUserData>>.OnBeforeDescendantDeactivate(Node2<TUserData> descendant, object? argument) {
        }
        void IDescendantNodeListener<Node2<TUserData>>.OnAfterDescendantDeactivate(Node2<TUserData> descendant, object? argument) {
        }

    }
}
