#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class RouterBase2<TTheme, TScreen, TApplication> : RouterBase
        where TTheme : ThemeBase
        where TScreen : ScreenBase
        where TApplication : ApplicationBase {

        private readonly IDependencyProvider m_Provider;
        private readonly TApplication m_Application;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.NotDisposed( $"Router {this} must be non-disposed", !this.IsDisposed );
                return this.m_Provider;
            }
        }
        protected TTheme Theme => this.Provider.RequireDependency<TTheme>();
        protected TScreen Screen => this.Provider.RequireDependency<TScreen>();
        protected TApplication Application {
            get {
                Assert.Operation.NotDisposed( $"Router {this} must be non-disposed", !this.IsDisposed );
                return this.m_Application;
            }
        }

        public RouterBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
            this.m_Application = provider.RequireDependency<TApplication>();
        }
        protected override void OnDispose() {
            base.OnDispose();
        }

    }
}
