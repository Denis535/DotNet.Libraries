#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial class State<T> {

        // Attach
        void IState<T>.Attach(StateMachine<T> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void IState<T>.Attach(T parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void IState<T>.Detach(StateMachine<T> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void IState<T>.Detach(T parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void IState<T>.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void IState<T>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

    }
}
