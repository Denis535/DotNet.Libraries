#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface INode : IDisposable {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

    }
    public partial interface INode<T> : INode where T : class, INode<T> {

        // Owner
        public object? Owner { get; }

        // Machine
        public TreeMachine<T>? Machine { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )]
        public bool IsRoot { get; }
        public T Root { get; }

        // Parent
        public T? Parent { get; }
        public IEnumerable<T> Ancestors { get; }
        public IEnumerable<T> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public IReadOnlyList<T> Children { get; }
        public IEnumerable<T> Descendants { get; }
        public IEnumerable<T> DescendantsAndSelf { get; }

    }
    public partial interface INode<T> {

        // Attach
        protected internal void Attach(TreeMachine<T> machine, object? argument);
        protected internal void Attach(T parent, object? argument);

        // Detach
        protected internal void Detach(TreeMachine<T> machine, object? argument);
        protected internal void Detach(T parent, object? argument);

        // Activate
        protected internal void Activate(object? argument);

        // Deactivate
        protected internal void Deactivate(object? argument);

    }
}
