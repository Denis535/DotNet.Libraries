#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {

        internal ScreenBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class ScreenBase<TRoot> : ScreenBase where TRoot : WidgetBase {

        protected TreeMachine<Node2<TRoot>, ScreenBase> Machine { get; }

        public ScreenBase() {
            this.Machine = new TreeMachine<Node2<TRoot>, ScreenBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Screen {this} must have no {this.Machine.Root} root", this.Machine.Root == null );
            base.Dispose();
        }

    }
}
