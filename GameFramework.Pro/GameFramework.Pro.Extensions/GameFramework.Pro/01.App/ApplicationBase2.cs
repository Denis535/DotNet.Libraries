#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ApplicationBase2 : ApplicationBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public ApplicationBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
