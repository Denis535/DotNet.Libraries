#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class StateMachine<T> : IDisposable where T : class, IState<T> {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private T? m_Root = null;

    }
    public sealed partial class StateMachine<T> {

        // IsDisposed
        public bool IsDisposing {
            get {
                return this.m_Lifecycle == Lifecycle.Disposing;
            }
        }
        public bool IsDisposed {
            get {
                return this.m_Lifecycle == Lifecycle.Disposed;
            }
        }

        // Root
        public T? Root {
            get {
                Check.Operation.Alive( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_Root;
            }
            private set {
                Check.Operation.Alive( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
                if (value != null) {
                    Check.Operation.Valid( $"StateMachine {this} must have no {this.m_Root} root", this.m_Root == null );
                } else {
                    Check.Operation.Valid( $"StateMachine {this} must have root", this.m_Root != null );
                }
                this.m_Root = value;
            }
        }

    }
    public sealed partial class StateMachine<T> {

        // Constructor
        public StateMachine() {
        }
        public void Dispose() {
            Check.Operation.Alive( $"StateMachine {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                Check.Operation.Valid( $"StateMachine {this} must have no {this.Root} root", this.Root == null || this.Root.IsDisposed );
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }

    }
    public sealed partial class StateMachine<T> {

        // SetRoot
        public void SetRoot(T? root, object? argument, Action<T, object?>? callback = null) {
            Check.Operation.Alive( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(T root, object? argument) {
            Check.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Check.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Check.Argument.Valid( $"Argument 'root' ({root}) must have no {root.Owner} owner", root.Owner == null );
            Check.Operation.Alive( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"StateMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(T root, object? argument, Action<T, object?>? callback = null) {
            Check.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Check.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Check.Argument.Valid( $"Argument 'root' ({root}) must have {this} owner", root.Owner == this );
            Check.Operation.Alive( $"StateMachine {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"StateMachine {this} must have {root} root", this.Root == root );
            this.Root.Detach( this, argument );
            this.Root = null;
            if (callback != null) {
                callback.Invoke( root, argument );
            } else {
                root.Dispose();
            }
        }

    }
}
