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
            this.Application = new Application( this );
            this.Router = new Router( this );
            this.Screen = new Screen( this );
            this.Theme = new Theme( this );
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Program {this} must be non-disposed", !this.IsDisposed );
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
            this.Machine.SetRoot( new MainPlayList( this.Provider ).State, null, null );
            this.Machine.SetRoot( new GamePlayList( this.Provider ).State, null, null );
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Theme {this} must be non-disposed", !this.IsDisposed );
            this.Machine.SetRoot( null, null, null );
            base.Dispose();
        }

    }
    internal class MainPlayList : PlayListBase2 {

        public MainPlayList(IDependencyProvider provider) : base( provider ) {
        }
        protected override void OnDispose() {
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class GamePlayList : PlayListBase2 {

        public GamePlayList(IDependencyProvider provider) : base( provider ) {
        }
        protected override void OnDispose() {
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class Screen : ScreenBase2<Router, Application> {

        public Screen(IDependencyProvider provider) : base( provider ) {
            this.Machine.SetRoot( new RootWidget( this.Provider ).Node, null, null );
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Screen {this} must be non-disposed", !this.IsDisposed );
            this.Machine.SetRoot( null, null, null );
            base.Dispose();
        }

    }
    internal class RootWidget : WidgetBase2 {

        public RootWidget(IDependencyProvider provider) : base( provider ) {
            this.NodeMutable.AddChild( new MainWidget( this.Provider ).Node, null );
            this.NodeMutable.AddChild( new GameWidget( this.Provider ).Node, null );
        }
        protected override void OnDispose() {
            _ = this.NodeMutable.RemoveChildren( i => true, null, null );
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class MainWidget : ViewableWidgetBase2<MainWidget.View> {
        new internal class View {
            public View() {
            }
        }

        public MainWidget(IDependencyProvider provider) : base( provider ) {
            base.View = new View();
        }
        protected override void OnDispose() {
            _ = this.NodeMutable.RemoveChildren( i => true, null, null );
        }

        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

    }
    internal class GameWidget : ViewableWidgetBase2<GameWidget.View> {
        new internal class View {
            public View() {
            }
        }

        public GameWidget(IDependencyProvider provider) : base( provider ) {
            base.View = new View();
        }
        protected override void OnDispose() {
            _ = this.NodeMutable.RemoveChildren( i => true, null, null );
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
            System.Assert.Operation.NotDisposed( $"Router {this} must be non-disposed", !this.IsDisposed );
            base.Dispose();
        }

    }
    // App
    internal class Application : ApplicationBase2 {

        private Game Game { get; init; }

        public Application(IDependencyProvider provider) : base( provider ) {
            this.Game = new Game( provider );
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Application {this} must be non-disposed", !this.IsDisposed );
            this.Game.Dispose();
            base.Dispose();
        }

    }
    // Domain
    internal class Game : GameBase2 {

        private Player Player { get; init; }
        private Entity Entity { get; init; }

        public Game(IDependencyProvider provider) : base( provider ) {
            this.Player = new Player( provider );
            this.Entity = new Entity();
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Game {this} must be non-disposed", !this.IsDisposed );
            this.Entity.Dispose();
            this.Player.Dispose();
            base.Dispose();
        }

    }
    internal class Player : PlayerBase2 {

        public Player(IDependencyProvider provider) : base( provider ) {
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Player {this} must be non-disposed", !this.IsDisposed );
            base.Dispose();
        }

    }
    internal class Entity : EntityBase {

        public Entity() {
        }
        public override void Dispose() {
            System.Assert.Operation.NotDisposed( $"Entity {this} must be non-disposed", !this.IsDisposed );
            base.Dispose();
        }

    }
}
