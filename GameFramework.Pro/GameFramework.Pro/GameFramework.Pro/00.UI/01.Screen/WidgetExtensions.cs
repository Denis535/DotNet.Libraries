#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.TreeMachine.Pro;

    public static class WidgetExtensions {

        public static WidgetBase Widget(this INode<WidgetBase> node) {
            return node.UserData;
        }
        public static T Widget<T>(this INode<WidgetBase> node) where T : notnull, WidgetBase {
            return (T) node.UserData;
        }

        public static CancellationToken GetCancellationToken_OnDetachCallback(this INode<WidgetBase> node) {
            var cts = new CancellationTokenSource();
            node.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this INode<WidgetBase> node) {
            var cts = new CancellationTokenSource();
            node.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
