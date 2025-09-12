# Overview
The library that allows you to easily implement a stateful object.

# Reference

###### System.StateMachine.Pro

```
namespace System.StateMachine.Pro;
public abstract class StateMachineBase {
}
public abstract class StateMachineBase<TRoot> : StateMachineBase where TRoot : class, IState  {

    protected TRoot? Root { get; }

    public StateMachineBase();

    protected virtual void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback);

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
// StateBase
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
// ChildableStateBase
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

}
// ChildrenableStateBase
public abstract partial class ChildrenableStateBase : IState {

    public StateMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public IState Root { get; }

    public IState? Parent { get; }
    public IEnumerable<IState> Ancestors { get; }
    public IEnumerable<IState> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IReadOnlyList<IState> Children { get; }
    public IEnumerable<IState> Descendants { get; }
    public IEnumerable<IState> DescendantsAndSelf { get; }

    public ChildrenableStateBase();

}
public abstract partial class ChildrenableStateBase {

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class ChildrenableStateBase {

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class ChildrenableStateBase {

    protected virtual void AddChild(IState child, object? argument);
    protected void AddChildren(IEnumerable<IState> children, object? argument);
    protected virtual void RemoveChild(IState child, object? argument, Action<IState, object?>? callback);
    protected bool RemoveChild(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback);
    protected int RemoveChildren(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback);
    protected int RemoveChildren(object? argument, Action<IState, object?>? callback);

    protected virtual void Sort(List<IState> children);

}
```

```
namespace System.StateMachine.Pro;
public sealed class StateMachine<TRoot, TUserData> : StateMachineBase<TRoot> where TRoot : class, IState {

    public new TRoot? Root { get; }

    public TUserData UserData { get; }

    public StateMachine(TUserData userData);

    public new void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback);

}
// State
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
// ChildableState
public sealed class ChildableState<TUserData> : ChildableStateBase {

    public TUserData UserData { get; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;

    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public ChildableState(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);

    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

    public new void SetChild(IState? child, object? argument, Action<IState, object?>? callback);

}
// ChildrenableState
public sealed class ChildrenableState<TUserData> : ChildrenableStateBase {

    public TUserData UserData { get; }

    public Action<List<IState>>? SortDelegate { get; init; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;

    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public ChildrenableState(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);

    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

    public new void AddChild(IState child, object? argument);
    public new void AddChildren(IEnumerable<IState> children, object? argument);
    public new void RemoveChild(IState child, object? argument, Action<IState, object?>? callback);
    public new bool RemoveChild(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback);
    public new int RemoveChildren(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback);
    public new int RemoveChildren(object? argument, Action<IState, object?>? callback);

    protected override void Sort(List<IState> children);

}
```

# Link

- https://github.com/Denis535/DotNet.Libraries/tree/main/StateMachine.Pro
- https://nuget.org/packages/StateMachine.Pro
- https://youtu.be/1T3O5YpGdAY