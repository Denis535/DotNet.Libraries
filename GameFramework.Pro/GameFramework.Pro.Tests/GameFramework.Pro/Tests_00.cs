#pragma warning disable CA2000 // Dispose objects before losing scope
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;
    using GameFramework.Pro.Extensions;
    using NUnit.Framework;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            using var program = new Program();
        }

    }
    internal class Program : ProgramBase2<Theme, Screen, Router, Application> {

        public Program() {
            base.Application = new Application();
            base.Router = new Router( () => base.Theme, () => base.Screen, base.Application );
            base.Screen = new Screen( base.Router, base.Application );
            base.Theme = new Theme( base.Router, base.Application );
        }
        public override void Dispose() {
            base.Theme.Dispose();
            base.Screen.Dispose();
            base.Router.Dispose();
            base.Application.Dispose();
            base.Dispose();
        }

    }
    // UI
    internal class Theme : ThemeBase2<Router, Application> {

        public Theme(Router router, Application application) {
            base.Router = router;
            base.Application = application;
            base.Machine.SetRoot( new MainPlayList().State, null, (state, arg) => state.UserData.Dispose() );
            base.Machine.SetRoot( new GamePlayList().State, null, (state, arg) => state.UserData.Dispose() );
        }
        public override void Dispose() {
            base.Machine.SetRoot( null, null, (state, arg) => state.UserData.Dispose() );
            base.Dispose();
        }

    }
    internal class MainPlayList : PlayListBase {

        public MainPlayList() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class GamePlayList : PlayListBase {

        public GamePlayList() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class Screen : ScreenBase2<Router, Application> {

        public Screen(Router router, Application application) {
            base.Router = router;
            base.Application = application;
            base.Machine.SetRoot( new RootWidget().Node, null, null );
        }
        public override void Dispose() {
            base.Machine.SetRoot( null, null, (root, arg) => root.UserData.Dispose() );
            base.Dispose();
        }

    }
    internal class RootWidget : WidgetBase {

        public RootWidget() {
            base.Node.AddChild( new MainWidget().Node, null );
            base.Node.AddChild( new GameWidget().Node, null );
        }
        public override void Dispose() {
            _ = base.Node.RemoveChildren( null, (child, arg) => ((Node2<WidgetBase>) child).UserData.Dispose() );
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class MainWidget : ViewableWidgetBase<MainWidget.MainWidgetView> {
        internal class MainWidgetView : DisposableBase {
            public MainWidgetView() {
            }
            public override void Dispose() {
                base.Dispose();
            }
        }

        public MainWidget() {
            this.View = new MainWidgetView();
        }
        public override void Dispose() {
            _ = base.Node.RemoveChildren( null, (child, arg) => ((Node2<WidgetBase>) child).UserData.Dispose() );
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class GameWidget : ViewableWidgetBase<GameWidget.GameWidgetView> {
        internal class GameWidgetView : DisposableBase {
            public GameWidgetView() {
            }
            public override void Dispose() {
                base.Dispose();
            }
        }

        public GameWidget() {
            this.View = new GameWidgetView();
        }
        public override void Dispose() {
            _ = base.Node.RemoveChildren( null, (child, arg) => ((Node2<WidgetBase>) child).UserData.Dispose() );
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class Router : RouterBase2<Theme, Screen, Application> {

        public Router(Func<Theme> theme, Func<Screen> screen, Application application) {
            base.Theme_ = theme;
            base.Screen_ = screen;
            base.Application = application;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // App
    internal class Application : ApplicationBase {

        private Game Game { get; init; }

        public Application() {
            this.Game = new Game();
        }
        public override void Dispose() {
            this.Game.Dispose();
            base.Dispose();
        }

    }
    // Domain
    internal class Game : GameBase {

        private Player Player { get; init; }
        private Entity Entity { get; init; }

        public Game() {
            this.Player = new Player();
            this.Entity = new Entity();
        }
        public override void Dispose() {
            this.Entity.Dispose();
            this.Player.Dispose();
            base.Dispose();
        }

    }
    internal class Player : PlayerBase {

        public Player() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    internal class Entity : EntityBase {

        public Entity() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
