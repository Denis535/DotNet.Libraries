#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class GameBase2 : GameBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Game {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public GameBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }

    public abstract class PlayerBase2 : PlayerBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Player {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public PlayerBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }

    public abstract class WorldBase2 : WorldBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"World {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public WorldBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }

    public abstract class EntityBase2 : EntityBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Entity {this} must be alive", !this.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public EntityBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
