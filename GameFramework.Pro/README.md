# Overview

The framework that allows you to design high-quality architecture of your game project.

# Reference

###### GameFramework.Pro

```
namespace GameFramework.Pro;
public abstract class ProgramBase : DisposableBase {

    public ProgramBase();
    public override void Dispose();

}
// UI
public abstract class ThemeBase : DisposableBase {

    protected StateMachine<State<PlayListBase>, ThemeBase> Machine { get; }

    public ThemeBase();
    public override void Dispose();

}
public abstract class ScreenBase : DisposableBase {

    protected TreeMachine<Node2<WidgetBase>, ScreenBase> Machine { get; }

    public ScreenBase();
    public override void Dispose();

}
public abstract class PlayListBase : DisposableBase {

    public State<PlayListBase> State { get; }

    public PlayListBase();
    public override void Dispose();

    protected virtual void OnAttach(object? argument);
    protected virtual void OnDetach(object? argument);

    protected virtual void OnActivate(object? argument);
    protected virtual void OnDeactivate(object? argument);

}
public abstract class WidgetBase : DisposableBase {

    public Node2<WidgetBase> Node { get; }

    public WidgetBase();
    public override void Dispose();

    protected virtual void OnAttach(object? argument);
    protected virtual void OnDetach(object? argument);

    protected virtual void OnActivate(object? argument);
    protected virtual void OnDeactivate(object? argument);

    protected virtual void OnBeforeDescendantAttach(Node2<WidgetBase> descendant, object? argument);
    protected virtual void OnAfterDescendantAttach(Node2<WidgetBase> descendant, object? argument);
    protected virtual void OnBeforeDescendantDetach(Node2<WidgetBase> descendant, object? argument);
    protected virtual void OnAfterDescendantDetach(Node2<WidgetBase> descendant, object? argument);

    protected virtual void OnBeforeDescendantActivate(Node2<WidgetBase> descendant, object? argument);
    protected virtual void OnAfterDescendantActivate(Node2<WidgetBase> descendant, object? argument);
    protected virtual void OnBeforeDescendantDeactivate(Node2<WidgetBase> descendant, object? argument);
    protected virtual void OnAfterDescendantDeactivate(Node2<WidgetBase> descendant, object? argument);

}
public abstract class ViewableWidgetBase : WidgetBase {

    protected IView View { get; init; }

    public ViewableWidgetBase();
    public override void Dispose();

}
public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : notnull, IView {

    protected new TView View { get; init; }

    public ViewableWidgetBase();
    public override void Dispose();

}
public abstract class ViewBase : DisposableBase, IView {

    public ViewBase();
    public override void Dispose();

}
public abstract class RouterBase : DisposableBase {

    public RouterBase();
    public override void Dispose();

}
// App
public abstract class ApplicationBase : DisposableBase {

    public ApplicationBase();
    public override void Dispose();

}
// Game (domain, business)
public abstract class GameBase : DisposableBase {

    public GameBase();
    public override void Dispose();

}
public abstract class PlayerBase : DisposableBase {

    public PlayerBase();
    public override void Dispose();

}
public abstract class EntityBase : DisposableBase {

    public EntityBase();
    public override void Dispose();

}
```

###### GameFramework.Pro.Extensions

```
namespace GameFramework.Pro.Extensions;
public abstract class ProgramBase2<TTheme, TScreen, TRouter, TApplication> : ProgramBase where TTheme : ThemeBase where TScreen : ScreenBase where TRouter : RouterBase where TApplication : ApplicationBase {

    protected TTheme Theme { get; init; }
    protected TScreen Screen { get; init; }
    protected TRouter Router { get; init; }
    protected TApplication Application { get; init; }

    public ProgramBase2();
    public override void Dispose();

}
// UI
public abstract class ThemeBase2<TRouter, TApplication> : ThemeBase where TRouter : RouterBase where TApplication : ApplicationBase {

    protected TRouter Router { get; init; }
    protected TApplication Application { get; init; }

    public ThemeBase2();
    public override void Dispose();

}
public abstract class ScreenBase2<TRouter, TApplication> : ScreenBase where TRouter : RouterBase where TApplication : ApplicationBase {

    protected TRouter Router { get; init; }
    protected TApplication Application { get; init; }

    public ScreenBase2();
    public override void Dispose();

}
public abstract class RouterBase2<TTheme, TScreen, TApplication> : RouterBase where TTheme : ThemeBase where TScreen : ScreenBase where TApplication : ApplicationBase {

    protected TTheme Theme { get; }
    protected Func<TTheme> Theme_ { init; }
    protected TScreen Screen { get; }
    protected Func<TScreen> Screen_ { init; }
    protected TApplication Application { get; init; }

    public RouterBase2();
    public override void Dispose();

}
```

# Links

- https://github.com/Denis535/DotNet.Libraries/tree/main/GameFramework.Pro
- https://nuget.org/packages/GameFramework.Pro
- https://youtu.be/u7wjaaMr6wQ