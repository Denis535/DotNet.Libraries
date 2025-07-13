namespace GameFramework.Pro.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ThemeBase2<TRouter, TApplication> : ThemeBase where TRouter : RouterBase where TApplication : ApplicationBase {

        private TRouter router = default!;
        private TApplication application = default!;

        protected TRouter Router {
            get {
                Assert.Operation.Valid( $"Router must be non-null", this.router != null );
                return this.router;
            }
            init {
                Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                this.router = value;
            }
        }
        protected TApplication Application {
            get {
                Assert.Operation.Valid( $"Application must be non-null", this.application != null );
                return this.application;
            }
            init {
                Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                this.application = value;
            }
        }

        public ThemeBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
