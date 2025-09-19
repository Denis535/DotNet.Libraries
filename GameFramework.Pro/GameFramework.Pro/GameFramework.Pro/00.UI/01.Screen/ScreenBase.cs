#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {

        protected TreeMachine<ScreenBase> Machine { get; }

        public ScreenBase() {
            this.Machine = new TreeMachine<ScreenBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Screen {this} must have no {this.Machine.Root} root", this.Machine.Root == null );
            base.Dispose();
        }

    }
}
