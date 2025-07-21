#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class State<TUserData> : StateBase<State<TUserData>> {

        // UserData
        public TUserData UserData { get; private set; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public State(TUserData userData) {
            this.UserData = userData;
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

        // SetChild
        public new void SetChild(State<TUserData>? child, object? argument, Action<State<TUserData>, object?>? callback) {
            base.SetChild( child, argument, callback );
        }

        // AddChild
        public new void AddChild(State<TUserData> child, object? argument) {
            base.AddChild( child, argument );
        }

        // RemoveChild
        public new void RemoveChild(State<TUserData> child, object? argument, Action<State<TUserData>, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new void RemoveChild(object? argument, Action<State<TUserData>, object?>? callback) {
            base.RemoveChild( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<State<TUserData>, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

    }
}
