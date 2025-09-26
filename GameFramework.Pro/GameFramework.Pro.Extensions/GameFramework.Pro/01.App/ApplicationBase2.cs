#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ApplicationBase2 : ApplicationBase {

        private readonly IDependencyProvider m_Provider;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.NotDisposed( $"Application {this} must be non-disposed", !this.IsDisposed );
                return this.m_Provider;
            }
        }

        public ApplicationBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
