#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class ThemeBase : DisposableBase {

        internal ThemeBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class ThemeBase<TRoot> : ThemeBase where TRoot : PlayListBase {

        protected StateMachine<State<TRoot>, ThemeBase> Machine { get; }

        public ThemeBase() {
            this.Machine = new StateMachine<State<TRoot>, ThemeBase>( this );
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Theme {this} must have no {this.Machine.Root} root", this.Machine.Root == null );
            base.Dispose();
        }

    }
}
