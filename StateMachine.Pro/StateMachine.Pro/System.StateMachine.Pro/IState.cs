#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public partial interface IState : IDisposable {

        // IsDisposed
        public bool IsDisposing { get; }
        public bool IsDisposed { get; }

    }
    public partial interface IState<T> : IState where T : class, IState<T> {

        // Owner
        public object? Owner { get; }

        // Machine
        public StateMachine<T>? Machine { get; }

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

    }
    public partial interface IState<T> {

        // Attach
        protected internal void Attach(StateMachine<T> machine, object? argument);
        protected internal void Attach(T parent, object? argument);

        // Detach
        protected internal void Detach(StateMachine<T> machine, object? argument);
        protected internal void Detach(T parent, object? argument);

        // Activate
        protected internal void Activate(object? argument);

        // Deactivate
        protected internal void Deactivate(object? argument);

    }
}
