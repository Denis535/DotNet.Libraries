namespace GameFramework.Pro.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class RouterBase2<TTheme, TScreen, TApplication> : ProgramBase where TTheme : ThemeBase where TScreen : ScreenBase where TApplication : ApplicationBase {

        private Func<TTheme> theme = default!;
        private Func<TScreen> screen = default!;
        private TApplication application = default!;

        protected TTheme Theme {
            get {
                Assert.Operation.Valid( $"Theme must be non-null", this.theme != null );
                return this.theme() ?? throw new InvalidOperationException( $"Theme must be non-null" );
            }
        }
        protected Func<TTheme> Theme_ {
            init {
                Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                this.theme = value;
            }
        }
        protected TScreen Screen {
            get {
                Assert.Operation.Valid( $"Theme must be non-null", this.screen != null );
                return this.screen() ?? throw new InvalidOperationException( $"Screen must be non-null" );
            }
        }
        protected Func<TScreen> Screen_ {
            init {
                Assert.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                this.screen = value;
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

        public RouterBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
