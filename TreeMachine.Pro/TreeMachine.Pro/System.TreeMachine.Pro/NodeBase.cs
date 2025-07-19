#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public abstract partial class NodeBase<TThis> : INode<TThis> where TThis : notnull, NodeBase<TThis> {

        object? INode<TThis>.Owner { get => this.Owner; set => this.Owner = value; }

        Activity INode<TThis>.Activity { get => this.Activity; set => this.Activity = value; }

        IReadOnlyList<TThis> INode<TThis>.Children => this.Children;

    }
    public abstract partial class NodeBase<TThis> {

        Action<object?>? INode<TThis>.OnBeforeAttachCallback {
            get => this.OnBeforeAttachCallback;
            set => this.OnBeforeAttachCallback = value;
        }
        Action<object?>? INode<TThis>.OnAfterAttachCallback {
            get => this.OnAfterAttachCallback;
            set => this.OnAfterAttachCallback = value;
        }
        Action<object?>? INode<TThis>.OnBeforeDetachCallback {
            get => this.OnBeforeDetachCallback;
            set => this.OnBeforeDetachCallback = value;
        }
        Action<object?>? INode<TThis>.OnAfterDetachCallback {
            get => this.OnAfterDetachCallback;
            set => this.OnAfterDetachCallback = value;
        }

        void INode<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void INode<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void INode<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void INode<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void INode<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void INode<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

    }
    public abstract partial class NodeBase<TThis> {

        Action<object?>? INode<TThis>.OnBeforeActivateCallback {
            get => this.OnBeforeActivateCallback;
            set => this.OnBeforeActivateCallback = value;
        }
        Action<object?>? INode<TThis>.OnAfterActivateCallback {
            get => this.OnAfterActivateCallback;
            set => this.OnAfterActivateCallback = value;
        }
        Action<object?>? INode<TThis>.OnBeforeDeactivateCallback {
            get => this.OnBeforeDeactivateCallback;
            set => this.OnBeforeDeactivateCallback = value;
        }
        Action<object?>? INode<TThis>.OnAfterDeactivateCallback {
            get => this.OnAfterDeactivateCallback;
            set => this.OnAfterDeactivateCallback = value;
        }

        void INode<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void INode<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void INode<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void INode<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void INode<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void INode<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class NodeBase<TThis> {

        void INode<TThis>.AddChild(TThis child, object? argument) {
            this.AddChild( child, argument );
        }
        void INode<TThis>.AddChildren(IEnumerable<TThis> children, object? argument) {
            this.AddChildren( children, argument );
        }
        void INode<TThis>.RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            this.RemoveChild( child, argument, callback );
        }
        bool INode<TThis>.RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChild( predicate, argument, callback );
        }
        int INode<TThis>.RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChildren( predicate, argument, callback );
        }
        int INode<TThis>.RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            return this.RemoveChildren( argument, callback );
        }
        void INode<TThis>.RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            this.RemoveSelf( argument, callback );
        }

        void INode<TThis>.Sort(List<TThis> children) {
            this.Sort( children );
        }

    }
    public abstract partial class NodeBase<TThis> {

        // Owner
        private object? Owner { get; set; }

        // Machine
        public ITreeMachine<TThis>? Machine => ((INode<TThis>) this).Machine;
        internal ITreeMachine<TThis>? Machine_NoRecursive => ((INode<TThis>) this).Machine_NoRecursive;

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => ((INode<TThis>) this).IsRoot;
        public TThis Root => ((INode<TThis>) this).Root;

        // Parent
        public TThis? Parent => ((INode<TThis>) this).Parent;
        public IEnumerable<TThis> Ancestors => ((INode<TThis>) this).Ancestors;
        public IEnumerable<TThis> AncestorsAndSelf => ((INode<TThis>) this).AncestorsAndSelf;

        // Activity
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Children
        public IReadOnlyList<TThis> Children { get; } = new List<TThis>( 0 );
        public IEnumerable<TThis> Descendants => ((INode<TThis>) this).Descendants;
        public IEnumerable<TThis> DescendantsAndSelf => ((INode<TThis>) this).DescendantsAndSelf;

        // Constructor
        public NodeBase() {
        }

    }
    public abstract partial class NodeBase<TThis> {

        // OnAttach
        public Action<object?>? OnBeforeAttachCallback { get; set; }
        public Action<object?>? OnAfterAttachCallback { get; set; }
        public Action<object?>? OnBeforeDetachCallback { get; set; }
        public Action<object?>? OnAfterDetachCallback { get; set; }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
        }
        protected virtual void OnAfterAttach(object? argument) {
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
        }
        protected virtual void OnAfterDetach(object? argument) {
        }

    }
    public abstract partial class NodeBase<TThis> {

        // OnActivate
        public Action<object?>? OnBeforeActivateCallback { get; set; }
        public Action<object?>? OnAfterActivateCallback { get; set; }
        public Action<object?>? OnBeforeDeactivateCallback { get; set; }
        public Action<object?>? OnAfterDeactivateCallback { get; set; }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
        }
        protected virtual void OnAfterActivate(object? argument) {
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
        }
        protected virtual void OnAfterDeactivate(object? argument) {
        }

    }
    public abstract partial class NodeBase<TThis> {

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            INode<TThis>.AddChild( (TThis) this, child, argument );
        }
        protected void AddChildren(IEnumerable<TThis> children, object? argument) {
            INode<TThis>.AddChildren( (TThis) this, children, argument );
        }

        // RemoveChild
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            INode<TThis>.RemoveChild( (TThis) this, child, argument, callback );
        }
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return INode<TThis>.RemoveChild( (TThis) this, predicate, argument, callback );
        }
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            return INode<TThis>.RemoveChildren( (TThis) this, predicate, argument, callback );
        }
        protected int RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            return INode<TThis>.RemoveChildren( (TThis) this, argument, callback );
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            INode<TThis>.RemoveSelf( (TThis) this, argument, callback );
        }

        // Sort
        protected virtual void Sort(List<TThis> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}
