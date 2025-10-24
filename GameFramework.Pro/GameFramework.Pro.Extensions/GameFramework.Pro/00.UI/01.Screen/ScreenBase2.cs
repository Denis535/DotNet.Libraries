#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ScreenBase2<TRouter, TApplication> : ScreenBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        private readonly IDependencyProvider m_Provider;
        private readonly TRouter m_Router;
        private readonly TApplication m_Application;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
                return this.m_Provider;
            }
        }
        protected TRouter Router {
            get {
                Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
                return this.m_Router;
            }
        }
        protected TApplication Application {
            get {
                Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
                return this.m_Application;
            }
        }

        public ScreenBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
            this.m_Router = provider.RequireDependency<TRouter>();
            this.m_Application = provider.RequireDependency<TApplication>();
        }
        protected override void OnDispose() {
            base.OnDispose();
        }

    }
}
