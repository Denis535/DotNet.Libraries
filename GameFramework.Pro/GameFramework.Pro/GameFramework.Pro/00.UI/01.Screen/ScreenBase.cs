#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {

        private readonly TreeMachine<ScreenBase, WidgetBase> m_Machine;

        protected TreeMachine<ScreenBase, WidgetBase> Machine {
            get {
                Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
                return this.m_Machine;
            }
        }

        public ScreenBase() {
            this.m_Machine = new TreeMachine<ScreenBase, WidgetBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
            this.Machine.Dispose();
            base.Dispose();
        }

    }
}
