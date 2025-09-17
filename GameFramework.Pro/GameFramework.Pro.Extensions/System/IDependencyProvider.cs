#nullable enable
namespace System {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDependencyProvider {

        // GetDependency
        public sealed object? GetDependency(Type type, object? argument = null) {
            var option = this.GetValue( type, argument );
            return option.ValueOrDefault;
        }
        public sealed T? GetDependency<T>(object? argument = null) {
            var option = this.GetValue<T>( argument );
            return (T?) option.ValueOrDefault;
        }

        // RequireDependency
        public sealed object? RequireDependency(Type type, object? argument = null) {
            var option = this.GetValue( type, argument );
            Assert.Operation.Valid( $"Dependency {type} ({argument}) was not found", option.HasValue );
            return option.Value;
        }
        public sealed T RequireDependency<T>(object? argument = null) {
            var option = this.GetValue<T>( argument );
            Assert.Operation.Valid( $"Dependency {typeof( T )} ({argument}) was not found", option.HasValue );
            return (T?) option.Value!;
        }

        // GetValue
        protected Option<object?> GetValue(Type type, object? argument);
        private Option<object?> GetValue<T>(object? argument) {
            return this.GetValue( typeof( T ), argument );
        }

    }
}
