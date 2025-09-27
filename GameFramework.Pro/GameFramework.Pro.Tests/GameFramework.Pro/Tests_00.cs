#pragma warning disable CA2000 // Dispose objects before losing scope
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            using var program = new Program();
        }

    }
    internal class Program : ProgramBase2<Theme, Screen, Router, Application> {

        public Program() {
            this.Application = new Application();
            this.Router = new Router( this );
            this.Screen = new Screen( this );
            this.Theme = new Theme( this );
        }
        public override void Dispose() {
            this.Theme.Dispose();
            this.Screen.Dispose();
            this.Router.Dispose();
            this.Application.Dispose();
            base.Dispose();
        }

    }
    // UI
    internal class Theme : ThemeBase2<Router, Application> {

        public Theme(IDependencyProvider provider) : base( provider ) {
            this.Machine.SetRoot( new MainPlayList().State, null, null );
            this.Machine.SetRoot( new GamePlayList().State, null, null );
        }
        public override void Dispose() {
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

        public Screen(IDependencyProvider provider) : base( provider ) {
            this.Machine.SetRoot( new RootWidget().Node, null, null );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    internal class RootWidget : WidgetBase {

        public RootWidget() {
            this.NodeMutable.AddChild( new MainWidget().Node, null );
            this.NodeMutable.AddChild( new GameWidget().Node, null );
        }
        public override void Dispose() {
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
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class Router : RouterBase2<Theme, Screen, Application> {

        public Router(IDependencyProvider provider) : base( provider ) {
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
            this.Player.Dispose();
            this.Entity.Dispose();
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
