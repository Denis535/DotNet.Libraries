#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class ChildrenableState<T> : IState<T> where T : class, IState<T> {

        private Lifecycle m_Lifecycle = Lifecycle.Alive;
        private object? m_Owner = null;
        private Activity m_Activity = Activity.Inactive;
        private readonly List<T> m_Children = new List<T>( 0 );

        private T Self => (T) (object) this;

    }
    public abstract partial class ChildrenableState<T> {

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

        // Owner
        public object? Owner {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Owner;
            }
            private set {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                if (value != null) {
                    Check.Operation.Valid( $"State {this} must have no {this.m_Owner} owner", this.m_Owner == null );
                    Check.Operation.Valid( $"State {this} must have valid activity", this.Activity is Activity.Inactive );
                } else {
                    Check.Operation.Valid( $"State {this} must have owner", this.m_Owner != null );
                    Check.Operation.Valid( $"State {this} must have valid activity", this.Activity is Activity.Active or Activity.Inactive );
                }
                this.m_Owner = value;
            }
        }

        // Machine
        public StateMachine<T>? Machine {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return (this.Owner as StateMachine<T>) ?? (this.Owner as T)?.Machine;
            }
        }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )]
        public bool IsRoot {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Parent == null;
            }
        }
        public T Root {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Parent?.Root ?? this.Self;
            }
        }

        // Parent
        public T? Parent {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Owner as T;
            }
        }
        public IEnumerable<T> Ancestors {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<T> AncestorsAndSelf {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.Ancestors.Prepend( this.Self );
            }
        }

        // Activity
        public Activity Activity {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Activity;
            }
            private set {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                Check.Operation.Valid( $"State {this} must have owner", this.Owner != null );
                Check.Operation.Valid( $"State {this} must have valid activity", this.m_Activity != value );
                this.m_Activity = value;
            }
        }

        // Children
        public IReadOnlyList<T> Children {
            get {
                Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
                return this.m_Children;
            }
        }

    }
    public abstract partial class ChildrenableState<T> {

        // Constructor
        public ChildrenableState() {
        }
        public void Dispose() {
            Check.Operation.Alive( $"State {this} must be alive", this.m_Lifecycle == Lifecycle.Alive );
            this.m_Lifecycle = Lifecycle.Disposing;
            {
                this.OnDispose();
                Check.Operation.Valid( $"State {this} must have no {this.Children.Count( i => !i.IsDisposed )} children", this.Children.All( i => i.IsDisposed ) );
            }
            this.m_Lifecycle = Lifecycle.Disposed;
        }
        protected virtual void OnDispose() {
        }

    }
    public abstract partial class ChildrenableState<T> {

        // Attach
        private void Attach(StateMachine<T> machine, object? argument) {
            Check.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"State {this} must have no {this.Owner} owner", this.Owner == null );
            {
                this.Owner = machine;
                this.OnAttach( argument );
            }
            if (true) {
                this.Activate( argument );
            }
        }
        private void Attach(T parent, object? argument) {
            Check.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"State {this} must have no {this.Owner} owner", this.Owner == null );
            {
                this.Owner = parent;
                this.OnAttach( argument );
            }
            if (this.Parent!.Activity == Activity.Active) {
                this.Activate( argument );
            }
        }

        // Detach
        private void Detach(StateMachine<T> machine, object? argument) {
            Check.Argument.NotNull( $"Argument 'machine' must be non-null", machine != null );
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"State {this} must have {machine} owner", this.Owner == machine );
            if (true) {
                this.Deactivate( argument );
            }
            {
                this.OnDetach( argument );
                this.Owner = null;
            }
        }
        private void Detach(T parent, object? argument) {
            Check.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"State {this} must have {parent} owner", this.Owner == parent );
            if (this.Activity == Activity.Active) {
                this.Deactivate( argument );
            }
            {
                this.OnDetach( argument );
                this.Owner = null;
            }
        }

        // OnAttach
        protected virtual void OnAttach(object? argument) {
        }

        // OnDetach
        protected virtual void OnDetach(object? argument) {
        }

    }
    public abstract partial class ChildrenableState<T> {

        // Activate
        private void Activate(object? argument) {
            this.Activity = Activity.Activating;
            this.OnActivate( argument );
            foreach (var child in this.Children.ToArray()) {
                child.Activate( argument );
            }
            this.Activity = Activity.Active;
        }

        // Deactivate
        private void Deactivate(object? argument) {
            this.Activity = Activity.Deactivating;
            foreach (var child in this.Children.Reverse()) {
                child.Deactivate( argument );
            }
            this.OnDeactivate( argument );
            this.Activity = Activity.Inactive;
        }

        // OnActivate
        protected virtual void OnActivate(object? argument) {
        }

        // OnDeactivate
        protected virtual void OnDeactivate(object? argument) {
        }

    }
    public abstract partial class ChildrenableState<T> {

        // AddChild
        public void AddChild(T child, object? argument) {
            Check.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Check.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Check.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Owner} owner", child.Owner == null );
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"State {this} must have no {child} child", !this.Children.Contains( child ) );
            this.m_Children.Add( child );
            this.Sort( this.m_Children );
            child.Attach( this.Self, argument );
        }
        public void AddChildren(IEnumerable<T> children, object? argument) {
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        public void RemoveChild(T child, object? argument, Action<T, object?>? callback = null) {
            Check.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Check.Argument.Valid( $"Argument 'child' ({child}) must be non-disposed", !child.IsDisposed );
            Check.Argument.Valid( $"Argument 'child' ({child}) must have {this} owner", child.Owner == this );
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            Check.Operation.Valid( $"State {this} must have {child} child", this.Children.Contains( child ) );
            child.Detach( this.Self, argument );
            _ = this.m_Children.Remove( child );
            if (callback != null) {
                callback.Invoke( child, argument );
            } else {
                child.Dispose();
            }
        }
        public int RemoveChildren(Func<T, bool> predicate, object? argument, Action<T, object?>? callback = null) {
            Check.Operation.Alive( $"State {this} must be non-disposed", !this.IsDisposed );
            var count = 0;
            foreach (var child in this.Children.Reverse().Where( predicate )) {
                this.RemoveChild( child, argument, callback );
                count++;
            }
            return count;
        }

        // Sort
        protected virtual void Sort(List<T> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}
