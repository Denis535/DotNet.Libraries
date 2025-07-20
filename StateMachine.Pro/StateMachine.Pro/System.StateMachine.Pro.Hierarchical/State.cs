#nullable enable
namespace System.StateMachine.Pro.Hierarchical {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class State<TThis, TUserData> : StateBase<TThis> where TThis : notnull, StateBase<TThis> {

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
        public new void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback) {
            base.SetChild( child, argument, callback );
        }

        // AddChild
        public new void AddChild(TThis child, object? argument) {
            base.AddChild( child, argument );
        }

        // RemoveChild
        public new void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new void RemoveChild(object? argument, Action<TThis, object?>? callback) {
            base.RemoveChild( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

    }
}
