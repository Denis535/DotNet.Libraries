#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ApplicationBase2 : ApplicationBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Application {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public ApplicationBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
