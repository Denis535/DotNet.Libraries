#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class StateBase<TThis> : IState<TThis> where TThis : notnull, StateBase<TThis> {

        IStateMachine<TThis>? IState<TThis>.Owner { get => this.Owner; set => this.Owner = value; }

        Activity IState<TThis>.Activity { get => this.Activity; set => this.Activity = value; }

    }
    public abstract partial class StateBase<TThis> {

        Action<object?>? IState<TThis>.OnBeforeAttachCallback {
            get => this.OnBeforeAttachCallback;
            set => this.OnBeforeAttachCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterAttachCallback {
            get => this.OnAfterAttachCallback;
            set => this.OnAfterAttachCallback = value;
        }
        Action<object?>? IState<TThis>.OnBeforeDetachCallback {
            get => this.OnBeforeDetachCallback;
            set => this.OnBeforeDetachCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterDetachCallback {
            get => this.OnAfterDetachCallback;
            set => this.OnAfterDetachCallback = value;
        }

        void IState<TThis>.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void IState<TThis>.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void IState<TThis>.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        void IState<TThis>.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void IState<TThis>.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void IState<TThis>.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        Action<object?>? IState<TThis>.OnBeforeActivateCallback {
            get => this.OnBeforeActivateCallback;
            set => this.OnBeforeActivateCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterActivateCallback {
            get => this.OnAfterActivateCallback;
            set => this.OnAfterActivateCallback = value;
        }
        Action<object?>? IState<TThis>.OnBeforeDeactivateCallback {
            get => this.OnBeforeDeactivateCallback;
            set => this.OnBeforeDeactivateCallback = value;
        }
        Action<object?>? IState<TThis>.OnAfterDeactivateCallback {
            get => this.OnAfterDeactivateCallback;
            set => this.OnAfterDeactivateCallback = value;
        }

        void IState<TThis>.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void IState<TThis>.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void IState<TThis>.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        void IState<TThis>.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void IState<TThis>.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void IState<TThis>.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class StateBase<TThis> {

        // Owner
        private IStateMachine<TThis>? Owner { get; set; }

        // Machine
        public IStateMachine<TThis>? Machine => ((IState<TThis>) this).Machine;

        // Activity
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Constructor
        public StateBase() {
        }

    }
    public abstract partial class StateBase<TThis> {

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
    public abstract partial class StateBase<TThis> {

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
}
