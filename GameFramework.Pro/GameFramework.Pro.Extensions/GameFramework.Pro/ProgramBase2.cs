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
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                return this.m_Theme ?? throw Exceptions.Internal.NullReference( $"Theme must be non-null" );
            }
            init {
                Check.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                this.m_Theme = value;
            }
        }

        protected TScreen Screen {
            get {
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                return this.m_Screen ?? throw Exceptions.Internal.NullReference( $"Screen must be non-null" );
            }
            init {
                Check.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                this.m_Screen = value;
            }
        }

        protected TRouter Router {
            get {
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                return this.m_Router ?? throw Exceptions.Internal.NullReference( $"Router must be non-null" );
            }
            init {
                Check.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                this.m_Router = value;
            }
        }

        protected TApplication Application {
            get {
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                return this.m_Application ?? throw Exceptions.Internal.NullReference( $"Application must be non-null" );
            }
            init {
                Check.Argument.NotNull( $"Argument 'value' must be non-null", value != null );
                Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
                this.m_Application = value;
            }
        }

        public ProgramBase2() {
            IDependencyProvider.Instance = this;
        }
        private protected override void OnDisposeInternal() {
            this.Theme.Dispose();
            this.Screen.Dispose();
            this.Router.Dispose();
            this.Application.Dispose();
            IDependencyProvider.Instance = null;
            base.OnDisposeInternal();
        }

        object? IDependencyProvider.GetValue(Type type, object? argument) {
            Check.Operation.Alive( $"Program {this} must be alive", !this.IsDisposed );
            return this.GetValue( type, argument );
        }
        protected virtual object? GetValue(Type type, object? argument) {
            if (type.IsAssignableFrom( typeof(TTheme) )) {
                return this.Theme;
            }
            if (type.IsAssignableFrom( typeof(TScreen) )) {
                return this.Screen;
            }
            if (type.IsAssignableFrom( typeof(TRouter) )) {
                return this.Router;
            }
            if (type.IsAssignableFrom( typeof(TApplication) )) {
                return this.Application;
            }
            return null;
        }

    }
}
