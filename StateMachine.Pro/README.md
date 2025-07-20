# Overview
The library that allows you to easily implement a stateful object.

# Reference
```
namespace System.StateMachine.Pro;
public abstract class StateMachineBase<T> where T : notnull, StateBase<T> {

    protected T? State { get; private set; }

    public StateMachineBase();

    protected virtual void SetState(T? state, object? argument, Action<T, object?>? callback);
    protected virtual void AddState(T state, object? argument);
    protected virtual void RemoveState(T state, object? argument, Action<T, object?>? callback);
    protected void RemoveState(object? argument, Action<T, object?>? callback);

}
public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {

    private StateMachineBase<TThis>? Owner { get; set; }

    public StateMachineBase<TThis>? Machine { get; }

    public Activity Activity { get; private set; }

    public StateBase();

}
public abstract partial class StateBase<TThis> {

    public event Action<object?>? OnBeforeAttachCallback;
    public event Action<object?>? OnAfterAttachCallback;
    public event Action<object?>? OnBeforeDetachCallback;
    public event Action<object?>? OnAfterDetachCallback;

    internal void Attach(StateMachineBase<TThis> machine, object? argument);
    internal void Detach(StateMachineBase<TThis> machine, object? argument);

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {

    public event Action<object?>? OnBeforeActivateCallback;
    public event Action<object?>? OnAfterActivateCallback;
    public event Action<object?>? OnBeforeDeactivateCallback;
    public event Action<object?>? OnAfterDeactivateCallback;

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public enum Activity {
    Inactive,
    Activating,
    Active,
    Deactivating,
}
```
```
namespace System.StateMachine.Pro.Hierarchical;
```

# Link
- https://github.com/Denis535/DotNet.Libraries/tree/main/StateMachine.Pro
- https://www.nuget.org/packages/StateMachine.Pro
