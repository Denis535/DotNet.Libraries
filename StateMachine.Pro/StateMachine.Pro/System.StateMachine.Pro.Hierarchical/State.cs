#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class State<TUserData> : StateBase<State<TUserData>> {

        // UserData
        public TUserData UserData { get; private set; }

        // Constructor
        public State(TUserData userData) {
            this.UserData = userData;
        }

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
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
