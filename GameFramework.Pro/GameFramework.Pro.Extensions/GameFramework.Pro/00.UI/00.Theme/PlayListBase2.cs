#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class PlayListBase2 : PlayListBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"PlayList {this} must be alive", !this.State.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public PlayListBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
