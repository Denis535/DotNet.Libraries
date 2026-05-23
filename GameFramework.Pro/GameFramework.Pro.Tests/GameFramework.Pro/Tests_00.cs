#pragma warning disable CA2000 // Dispose objects before losing scope
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            using var program = new Program();
        }

    }

    // Main
    internal class Program : ProgramBase2<Theme, Screen, Router, Application> {

        public Program() {
            this.Application = new Application();
            this.Router = new Router();
            this.Screen = new Screen();
            this.Theme = new Theme();
        }
        protected override void OnDispose() {
        }

    }

    // UI
    internal class Theme : ThemeBase2<Router, Application> {

        public Theme() {
            this.Machine.SetRoot( new MainPlayList().State, null );
            this.Machine.SetRoot( new GamePlayList().State, null );
        }
        protected override void OnDispose() {
            this.Machine.Root!.Dispose();
        }

    }

    internal class MainPlayList : PlayListBase2 {

        public MainPlayList() {
        }
        protected internal override void OnDispose() {
        }

        protected internal override void OnActivate(object? argument) {
        }
        protected internal override void OnDeactivate(object? argument) {
        }

    }

    internal class GamePlayList : PlayListBase2 {

        public GamePlayList() {
        }
        protected internal override void OnDispose() {
        }

        protected internal override void OnActivate(object? argument) {
        }
        protected internal override void OnDeactivate(object? argument) {
        }

    }

    // UI
    internal class Screen : ScreenBase2<Router, Application> {

        public Screen() {
            this.Machine.SetRoot( new RootWidget().Node, null );
        }
        protected override void OnDispose() {
            this.Machine.Root!.Dispose();
        }

    }

    internal class RootWidget : WidgetBase2 {

        public RootWidget() {
            this.Node.AddChild( new MainWidget().Node, null );
            this.Node.AddChild( new GameWidget().Node, null );
        }
        protected internal override void OnDispose() {
            this.Node.Children.Reverse().DisposeAll();
        }

        protected internal override void OnActivate(object? argument) {
        }
        protected internal override void OnDeactivate(object? argument) {
        }

    }

    internal class MainWidget : ViewableWidgetBase2<MainWidget.View> {

        internal new class View {
            public View() {
            }
        }

        public MainWidget() {
            base.View = new View();
        }
        protected internal override void OnDispose() {
            this.Node.Children.Reverse().DisposeAll();
        }

        protected internal override void OnActivate(object? argument) {
        }
        protected internal override void OnDeactivate(object? argument) {
        }

    }

    internal class GameWidget : ViewableWidgetBase2<GameWidget.View> {
        internal new class View {
            public View() {
            }
        }

        public GameWidget() {
            base.View = new View();
        }
        protected internal override void OnDispose() {
            this.Node.Children.Reverse().DisposeAll();
        }

        protected internal override void OnActivate(object? argument) {
        }
        protected internal override void OnDeactivate(object? argument) {
        }

    }

    // UI
    internal class Router : RouterBase2<Theme, Screen, Application> {

        public Router() {
        }
        protected override void OnDispose() {
        }

    }

    // App
    internal class Application : ApplicationBase2 {

        private Game Game { get; }

        public Application() {
            this.Game = new Game();
        }
        protected override void OnDispose() {
            this.Game.Dispose();
        }

    }

    // Game
    internal class Game : GameBase2 {

        public Game() {
        }
        protected override void OnDispose() {
        }

    }
}
