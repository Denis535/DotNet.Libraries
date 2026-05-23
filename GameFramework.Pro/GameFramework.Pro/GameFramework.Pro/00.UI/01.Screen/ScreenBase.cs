#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {

        private readonly TreeMachine<WidgetBase.Node2> m_Machine;

        protected TreeMachine<WidgetBase.Node2> Machine {
            get {
                Check.Operation.Alive( $"Screen {this} must be alive", !this.IsDisposed );
                return this.m_Machine;
            }
        }

        public ScreenBase() {
            this.m_Machine = new TreeMachine<WidgetBase.Node2>();
        }
        private protected override void OnDisposeInternal() {
            this.Machine.Dispose();
        }

    }
}
