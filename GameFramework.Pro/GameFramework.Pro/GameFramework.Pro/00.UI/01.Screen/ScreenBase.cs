#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {

        protected TreeMachine<Node2<WidgetBase>, Node2<WidgetBase>, ScreenBase> Machine { get; }

        public ScreenBase() {
            this.Machine = new TreeMachine<Node2<WidgetBase>, Node2<WidgetBase>, ScreenBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Screen {this} must have no {this.Machine.Root} root", this.Machine.Root == null );
            base.Dispose();
        }

    }
}
