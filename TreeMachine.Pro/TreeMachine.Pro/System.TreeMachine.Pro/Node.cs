#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public sealed class Node<TThis, TUserData> : NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

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
        public new void AddChild(TThis child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<TThis> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

    }
    public sealed class Node2<TThis, TUserData> : NodeBase<TThis>, IDescendantNodeListener<TThis> where TThis : notnull, NodeBase<TThis> {

        // UserData
        public TUserData UserData { get; private set; }

        // OnDescendantAttach
        public Action<TThis, object?>? OnBeforeDescendantAttachCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantAttachCallback { get; set; }
        public Action<TThis, object?>? OnBeforeDescendantDetachCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantDetachCallback { get; set; }

        // OnDescendantActivate
        public Action<TThis, object?>? OnBeforeDescendantActivateCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantActivateCallback { get; set; }
        public Action<TThis, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantDeactivateCallback { get; set; }

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
        public new void AddChild(TThis child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<TThis> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // OnDescendantAttach
        void IDescendantNodeListener<TThis>.OnBeforeDescendantAttach(TThis descendant, object? argument) {
        }
        void IDescendantNodeListener<TThis>.OnAfterDescendantAttach(TThis descendant, object? argument) {
        }
        void IDescendantNodeListener<TThis>.OnBeforeDescendantDetach(TThis descendant, object? argument) {
        }
        void IDescendantNodeListener<TThis>.OnAfterDescendantDetach(TThis descendant, object? argument) {
        }

        // OnDescendantActivate
        void IDescendantNodeListener<TThis>.OnBeforeDescendantActivate(TThis descendant, object? argument) {
        }
        void IDescendantNodeListener<TThis>.OnAfterDescendantActivate(TThis descendant, object? argument) {
        }
        void IDescendantNodeListener<TThis>.OnBeforeDescendantDeactivate(TThis descendant, object? argument) {
        }
        void IDescendantNodeListener<TThis>.OnAfterDescendantDeactivate(TThis descendant, object? argument) {
        }

    }
}
