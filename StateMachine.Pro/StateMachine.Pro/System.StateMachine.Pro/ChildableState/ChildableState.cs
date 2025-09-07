#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class ChildableState<TUserData> : ChildableStateBase {

        // UserData
        public TUserData UserData { get; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public ChildableState(TUserData userData) {
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
        public new void SetChild(IState? child, object? argument, Action<IState, object?>? callback) {
            base.SetChild( child, argument, callback );
        }

        // AddChild
        public new void AddChild(IState child, object? argument) {
            base.AddChild( child, argument );
        }

        // RemoveChild
        public new void RemoveChild(IState child, object? argument, Action<IState, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new void RemoveChild(object? argument, Action<IState, object?>? callback) {
            base.RemoveChild( argument, callback );
        }

    }
}
