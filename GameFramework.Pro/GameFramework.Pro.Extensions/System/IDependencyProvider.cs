#nullable enable
namespace System {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDependencyProvider {

        public sealed T? GetDependency<T>(object? argument = null) where T : notnull {
            var value = this.GetValue( typeof( T ), argument );
            return (T?) value;
        }
        public sealed T? GetDependency<T>(Type type, object? argument = null) where T : notnull {
            var value = this.GetValue( type, argument );
            return (T?) value;
        }

        public sealed T RequireDependency<T>(object? argument = null) where T : notnull {
            var value = this.GetValue( typeof( T ), argument );
            Assert.Operation.Valid( $"Dependency {typeof( T )} ({argument}) was not found", value != null );
            return (T) value;
        }
        public sealed T RequireDependency<T>(Type type, object? argument = null) where T : notnull {
            var value = this.GetValue( type, argument );
            Assert.Operation.Valid( $"Dependency {type} ({argument}) was not found", value != null );
            return (T) value;
        }

        protected object? GetValue(Type type, object? argument);

    }
}
