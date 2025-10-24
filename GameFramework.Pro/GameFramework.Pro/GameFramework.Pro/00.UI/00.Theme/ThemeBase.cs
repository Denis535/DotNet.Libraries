#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {

        private readonly StateMachine<ThemeBase, PlayListBase> m_Machine;

        protected StateMachine<ThemeBase, PlayListBase> Machine {
            get {
                Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
                return this.m_Machine;
            }
        }

        public ThemeBase() {
            this.m_Machine = new StateMachine<ThemeBase, PlayListBase>( this );
        }
        protected override void OnDispose() {
            this.Machine.Dispose();
        }

    }
}
