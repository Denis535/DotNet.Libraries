#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Node2 : NodeBase2 {

        // Sort
        public Action<List<INode>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // OnDescendantAttach
        public event Action<INode2, object?>? OnBeforeDescendantAttachCallback;
        public event Action<INode2, object?>? OnAfterDescendantAttachCallback;
        public event Action<INode2, object?>? OnBeforeDescendantDetachCallback;
        public event Action<INode2, object?>? OnAfterDescendantDetachCallback;

        // OnDescendantActivate
        public event Action<INode2, object?>? OnBeforeDescendantActivateCallback;
        public event Action<INode2, object?>? OnAfterDescendantActivateCallback;
        public event Action<INode2, object?>? OnBeforeDescendantDeactivateCallback;
        public event Action<INode2, object?>? OnAfterDescendantDeactivateCallback;

        // Constructor
        public Node2() {
        }
        public override void Dispose() {
            foreach (var child in this.Children) {
                child.Dispose();
            }
            base.Dispose();
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
        protected override void OnBeforeDescendantAttach(INode2 descendant, object? argument) {
            this.OnBeforeDescendantAttachCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantAttach(INode2 descendant, object? argument) {
            this.OnAfterDescendantAttachCallback?.Invoke( descendant, argument );
        }
        protected override void OnBeforeDescendantDetach(INode2 descendant, object? argument) {
            this.OnBeforeDescendantDetachCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantDetach(INode2 descendant, object? argument) {
            this.OnAfterDescendantDetachCallback?.Invoke( descendant, argument );
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(INode2 descendant, object? argument) {
            this.OnBeforeDescendantActivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantActivate(INode2 descendant, object? argument) {
            this.OnAfterDescendantActivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnBeforeDescendantDeactivate(INode2 descendant, object? argument) {
            this.OnBeforeDescendantDeactivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantDeactivate(INode2 descendant, object? argument) {
            this.OnAfterDescendantDeactivateCallback?.Invoke( descendant, argument );
        }

        // AddChild
        public new void AddChild(INode child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<INode> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(INode child, object? argument, Action<INode, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<INode, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<INode, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // Sort
        protected override void Sort(List<INode> children) {
            this.SortDelegate?.Invoke( children );
        }

    }
    public class Node2<TUserData> : Node2, IUserData<TUserData> {

        private TUserData m_UserData = default!;

        // UserData
        public TUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"Node {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // Constructor
        public Node2(TUserData userData) {
            this.UserData = userData;
        }
        public override void Dispose() {
            (this.UserData as IDisposable)?.Dispose();
            base.Dispose();
        }

    }
}
