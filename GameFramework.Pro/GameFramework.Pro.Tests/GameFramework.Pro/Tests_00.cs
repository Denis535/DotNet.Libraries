namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using GameFramework.Pro.Extensions;
    using NUnit.Framework;

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

    internal class Theme : ThemeBase2<Router, Application> {

        public Theme(Router router, Application application) {
            base.Router = router;
            base.Application = application;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    internal class Screen : ScreenBase2<Router, Application> {

        public Screen(Router router, Application application) {
            base.Router = router;
            base.Application = application;
        }
        public override void Dispose() {
            base.Dispose();
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

    public class Tests_00 {

        [Test]
        public void Test_00() {
            using var program = new Program();
        }

    }
}
