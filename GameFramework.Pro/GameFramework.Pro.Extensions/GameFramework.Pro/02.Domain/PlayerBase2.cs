#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class PlayerBase2 : PlayerBase {

        private readonly IDependencyProvider m_Provider;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.NotDisposed( $"Player {this} must be non-disposed", !this.IsDisposed );
                return this.m_Provider;
            }
        }

        public PlayerBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
        }
        protected override void OnDispose() {
            base.OnDispose();
        }

    }
}
