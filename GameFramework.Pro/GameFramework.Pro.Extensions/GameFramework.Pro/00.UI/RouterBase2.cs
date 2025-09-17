#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class RouterBase2<TTheme, TScreen, TApplication> : RouterBase
        where TTheme : ThemeBase
        where TScreen : ScreenBase
        where TApplication : ApplicationBase {

        protected IDependencyProvider Provider { get; }
        protected TTheme Theme => this.Provider.RequireDependency<TTheme>();
        protected TScreen Screen => this.Provider.RequireDependency<TScreen>();
        protected TApplication Application { get; }

        public RouterBase2(IDependencyProvider provider) {
            this.Provider = provider;
            this.Application = provider.RequireDependency<TApplication>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
