#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.TreeMachine.Pro;

    public static class ScreenExtensions {

        public static WidgetBase Widget(this INode<ScreenBase, WidgetBase> node) {
            return node.UserData;
        }
        public static T Widget<T>(this INode<ScreenBase, WidgetBase> node) where T : notnull, WidgetBase {
            return (T) node.UserData;
        }

        public static CancellationToken GetCancellationToken_OnDetachCallback(this WidgetBase widget) {
            return widget.Node.GetCancellationToken_OnDetachCallback();
        }
        public static CancellationToken GetCancellationToken_OnDetachCallback(this INode<ScreenBase, WidgetBase> node) {
            // todo: should we trigger event if the state is already non-attached?
            var cts = new CancellationTokenSource();
            node.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }

        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this WidgetBase widget) {
            return widget.Node.GetCancellationToken_OnDeactivateCallback();
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this INode<ScreenBase, WidgetBase> node) {
            // todo: should we trigger event if the state is already inactive?
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
