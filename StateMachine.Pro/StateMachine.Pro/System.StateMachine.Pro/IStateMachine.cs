#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IStateMachine {

        // IsDisposed
        public bool IsDisposed { get; }

        // Root
        public IState? Root { get; }

        // Dispose
        internal void Dispose();

    }
    public interface IStateMachine<out TUserData> : IStateMachine {

        public TUserData UserData { get; }

    }
}
