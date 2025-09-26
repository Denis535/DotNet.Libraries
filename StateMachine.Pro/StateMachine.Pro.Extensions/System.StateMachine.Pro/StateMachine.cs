#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StateMachine : StateMachineBase {

        // Root
        public new IState? Root => base.Root;

        // Constructor
        public StateMachine() {
        }
        public override void Dispose() {
            if (this.Root != null) {
                this.Root.Dispose();
            }
            base.Dispose();
        }

        // SetRoot
        public new void SetRoot(IState? root, object? argument, Action<IState, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
    public class StateMachine<TUserData> : StateMachine, IUserData<TUserData> {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public StateMachine(TUserData userData) {
            this.UserData = userData;
        }
        public override void Dispose() {
            (this.UserData as IDisposable)?.Dispose();
            base.Dispose();
        }

    }
}
