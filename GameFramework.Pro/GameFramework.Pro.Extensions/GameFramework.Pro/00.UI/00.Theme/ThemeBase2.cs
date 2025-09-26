#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ThemeBase2<TRouter, TApplication> : ThemeBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        private readonly IDependencyProvider m_Provider;
        private readonly TRouter m_Router;
        private readonly TApplication m_Application;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
                return this.m_Provider;
            }
        }
        protected TRouter Router {
            get {
                Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
                return this.m_Router;
            }
        }
        protected TApplication Application {
            get {
                Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
                return this.m_Application;
            }
        }

        public ThemeBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
            this.m_Router = provider.RequireDependency<TRouter>();
            this.m_Application = provider.RequireDependency<TApplication>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
