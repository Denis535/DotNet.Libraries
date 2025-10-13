#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;

    public static class ThemeExtensions {

        public static PlayListBase PlayList(this IState<ThemeBase, PlayListBase> state) {
            return state.UserData;
        }
        public static T PlayList<T>(this IState<ThemeBase, PlayListBase> state) where T : notnull, PlayListBase {
            return (T) state.UserData;
        }

    }
}
