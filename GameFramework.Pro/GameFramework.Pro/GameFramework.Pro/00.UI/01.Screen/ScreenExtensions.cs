#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.TreeMachine.Pro;

    public static class ScreenExtensions {

        public static WidgetBase Widget(this INode<ScreenBase, WidgetBase> node) {
            return node.UserData;
        }
        public static T Widget<T>(this INode<ScreenBase, WidgetBase> node) where T : notnull, WidgetBase {
            return (T) node.UserData;
        }

    }
}
