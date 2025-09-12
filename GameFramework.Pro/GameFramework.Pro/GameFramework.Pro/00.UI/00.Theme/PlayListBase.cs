#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : DisposableBase {

        internal PlayListBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class PlayListBase<TThis> : PlayListBase where TThis : PlayListBase {

        public State<TThis> State { get; }

        public PlayListBase() {
            this.State = new State<TThis>( (TThis) (object) this );
            this.State.OnActivateCallback += this.OnActivate;
            this.State.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"PlayList {this} must be inactive", this.State.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected virtual void OnActivate(object? argument) {
        }
        protected virtual void OnDeactivate(object? argument) {
        }

    }
}
