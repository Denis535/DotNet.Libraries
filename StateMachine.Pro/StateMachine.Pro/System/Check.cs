#nullable enable
namespace System {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    internal static class Check {
        public static class Argument {

            public static void Valid([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Argument.Invalid( null );
            }
            public static void NotNull([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Argument.Null( null );
            }
            public static void InRange([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Argument.OutOfRange( null );
            }

            public static void Valid(FormattableString message, [DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Argument.Invalid( message );
            }
            public static void NotNull(FormattableString message, [DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Argument.Null( message );
            }
            public static void InRange(FormattableString message, [DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Argument.OutOfRange( message );
            }

        }

        public static class Operation {

            public static void Valid([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Operation.Invalid( null );
            }
            public static void Ready([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Operation.NotReady( null );
            }
            public static void Alive([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Operation.Disposed( null );
            }

            public static void Valid(FormattableString message, [DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Operation.Invalid( message );
            }
            public static void Ready(FormattableString message, [DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Operation.NotReady( message );
            }
            public static void Alive(FormattableString message, [DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Operation.Disposed( message );
            }

        }
    }
}
