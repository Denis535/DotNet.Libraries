#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ScreenBase2<TRouter, TApplication> : ScreenBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        private readonly TRouter m_Router;
        private readonly TApplication m_Application;

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Screen {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        protected TRouter Router {
            get {
                Check.Operation.Alive( $"Screen {this} must be alive", !this.IsDisposed );
                return this.m_Router;
            }
        }

        protected TApplication Application {
            get {
                Check.Operation.Alive( $"Screen {this} must be alive", !this.IsDisposed );
                return this.m_Application;
            }
        }

        public ScreenBase2() {
            this.m_Router = this.Provider.RequireDependency<TRouter>();
            this.m_Application = this.Provider.RequireDependency<TApplication>();
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
