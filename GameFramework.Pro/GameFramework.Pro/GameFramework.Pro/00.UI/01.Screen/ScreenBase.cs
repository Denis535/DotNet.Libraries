#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {

        private readonly TreeMachine<ScreenBase> m_Machine;

        protected TreeMachine<ScreenBase> Machine {
            get {
                Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
                return this.m_Machine;
            }
        }

        public ScreenBase() {
            this.m_Machine = new TreeMachine<ScreenBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
            if (!this.Machine.IsDisposed) {
                this.Machine.Dispose();
                return;
            }
            base.Dispose();
        }

    }
}
