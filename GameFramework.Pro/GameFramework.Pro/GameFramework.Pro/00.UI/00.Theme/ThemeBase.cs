#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {

        private readonly StateMachine<PlayListBase.State2> m_Machine;

        protected StateMachine<PlayListBase.State2> Machine {
            get {
                Check.Operation.Alive( $"Theme {this} must be alive", !this.IsDisposed );
                return this.m_Machine;
            }
        }

        public ThemeBase() {
            this.m_Machine = new StateMachine<PlayListBase.State2>();
        }
        private protected override void OnDisposeInternal() {
            this.Machine.Dispose();
        }

    }
}
