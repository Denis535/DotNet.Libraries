namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
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

    internal class Theme : ThemeBase2<Router, Application> {

        public Theme(Router router, Application application) {
            base.Router = router;
            base.Application = application;
            base.SetState( new MainPlayList(), null, (state, arg) => state.Dispose() );
            base.SetState( new GamePlayList(), null, (state, arg) => state.Dispose() );
        }
        public override void Dispose() {
            base.SetState( null, null, (state, arg) => state.Dispose() );
            base.Dispose();
        }

    }
    internal class MainPlayList : PlayListBase {

        public MainPlayList() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
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

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
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
            base.AddRoot( new RootWidget(), null );
        }
        public override void Dispose() {
            base.RemoveRoot( null, (root, arg) => root.Dispose() );
            base.Dispose();
        }

    }
    internal class RootWidget : WidgetBase {

        public RootWidget() {
            base.AddChild( new MainWidget(), null );
            base.AddChild( new GameWidget(), null );
        }
        public override void Dispose() {
            base.RemoveChildren( null, (child, arg) => child.Dispose() );
            base.Dispose();
        }

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

        protected override void OnBeforeDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDetach(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDetach(WidgetBase descendant, object? argument) {
        }

        protected override void OnBeforeDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(WidgetBase descendant, object? argument) {
        }

        protected override bool TryShowWidget(WidgetBase widget) {
            if (widget is MainWidget) {
                return true;
            }
            if (widget is GameWidget) {
                return true;
            }
            return false;
        }
        protected override bool TryHideWidget(WidgetBase widget) {
            if (widget is MainWidget) {
                return true;
            }
            if (widget is GameWidget) {
                return true;
            }
            return false;
        }

    }
    internal class MainWidget : ViewableWidgetBase<MainWidget.MainWidgetView> {
        internal class MainWidgetView : ViewBase {
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

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        protected override void OnActivate(object? argument) {
            base.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            base.HideSelf();
        }

        protected override void OnBeforeDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDetach(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDetach(WidgetBase descendant, object? argument) {
        }

        protected override void OnBeforeDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(WidgetBase descendant, object? argument) {
        }

    }
    internal class GameWidget : ViewableWidgetBase<GameWidget.GameWidgetView> {
        internal class GameWidgetView : ViewBase {
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

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        protected override void OnActivate(object? argument) {
            base.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            base.HideSelf();
        }

        protected override void OnBeforeDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantAttach(WidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDetach(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDetach(WidgetBase descendant, object? argument) {
        }

        protected override void OnBeforeDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(WidgetBase descendant, object? argument) {
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
}
