#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class GameBase2 : GameBase {

        private readonly IDependencyProvider m_Provider;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.NotDisposed( $"Game {this} must be non-disposed", !this.IsDisposed );
                return this.m_Provider;
            }
        }

        public GameBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
