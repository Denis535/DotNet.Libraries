#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ThemeBase2<TRouter, TApplication> : ThemeBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        private readonly TRouter m_Router;
        private readonly TApplication m_Application;

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Theme {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        protected TRouter Router {
            get {
                Check.Operation.Alive( $"Theme {this} must be alive", !this.IsDisposed );
                return this.m_Router;
            }
        }

        protected TApplication Application {
            get {
                Check.Operation.Alive( $"Theme {this} must be alive", !this.IsDisposed );
                return this.m_Application;
            }
        }

        public ThemeBase2() {
            this.m_Router = this.Provider.RequireDependency<TRouter>();
            this.m_Application = this.Provider.RequireDependency<TApplication>();
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
