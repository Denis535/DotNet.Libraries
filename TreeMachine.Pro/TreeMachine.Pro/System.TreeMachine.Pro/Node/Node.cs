#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class Node<TUserData> : NodeBase<Node<TUserData>> {

        // UserData
        public TUserData UserData { get; private set; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public Node(TUserData userData) {
            this.UserData = userData;
        }

        // OnAttach
        protected override void OnAttach(object? argument) {
            this.OnAttachCallback?.Invoke( argument );
        }
        protected override void OnDetach(object? argument) {
            this.OnDetachCallback?.Invoke( argument );
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            this.OnActivateCallback?.Invoke( argument );
        }
        protected override void OnDeactivate(object? argument) {
            this.OnDeactivateCallback?.Invoke( argument );
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
}
