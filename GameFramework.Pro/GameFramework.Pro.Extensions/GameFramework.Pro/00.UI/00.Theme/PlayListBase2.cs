#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class PlayListBase2 : PlayListBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public PlayListBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
