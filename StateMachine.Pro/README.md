# Overview
The library that allows you to easily implement a stateful object.

# Reference

###### System.StateMachine.Pro

```
namespace System.StateMachine.Pro;
public abstract class StateMachineBase {

    protected IState? State { get; }

    public StateMachineBase();

    protected virtual void SetState(IState? state, object? argument, Action<IState, object?>? callback);
    protected virtual void AddState(IState state, object? argument);
    protected virtual void RemoveState(IState state, object? argument, Action<IState, object?>? callback);
    protected void RemoveState(object? argument, Action<IState, object?>? callback);

}
public interface IState {

    public StateMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public IState Root { get; }

    public IState? Parent { get; }
    public IEnumerable<IState> Ancestors { get; }
    public IEnumerable<IState> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IEnumerable<IState> Children { get; }
    public IEnumerable<IState> Descendants { get; }
    public IEnumerable<IState> DescendantsAndSelf { get; }

}
public enum Activity {
    Inactive,
    Activating,
    Active,
    Deactivating,
}
public abstract partial class StateBase : IState {

    public StateMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public IState Root { get; }

    public IState? Parent { get; }
    public IEnumerable<IState> Ancestors { get; }
    public IEnumerable<IState> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public StateBase();

}
public abstract partial class StateBase {

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase {

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class ChildableStateBase : IState {

    public StateMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public IState Root { get; }

    public IState? Parent { get; }
    public IEnumerable<IState> Ancestors { get; }
    public IEnumerable<IState> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IState? Child { get; }
    public IEnumerable<IState> Descendants { get; }
    public IEnumerable<IState> DescendantsAndSelf { get; }

    public ChildableStateBase();

}
public abstract partial class ChildableStateBase {

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class ChildableStateBase {

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class ChildableStateBase {

    protected virtual void SetChild(IState? child, object? argument, Action<IState, object?>? callback);
    protected virtual void AddChild(IState child, object? argument);
    protected virtual void RemoveChild(IState child, object? argument, Action<IState, object?>? callback);
    protected void RemoveChild(object? argument, Action<IState, object?>? callback);

}
```

```
namespace System.StateMachine.Pro;
public sealed class StateMachine<TUserData> : StateMachineBase {

    public new IState? State { get; }

    public TUserData UserData { get; }

    public StateMachine(TUserData userData);

    public new void SetState(IState? state, object? argument, Action<IState, object?>? callback);
    public new void AddState(IState state, object? argument);
    public new void RemoveState(IState state, object? argument, Action<IState, object?>? callback);
    public new void RemoveState(object? argument, Action<IState, object?>? callback);

}
public sealed class State<TUserData> : StateBase {

    public TUserData UserData { get; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;

    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public State(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);

    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

}
public sealed class ChildableState<TUserData> : ChildableStateBase {

    public TUserData UserData { get; private set; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;

    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public ChildableState(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);

    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

}
```

# Link

- https://github.com/Denis535/DotNet.Libraries/tree/main/StateMachine.Pro
- https://nuget.org/packages/StateMachine.Pro
- https://youtu.be/1T3O5YpGdAY