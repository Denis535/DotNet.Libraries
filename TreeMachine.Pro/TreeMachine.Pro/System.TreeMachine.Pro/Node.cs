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
    public sealed class Node2<TUserData> : NodeBase2<Node2<TUserData>> {

        // UserData
        public TUserData UserData { get; private set; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // OnDescendantAttach
        public event Action<Node2<TUserData>, object?>? OnBeforeDescendantAttachCallback;
        public event Action<Node2<TUserData>, object?>? OnAfterDescendantAttachCallback;

        public event Action<Node2<TUserData>, object?>? OnBeforeDescendantDetachCallback;
        public event Action<Node2<TUserData>, object?>? OnAfterDescendantDetachCallback;

        // OnDescendantActivate
        public event Action<Node2<TUserData>, object?>? OnBeforeDescendantActivateCallback;
        public event Action<Node2<TUserData>, object?>? OnAfterDescendantActivateCallback;

        public event Action<Node2<TUserData>, object?>? OnBeforeDescendantDeactivateCallback;
        public event Action<Node2<TUserData>, object?>? OnAfterDescendantDeactivateCallback;

        // Constructor
        public Node2(TUserData userData) {
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

        // OnDescendantAttach
        protected override void OnBeforeDescendantAttach(Node2<TUserData> descendant, object? argument) {
            this.OnBeforeDescendantAttachCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantAttach(Node2<TUserData> descendant, object? argument) {
            this.OnAfterDescendantAttachCallback?.Invoke( descendant, argument );
        }
        protected override void OnBeforeDescendantDetach(Node2<TUserData> descendant, object? argument) {
            this.OnBeforeDescendantDetachCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantDetach(Node2<TUserData> descendant, object? argument) {
            this.OnAfterDescendantDetachCallback?.Invoke( descendant, argument );
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(Node2<TUserData> descendant, object? argument) {
            this.OnBeforeDescendantActivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantActivate(Node2<TUserData> descendant, object? argument) {
            this.OnAfterDescendantActivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnBeforeDescendantDeactivate(Node2<TUserData> descendant, object? argument) {
            this.OnBeforeDescendantDeactivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantDeactivate(Node2<TUserData> descendant, object? argument) {
            this.OnAfterDescendantDeactivateCallback?.Invoke( descendant, argument );
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

    }
}
