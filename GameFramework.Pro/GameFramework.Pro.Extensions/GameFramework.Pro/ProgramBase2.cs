#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ProgramBase2<TTheme, TScreen, TRouter, TApplication> : ProgramBase
        where TTheme : ThemeBase
        where TScreen : ScreenBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        private TTheme theme = default!;
        private TScreen screen = default!;
        private TRouter router = default!;
        private TApplication application = default!;

        protected TTheme Theme {
            get {
                Assert.Operation.Valid( $"Theme must be non-null", this.theme != null );
                return this.theme;
            }
            init {
                Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                this.theme = value;
            }
        }
        protected TScreen Screen {
            get {
                Assert.Operation.Valid( $"Screen must be non-null", this.screen != null );
                return this.screen;
            }
            init {
                Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                this.screen = value;
            }
        }
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

        public ProgramBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
