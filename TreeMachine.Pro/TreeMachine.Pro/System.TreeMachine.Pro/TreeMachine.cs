#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed partial class TreeMachine<T> : IDisposable where T : class, INode<T> {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private T? m_Root = null;

    }
    public sealed partial class TreeMachine<T> {

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
                Check.Operation.Alive( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_Root;
            }
            private set {
                Check.Operation.Alive( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                if (value != null) {
                    Check.Operation.Valid( $"TreeMachine {this} must have no {this.m_Root} root", this.m_Root == null );
                } else {
                    Check.Operation.Valid( $"TreeMachine {this} must have root", this.m_Root != null );
                }
                this.m_Root = value;
            }
        }

    }
    public sealed partial class TreeMachine<T> {

        // Constructor
        public TreeMachine() {
        }
        public void Dispose() {
            Check.Operation.Alive( $"TreeMachine {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                Check.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null || this.Root.IsDisposed );
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }

    }
    public sealed partial class TreeMachine<T> {

        // SetRoot
        public void SetRoot(T? root, object? argument, Action<T, object?>? callback = null) {
            Check.Operation.Alive( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
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
            Check.Operation.Alive( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        internal void RemoveRoot(T root, object? argument, Action<T, object?>? callback = null) {
            Check.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Check.Argument.Valid( $"Argument 'root' ({root}) must be non-disposed", !root.IsDisposed );
            Check.Argument.Valid( $"Argument 'root' ({root}) must have {this} owner", root.Owner == this );
            Check.Operation.Alive( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"TreeMachine {this} must have {root} root", this.Root == root );
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
