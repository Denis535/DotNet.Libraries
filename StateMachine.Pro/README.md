# Overview
The library that allows you to easily implement a stateful object.

# Reference
```
namespace System.StateMachine.Pro;
public interface IStateMachine<T> where T : class, IState<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T, object?>? callback);
    protected void AddState(T state, object? argument);
    protected void RemoveState(T state, object? argument, Action<T, object?>? callback);
    protected void RemoveState(object? argument, Action<T, object?>? callback);

}
public partial interface IState<TThis> where TThis : class, IState<TThis> {

    protected IStateMachine<TThis>? Owner { get; }

    public IStateMachine<TThis>? Machine { get; }

    public Activity Activity { get; }

}
public partial interface IState<TThis> {

    public event Action<object?>? OnBeforeAttachCallback;
    public event Action<object?>? OnAfterAttachCallback;
    public event Action<object?>? OnBeforeDetachCallback;
    public event Action<object?>? OnAfterDetachCallback;

    protected internal void Attach(IStateMachine<TThis> machine, object? argument);
    protected internal void Detach(IStateMachine<TThis> machine, object? argument);

    protected void OnAttach(object? argument);
    protected void OnBeforeAttach(object? argument);
    protected void OnAfterAttach(object? argument);

    protected void OnDetach(object? argument);
    protected void OnBeforeDetach(object? argument);
    protected void OnAfterDetach(object? argument);

}
public partial interface IState<TThis> {

    public event Action<object?>? OnBeforeActivateCallback;
    public event Action<object?>? OnAfterActivateCallback;
    public event Action<object?>? OnBeforeDeactivateCallback;
    public event Action<object?>? OnAfterDeactivateCallback;

    protected void Activate(object? argument);
    protected void Deactivate(object? argument);

    protected void OnActivate(object? argument);
    protected void OnBeforeActivate(object? argument);
    protected void OnAfterActivate(object? argument);

    protected void OnDeactivate(object? argument);
    protected void OnBeforeDeactivate(object? argument);
    protected void OnAfterDeactivate(object? argument);

}
```
```
namespace System.StateMachine.Pro.Hierarchical;
public interface IStateMachine<T> where T : class, IState<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T, object?>? callback);
    protected void AddState(T state, object? argument);
    protected internal void RemoveState(T state, object? argument, Action<T, object?>? callback);
    protected void RemoveState(object? argument, Action<T, object?>? callback);

}
public partial interface IState<TThis> where TThis : class, IState<TThis> {

    protected object? Owner { get; }

    public IStateMachine<TThis>? Machine { get; }
    protected internal IStateMachine<TThis>? Machine_NoRecursive { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public TThis? Child { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

}
public partial interface IState<TThis> {

    public event Action<object?>? OnBeforeAttachCallback;
    public event Action<object?>? OnAfterAttachCallback;
    public event Action<object?>? OnBeforeDetachCallback;
    public event Action<object?>? OnAfterDetachCallback;

    protected internal void Attach(IStateMachine<TThis> machine, object? argument);
    protected void Attach(TThis parent, object? argument);
    protected internal void Detach(IStateMachine<TThis> machine, object? argument);
    protected void Detach(TThis parent, object? argument);

    protected void OnAttach(object? argument);
    protected void OnBeforeAttach(object? argument);
    protected void OnAfterAttach(object? argument);

    protected void OnDetach(object? argument);
    protected void OnBeforeDetach(object? argument);
    protected void OnAfterDetach(object? argument);

}
public partial interface IState<TThis> {

    public event Action<object?>? OnBeforeActivateCallback;
    public event Action<object?>? OnAfterActivateCallback;
    public event Action<object?>? OnBeforeDeactivateCallback;
    public event Action<object?>? OnAfterDeactivateCallback;

    protected void Activate(object? argument);
    protected void Deactivate(object? argument);

    protected void OnActivate(object? argument);
    protected void OnBeforeActivate(object? argument);
    protected void OnAfterActivate(object? argument);

    protected void OnDeactivate(object? argument);
    protected void OnBeforeDeactivate(object? argument);
    protected void OnAfterDeactivate(object? argument);

}
public partial interface IState<TThis> {

    protected void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback);
    protected void AddChild(TThis child, object? argument);
    protected void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected void RemoveChild(object? argument, Action<TThis, object?>? callback);
    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

}
```

# Link
- https://github.com/Denis535/DotNet.Libraries/tree/main/StateMachine.Pro
- https://www.nuget.org/packages/StateMachine.Pro
