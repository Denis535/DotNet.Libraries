#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class StateMachine<TRoot, TUserData> : StateMachineBase<TRoot> where TRoot : class, IState {

        // Root
        public new TRoot? Root => base.Root;

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public StateMachine(TUserData userData) {
            this.UserData = userData;
        }

        // SetRoot
        public new void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
}
