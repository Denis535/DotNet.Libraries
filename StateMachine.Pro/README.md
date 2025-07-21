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
public abstract class StateMachineBase<T> where T : notnull, StateBase<T> {

    protected T? State { get; private set; }

    public StateMachineBase();

    protected virtual void SetState(T? state, object? argument, Action<T, object?>? callback);
    protected virtual void AddState(T state, object? argument);
    protected virtual void RemoveState(T state, object? argument, Action<T, object?>? callback);
    protected void RemoveState(object? argument, Action<T, object?>? callback);

}
public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {

    private object? Owner { get; set; }

    public StateMachineBase<TThis>? Machine { get; }
    internal StateMachineBase<TThis>? Machine_NoRecursive { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity Activity { get; private set; }

    public TThis? Child { get; private set; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    public StateBase();

}
public abstract partial class StateBase<TThis> {

    internal void Attach(StateMachineBase<TThis> machine, object? argument);
    private void Attach(TThis parent, object? argument);
    internal void Detach(StateMachineBase<TThis> machine, object? argument);
    private void Detach(TThis parent, object? argument);

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class StateBase<TThis> {

    protected virtual void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback);
    protected virtual void AddChild(TThis child, object? argument);
    protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected void RemoveChild(object? argument, Action<TThis, object?>? callback);
    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

}
public enum Activity {
    Inactive,
    Activating,
    Active,
    Deactivating,
}
```

# Link
- https://github.com/Denis535/DotNet.Libraries/tree/main/StateMachine.Pro
- https://www.nuget.org/packages/StateMachine.Pro
