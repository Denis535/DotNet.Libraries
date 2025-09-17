#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ThemeBase2<TRouter, TApplication> : ThemeBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        protected IDependencyProvider Provider { get; }
        protected TRouter Router { get; }
        protected TApplication Application { get; }

        public ThemeBase2(IDependencyProvider provider) {
            this.Provider = provider;
            this.Router = provider.RequireDependency<TRouter>();
            this.Application = provider.RequireDependency<TApplication>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
