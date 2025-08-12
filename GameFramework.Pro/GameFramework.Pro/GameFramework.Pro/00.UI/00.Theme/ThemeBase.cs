#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {

        protected StateMachine<State<PlayListBase>, object?> Machine { get; }

        public ThemeBase() {
            this.Machine = new StateMachine<State<PlayListBase>, object?>( null );
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Theme {this} must have no {this.Machine.State} state", this.Machine.State == null );
            base.Dispose();
        }

    }
}
