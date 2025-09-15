#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : DisposableBase {

        public State<PlayListBase> State { get; }
        protected ThemeBase? Theme => ((StateMachine<State<PlayListBase>, ThemeBase>?) this.State.Machine)?.UserData;

        public PlayListBase() {
            this.State = new State<PlayListBase>( this );
            this.State.OnActivateCallback += this.OnActivate;
            this.State.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"PlayList {this} must be inactive", this.State.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
    public static class StateExtensions {
        public static PlayListBase PlayList(this StateBase state) {
            return ((State<PlayListBase>) state).UserData;
        }
        public static T PlayList<T>(this StateBase state) where T : PlayListBase {
            return (T) ((State<PlayListBase>) state).UserData;
        }
    }
}
