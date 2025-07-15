# Overview

The framework that allows you to design high-quality architecture of your game project.

# Reference

```
namespace System;
public abstract class DisposableBase : IDisposable {

    public bool IsDisposed { get; }
    public CancellationToken DisposeCancellationToken { get; }

    public DisposableBase();
    public virtual void Dispose();

}
public static class DisposableExtensions {

    public static void DisposeAll(this IEnumerable<IDisposable> disposables);

}
```

```
namespace GameFramework.Pro;
public abstract class ProgramBase : DisposableBase {

    public ProgramBase();
    public override void Dispose();

}

// UI
public abstract class ThemeBase : DisposableBase, IStateMachine<PlayListBase> {

    protected PlayListBase? State { get; }

    public ThemeBase();
    public override void Dispose();

    protected void SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback);
    protected void AddState(PlayListBase state, object? argument);
    protected void RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback);
    protected void RemoveState(object? argument, Action<PlayListBase, object?>? callback);

}
public abstract class PlayListBase : StateBase<PlayListBase>, IDisposable {

    public bool IsDisposed { get; }
    public CancellationToken DisposeCancellationToken { get; }

    public PlayListBase();
    public virtual void Dispose();

}

public abstract class ScreenBase : DisposableBase, ITreeMachine<WidgetBase> {

    protected WidgetBase? Root { get; }

    public ScreenBase();
    public override void Dispose();

    public void AddRoot(WidgetBase root, object? argument);
    public void RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback);
    public void RemoveRoot(object? argument, Action<WidgetBase, object?>? callback);

}
public abstract class WidgetBase : NodeBase2<WidgetBase>, IDisposable {

    public bool IsDisposed { get; }
    public CancellationToken DisposeCancellationToken { get; }

    public ScreenBase? Screen { get; }

    public WidgetBase();
    public virtual void Dispose();

}
public abstract class ViewableWidgetBase : WidgetBase {

    protected ViewBase View { get; init; }

    public ViewableWidgetBase();
    public override void Dispose();

}
public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : ViewBase {

    protected new TView View { get; init; }

    public ViewableWidgetBase();
    public override void Dispose();

}
public abstract class ViewBase : DisposableBase {

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

// Domain (business)
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
