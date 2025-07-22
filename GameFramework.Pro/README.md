# Overview

The framework that allows you to design high-quality architecture of your game project.

# Reference

###### System

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

###### GameFramework.Pro

```
namespace GameFramework.Pro;
public abstract class ProgramBase : DisposableBase {

    public ProgramBase();
    public override void Dispose();

}
// UI
public abstract class ThemeBase : DisposableBase {

    protected PlayListBase? State { get; }

    public ThemeBase();
    public override void Dispose();

    protected virtual void SetState(PlayListBase? state, object? argument, Action<PlayListBase, object?>? callback);
    protected virtual void AddState(PlayListBase state, object? argument);
    protected virtual void RemoveState(PlayListBase state, object? argument, Action<PlayListBase, object?>? callback);
    protected virtual void RemoveState(object? argument, Action<PlayListBase, object?>? callback);

}
public abstract class ScreenBase : DisposableBase {

    protected WidgetBase? Root { get; }

    public ScreenBase();
    public override void Dispose();

    protected virtual void AddRoot(WidgetBase root, object? argument);
    protected virtual void RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback);
    protected virtual void RemoveRoot(object? argument, Action<WidgetBase, object?>? callback);

}
public abstract class PlayListBase : DisposableBase {

    public Activity Activity { get; }

    public PlayListBase();
    public override void Dispose();

    protected virtual void OnAttach(object? argument);
    protected virtual void OnDetach(object? argument);

    protected virtual void OnActivate(object? argument);
    protected virtual void OnDeactivate(object? argument);

}
public abstract class WidgetBase : DisposableBase {

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public WidgetBase Root { get; }

    public WidgetBase? Parent { get; }
    public IEnumerable<WidgetBase> Ancestors { get; }
    public IEnumerable<WidgetBase> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IEnumerable<WidgetBase> Children { get; }
    public IEnumerable<WidgetBase> Descendants { get; }
    public IEnumerable<WidgetBase> DescendantsAndSelf { get; }

    protected IComparer<WidgetBase>? Comparer { get; init; }

    public WidgetBase();
    public override void Dispose();

    protected virtual void OnAttach(object? argument);
    protected virtual void OnDetach(object? argument);

    protected virtual void OnActivate(object? argument);
    protected virtual void OnDeactivate(object? argument);

    protected virtual void OnBeforeDescendantAttach(WidgetBase descendant, object? argument);
    protected virtual void OnAfterDescendantAttach(WidgetBase descendant, object? argument);
    protected virtual void OnBeforeDescendantDetach(WidgetBase descendant, object? argument);
    protected virtual void OnAfterDescendantDetach(WidgetBase descendant, object? argument);

    protected virtual void OnBeforeDescendantActivate(WidgetBase descendant, object? argument);
    protected virtual void OnAfterDescendantActivate(WidgetBase descendant, object? argument);
    protected virtual void OnBeforeDescendantDeactivate(WidgetBase descendant, object? argument);
    protected virtual void OnAfterDescendantDeactivate(WidgetBase descendant, object? argument);

    protected virtual void AddChild(WidgetBase child, object? argument);
    protected virtual void AddChildren(IEnumerable<WidgetBase> children, object? argument);
    protected virtual void RemoveChild(WidgetBase child, object? argument, Action<WidgetBase, object?>? callback);
    protected virtual bool RemoveChild(Func<WidgetBase, bool> predicate, object? argument, Action<WidgetBase, object?>? callback);
    protected virtual int RemoveChildren(Func<WidgetBase, bool> predicate, object? argument, Action<WidgetBase, object?>? callback);
    protected virtual int RemoveChildren(object? argument, Action<WidgetBase, object?>? callback);
    protected virtual void RemoveSelf(object? argument, Action<WidgetBase, object?>? callback);

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
- https://www.nuget.org/packages/GameFramework.Pro
- https://youtu.be/u7wjaaMr6wQ