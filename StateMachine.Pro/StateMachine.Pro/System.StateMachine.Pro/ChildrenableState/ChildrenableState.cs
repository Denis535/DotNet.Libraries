#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class ChildrenableState<TUserData> : ChildrenableStateBase {

        // UserData
        public TUserData UserData { get; }

        // Sort
        public Action<List<IState>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public ChildrenableState(TUserData userData) {
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

        // Sort
        protected override void Sort(List<IState> children) {
            this.SortDelegate?.Invoke( children );
        }

    }
}
