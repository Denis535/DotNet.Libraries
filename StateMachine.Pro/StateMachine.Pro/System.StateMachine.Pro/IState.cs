#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IState<TThis> where TThis : class, IState<TThis> {

        // Machine
        public StateMachineBase<TThis>? Machine { get; }

        // Activity
        public Activity Activity { get; }

        // Attach
        internal void Attach(StateMachineBase<TThis> machine, object? argument);

        // Detach
        internal void Detach(StateMachineBase<TThis> machine, object? argument);

    }
}
