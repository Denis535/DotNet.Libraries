# Overview

The framework that allows you to design high-quality architecture of your game project.

# Reference

###### GameFramework.Pro

```
public abstract class ProgramBase : DisposableBase {

    public ProgramBase();
    public override void Dispose();

}
```
```
// UI
public abstract class ThemeBase : DisposableBase {

    protected StateMachine<State<PlayListBase>, ThemeBase> Machine { get; }

    public ThemeBase();
    public override void Dispose();

}
public abstract class PlayListBase : DisposableBase {

    public State<PlayListBase> State { get; }
    protected ThemeBase? Theme { get; }

    public PlayListBase();
    public override void Dispose();

    protected abstract void OnActivate(object? argument);
    protected abstract void OnDeactivate(object? argument);

}
public static class PlayListExtensions {

    public static PlayListBase PlayList(this StateBase state);
    public static T PlayList<T>(this StateBase state);

    public static CancellationToken GetCancellationToken_OnDetachCallback(this PlayListBase playList);
    public static CancellationToken GetCancellationToken_OnDeactivateCallback(this PlayListBase playList);

}
```
```
// UI
public abstract class ScreenBase : DisposableBase {

    protected TreeMachine<Node2<WidgetBase>, ScreenBase> Machine { get; }

    public ScreenBase();
    public override void Dispose();

}
public abstract class WidgetBase : DisposableBase {

    public Node2<WidgetBase> Node { get; }
    protected ScreenBase? Screen { get; }

    public WidgetBase();
    public override void Dispose();

    protected abstract void OnActivate(object? argument);
    protected abstract void OnDeactivate(object? argument);

    protected virtual void OnBeforeDescendantActivate(NodeBase descendant, object? argument);
    protected virtual void OnAfterDescendantActivate(NodeBase descendant, object? argument);
    protected virtual void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument);
    protected virtual void OnAfterDescendantDeactivate(NodeBase descendant, object? argument);

    protected virtual void Sort(List<NodeBase> children);

}
public abstract class ViewableWidgetBase : WidgetBase {

    public object View { get; protected init; }

    public ViewableWidgetBase();
    public override void Dispose();

}
public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase {

    protected new TView View { get; init; }

    public ViewableWidgetBase();
    public override void Dispose();

}
public static class WidgetExtensions {

    public static WidgetBase Widget(this NodeBase node);
    public static T Widget<T>(this NodeBase node);

    public static CancellationToken GetCancellationToken_OnDetachCallback(this WidgetBase widget);
    public static CancellationToken GetCancellationToken_OnDeactivateCallback(this WidgetBase widget);

}
```
```
// UI
public abstract class RouterBase : DisposableBase {

    public RouterBase();
    public override void Dispose();

}
```
```
// App
public abstract class ApplicationBase : DisposableBase {

    public ApplicationBase();
    public override void Dispose();

}
```
```
// Domain (Business, Game)
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

# Links

- https://github.com/Denis535/DotNet.Libraries/tree/main/GameFramework.Pro
- https://nuget.org/packages/GameFramework.Pro
- https://youtu.be/u7wjaaMr6wQ