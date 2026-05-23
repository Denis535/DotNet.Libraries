#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal static class Exceptions {
        public static class Argument {

            public static ArgumentException Invalid(FormattableString? message) {
                return new ArgumentException( message: message?.ToString() );
            }
            public static ArgumentNullException Null(FormattableString? message) {
                return new ArgumentNullException( null, message: message?.ToString() );
            }
            public static ArgumentOutOfRangeException OutOfRange(FormattableString? message) {
                return new ArgumentOutOfRangeException( null, message: message?.ToString() );
            }

        }

        public static class Operation {

            public static InvalidOperationException Invalid(FormattableString? message) {
                return new InvalidOperationException( message: message?.ToString() );
            }
            public static InvalidOperationException NotReady(FormattableString? message) {
                return new InvalidOperationException( message: message?.ToString() );
            }
            public static ObjectDisposedException Disposed(FormattableString? message) {
                return new ObjectDisposedException( null, message: message?.ToString() );
            }

        }

        public static class Internal {

            public static Exception Exception(FormattableString? message) {
                return new Exception( message: message?.ToString() );
            }
            public static NullReferenceException NullReference(FormattableString? message) {
                return new NullReferenceException( message: message?.ToString() );
            }
            public static NotSupportedException NotSupported(FormattableString? message) {
                return new NotSupportedException( message: message?.ToString() );
            }
            public static NotImplementedException NotImplemented(FormattableString? message) {
                return new NotImplementedException( message: message?.ToString() );
            }

        }
    }
}
