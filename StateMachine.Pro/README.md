# Overview
The library that allows you to easily implement a stateful object.

# Reference

###### System.StateMachine.Pro

```
namespace System.StateMachine.Pro;
public abstract class StateMachineBase {

    protected IState? Root { get; }

    public StateMachineBase();

    protected virtual void SetRoot(IState? root, object? argument, Action<IState, object?>? callback);

}
public class StateMachine : StateMachineBase {

    public new IState? Root { get; }

    public StateMachine();

    public new void SetRoot(IState? root, object? argument, Action<IState, object?>? callback);

}
public class StateMachine<TUserData> : StateMachine {

    public TUserData UserData { get; }

    public StateMachine(TUserData userData);

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
```

# Link

- https://github.com/Denis535/DotNet.Libraries/tree/main/StateMachine.Pro
- https://nuget.org/packages/StateMachine.Pro
- https://youtu.be/1T3O5YpGdAY