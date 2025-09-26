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

        Option<object?> IDependencyProvider.GetValue(Type type, object? argument) {
            return this.GetValue( type, argument );
        }
        protected virtual Option<object?> GetValue(Type type, object? argument) {
            Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
            if (typeof( TTheme ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Theme must be non-null", this.Theme != null );
                Assert.Operation.NotDisposed( $"Theme {this.Theme} must be non-disposed", !this.Theme.IsDisposed );
                return Option.Create<object?>( this.Theme );
            }
            if (typeof( TScreen ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Screen must be non-null", this.Screen != null );
                Assert.Operation.NotDisposed( $"Screen {this.Screen} must be non-disposed", !this.Screen.IsDisposed );
                return Option.Create<object?>( this.Screen );
            }
            if (typeof( TRouter ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Router must be non-null", this.Router != null );
                Assert.Operation.NotDisposed( $"Router {this.Router} must be non-disposed", !this.Router.IsDisposed );
                return Option.Create<object?>( this.Router );
            }
            if (typeof( TApplication ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Application must be non-null", this.Application != null );
                Assert.Operation.NotDisposed( $"Application {this.Application} must be non-disposed", !this.Application.IsDisposed );
                return Option.Create<object?>( this.Application );
            }
            return default;
        }

    }
}
