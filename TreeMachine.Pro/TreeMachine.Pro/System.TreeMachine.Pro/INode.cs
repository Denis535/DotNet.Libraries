#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public partial interface INode<TThis> where TThis : class, INode<TThis> {

        // Owner
        protected object? Owner { get; }

        // Machine
        public ITreeMachine<TThis>? Machine { get; }
        protected internal ITreeMachine<TThis>? Machine_NoRecursive { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public TThis Root { get; }

        // Parent
        public TThis? Parent { get; }
        public IEnumerable<TThis> Ancestors { get; }
        public IEnumerable<TThis> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public IReadOnlyList<TThis> Children { get; }
        public IEnumerable<TThis> Descendants { get; }
        public IEnumerable<TThis> DescendantsAndSelf { get; }

    }
    public partial interface INode<TThis> {

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Attach
        protected internal void Attach(ITreeMachine<TThis> machine, object? argument);
        protected void Attach(TThis parent, object? argument);

        // Detach
        protected internal void Detach(ITreeMachine<TThis> machine, object? argument);
        protected void Detach(TThis parent, object? argument);

        // OnAttach
        protected void OnAttach(object? argument);
        protected void OnBeforeAttach(object? argument);
        protected void OnAfterAttach(object? argument);

        // OnDetach
        protected void OnDetach(object? argument);
        protected void OnBeforeDetach(object? argument);
        protected void OnAfterDetach(object? argument);

    }
    public partial interface INode<TThis> {

        // OnActivate
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Activate
        protected void Activate(object? argument);

        // Deactivate
        protected void Deactivate(object? argument);

        // OnActivate
        protected void OnActivate(object? argument);
        protected void OnBeforeActivate(object? argument);
        protected void OnAfterActivate(object? argument);

        // OnDeactivate
        protected void OnDeactivate(object? argument);
        protected void OnBeforeDeactivate(object? argument);
        protected void OnAfterDeactivate(object? argument);

    }
    public partial interface INode<TThis> {

        // AddChild
        protected void AddChild(TThis child, object? argument);
        protected void AddChildren(IEnumerable<TThis> children, object? argument);

        // RemoveChild
        protected void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
        protected int RemoveChildren(object? argument, Action<TThis, object?>? callback);

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

    }
    public partial interface INode2<TThis> where TThis : class, INode<TThis> {

        // OnDescendantAttach
        public Action<TThis, object?>? OnBeforeDescendantAttachCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantAttachCallback { get; set; }
        public Action<TThis, object?>? OnBeforeDescendantDetachCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantDetachCallback { get; set; }

        // OnDescendantAttach
        protected void OnBeforeDescendantAttach(TThis descendant, object? argument);
        protected void OnAfterDescendantAttach(TThis descendant, object? argument);
        protected void OnBeforeDescendantDetach(TThis descendant, object? argument);
        protected void OnAfterDescendantDetach(TThis descendant, object? argument);

        // Helpers
        protected static void OnBeforeAttach(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.Reverse().OfType<INode2<TThis>>()) {
                ancestor.OnBeforeDescendantAttachCallback?.Invoke( node, argument );
                ancestor.OnBeforeDescendantAttach( node, argument );
            }
        }
        protected static void OnAfterAttach(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.OfType<INode2<TThis>>()) {
                ancestor.OnAfterDescendantAttach( node, argument );
                ancestor.OnAfterDescendantAttachCallback?.Invoke( node, argument );
            }
        }
        protected static void OnBeforeDetach(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.Reverse().OfType<INode2<TThis>>()) {
                ancestor.OnBeforeDescendantDetachCallback?.Invoke( node, argument );
                ancestor.OnBeforeDescendantDetach( node, argument );
            }
        }
        protected static void OnAfterDetach(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.OfType<INode2<TThis>>()) {
                ancestor.OnAfterDescendantDetach( node, argument );
                ancestor.OnAfterDescendantDetachCallback?.Invoke( node, argument );
            }
        }

    }
    public partial interface INode2<TThis> {

        // OnDescendantActivate
        public Action<TThis, object?>? OnBeforeDescendantActivateCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantActivateCallback { get; set; }
        public Action<TThis, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantDeactivateCallback { get; set; }

        // OnDescendantActivate
        protected void OnBeforeDescendantActivate(TThis descendant, object? argument);
        protected void OnAfterDescendantActivate(TThis descendant, object? argument);
        protected void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
        protected void OnAfterDescendantDeactivate(TThis descendant, object? argument);

        // Helpers
        protected static void OnBeforeActivate(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.Reverse().OfType<INode2<TThis>>()) {
                ancestor.OnBeforeDescendantActivateCallback?.Invoke( node, argument );
                ancestor.OnBeforeDescendantActivate( node, argument );
            }
        }
        protected static void OnAfterActivate(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.OfType<INode2<TThis>>()) {
                ancestor.OnAfterDescendantActivate( node, argument );
                ancestor.OnAfterDescendantActivateCallback?.Invoke( node, argument );
            }
        }
        protected static void OnBeforeDeactivate(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.Reverse().OfType<INode2<TThis>>()) {
                ancestor.OnBeforeDescendantDeactivateCallback?.Invoke( node, argument );
                ancestor.OnBeforeDescendantDeactivate( node, argument );
            }
        }
        protected static void OnAfterDeactivate(TThis node, object? argument) {
            foreach (var ancestor in node.Ancestors.OfType<INode2<TThis>>()) {
                ancestor.OnAfterDescendantDeactivate( node, argument );
                ancestor.OnAfterDescendantDeactivateCallback?.Invoke( node, argument );
            }
        }

    }
}
