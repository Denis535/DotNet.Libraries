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

        protected TTheme Theme { get; init; } = default!;
        protected TScreen Screen { get; init; } = default!;
        protected TRouter Router { get; init; } = default!;
        protected TApplication Application { get; init; } = default!;

        public ProgramBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        Option<object?> IDependencyProvider.GetValue(Type type, object? argument) {
            return this.GetValue( type, argument );
        }
        protected virtual Option<object?> GetValue(Type type, object? argument) {
            if (typeof( TTheme ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Theme must be non-null", this.Theme != null );
                return Option.Create<object?>( this.Theme );
            }
            if (typeof( TScreen ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Screen must be non-null", this.Screen != null );
                return Option.Create<object?>( this.Screen );
            }
            if (typeof( TRouter ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Router must be non-null", this.Router != null );
                return Option.Create<object?>( this.Router );
            }
            if (typeof( TApplication ).IsAssignableFrom( type )) {
                Assert.Operation.Valid( $"Application must be non-null", this.Application != null );
                return Option.Create<object?>( this.Application );
            }
            return default;
        }

    }
}
