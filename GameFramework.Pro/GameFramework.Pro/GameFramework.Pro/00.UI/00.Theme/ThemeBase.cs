#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {

        private readonly StateMachine<ThemeBase> m_Machine;

        protected StateMachine<ThemeBase> Machine {
            get {
                Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
                return this.m_Machine;
            }
        }

        public ThemeBase() {
            this.m_Machine = new StateMachine<ThemeBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
            if (!this.Machine.IsDisposed) {
                this.Machine.Dispose();
                return;
            }
            base.Dispose();
        }

    }
}
