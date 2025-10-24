#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ProgramBase2<TTheme, TScreen, TRouter, TApplication> : ProgramBase, IDependencyProvider
        where TTheme : ThemeBase
        where TScreen : ScreenBase
        where TRouter : RouterBase
        where TApplication : ApplicationBase {

        private readonly TTheme m_Theme = default!;
        private readonly TScreen m_Screen = default!;
        private readonly TRouter m_Router = default!;
        private readonly TApplication m_Application = default!;

        protected TTheme Theme {
            get {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                Assert.Operation.Valid( $"Theme must be non-null", this.m_Theme != null );
                return this.m_Theme;
            }
            init {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                this.m_Theme = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }
        protected TScreen Screen {
            get {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                Assert.Operation.Valid( $"Screen must be non-null", this.m_Screen != null );
                return this.m_Screen;
            }
            init {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                this.m_Screen = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }
        protected TRouter Router {
            get {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                Assert.Operation.Valid( $"Router must be non-null", this.m_Router != null );
                return this.m_Router;
            }
            init {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                this.m_Router = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }
        protected TApplication Application {
            get {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                Assert.Operation.Valid( $"Application must be non-null", this.m_Application != null );
                return this.m_Application;
            }
            init {
                Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
                this.m_Application = value ?? throw new ArgumentNullException( nameof( value ) );
            }
        }

        public ProgramBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        object? IDependencyProvider.GetValue(Type type, object? argument) {
            Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
            return this.GetValue( type, argument );
        }
        protected virtual object? GetValue(Type type, object? argument) {
            if (type.IsAssignableFrom( this.Theme.GetType() )) {
                return this.Theme;
            }
            if (type.IsAssignableFrom( this.Screen.GetType() )) {
                return this.Screen;
            }
            if (type.IsAssignableFrom( this.Router.GetType() )) {
                return this.Router;
            }
            if (type.IsAssignableFrom( this.Application.GetType() )) {
                return this.Application;
            }
            return default;
        }

    }
}
