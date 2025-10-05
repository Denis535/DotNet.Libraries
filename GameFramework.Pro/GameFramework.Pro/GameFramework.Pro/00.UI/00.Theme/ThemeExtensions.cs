#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Threading;

    public static class ThemeExtensions {

        public static PlayListBase PlayList(this IState<ThemeBase, PlayListBase> state) {
            return state.UserData;
        }
        public static T PlayList<T>(this IState<ThemeBase, PlayListBase> state) where T : notnull, PlayListBase {
            return (T) state.UserData;
        }

        public static CancellationToken GetCancellationToken_OnDetachCallback(this PlayListBase playList) {
            return playList.State.GetCancellationToken_OnDetachCallback();
        }
        public static CancellationToken GetCancellationToken_OnDetachCallback(this IState<ThemeBase, PlayListBase> state) {
            // todo: should we trigger event if the state is already non-attached?
            var cts = new CancellationTokenSource();
            state.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                state.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }

        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this PlayListBase playList) {
            return playList.State.GetCancellationToken_OnDeactivateCallback();
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this IState<ThemeBase, PlayListBase> state) {
            // todo: should we trigger event if the state is already inactive?
            var cts = new CancellationTokenSource();
            state.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                state.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
