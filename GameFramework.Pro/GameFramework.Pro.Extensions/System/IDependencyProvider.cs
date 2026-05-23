#nullable enable
namespace System {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public interface IDependencyProvider {

        private static IDependencyProvider? m_Instance;

        [AllowNull]
        internal static IDependencyProvider Instance {
            get {
                return m_Instance ?? throw Exceptions.Internal.NullReference( $"Instance must be non-null" );
            }
            set {
                if (value != null) {
                    Check.Operation.Valid( $"Instance {m_Instance} must be null", m_Instance == null );
                    m_Instance = value;
                } else {
                    Check.Operation.Valid( $"Instance must be non-null", m_Instance != null );
                    m_Instance = null;
                }
            }
        }

        public sealed T? GetDependency<T>(object? argument = null) where T : notnull {
            var value = this.GetValue( typeof(T), argument );
            return (T?) value;
        }
        public sealed T? GetDependency<T>(Type type, object? argument = null) where T : notnull {
            var value = this.GetValue( type, argument );
            return (T?) value;
        }

        public sealed T RequireDependency<T>(object? argument = null) where T : notnull {
            var value = this.GetValue( typeof(T), argument );
            Check.Operation.Valid( $"Dependency {typeof(T)} ({argument ?? "Null"}) was not found", value != null );
            return (T) value;
        }
        public sealed T RequireDependency<T>(Type type, object? argument = null) where T : notnull {
            var value = this.GetValue( type, argument );
            Check.Operation.Valid( $"Dependency {type} ({argument ?? "Null"}) was not found", value != null );
            return (T) value;
        }

        protected object? GetValue(Type type, object? argument);

    }
}
