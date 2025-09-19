#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Threading;

    public static class PlayListExtensions {

        public static PlayListBase PlayList(this IState state) {
            return ((State<PlayListBase>) state).UserData;
        }
        public static T PlayList<T>(this IState state) where T : notnull, PlayListBase {
            return (T) ((State<PlayListBase>) state).UserData;
        }

        public static CancellationToken GetCancellationToken_OnDetachCallback(this PlayListBase playList) {
            var cts = new CancellationTokenSource();
            playList.StateMutable.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                playList.StateMutable.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this PlayListBase playList) {
            var cts = new CancellationTokenSource();
            playList.StateMutable.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                playList.StateMutable.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
