#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Text;

    public abstract class PlayListBase : DisposableBase {

        protected ThemeBase? Theme => ((IUserData<ThemeBase>?) this.State.Machine)?.UserData;
        public IState State => this.StateMutable;
        protected internal State<PlayListBase> StateMutable { get; }

        public PlayListBase() {
            this.StateMutable = new State<PlayListBase>( this );
            this.StateMutable.OnActivateCallback += this.OnActivate;
            this.StateMutable.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"PlayList {this} must be inactive", this.State.Activity == Activity.Inactive );
            base.Dispose();
        }

        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

    }
}
