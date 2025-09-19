# Overview
The library that allows you to easily implement a hierarchical object.

# Reference

###### System.TreeMachine.Pro

```
namespace System.TreeMachine.Pro;
public abstract class TreeMachineBase {

    protected INode? Root { get; }

    public TreeMachineBase();

    protected virtual void SetRoot(INode? root, object? argument, Action<INode, object?>? callback);

}
public class TreeMachine : TreeMachineBase {

    public new INode? Root { get; }

    public TreeMachine();

    public new void SetRoot(INode? root, object? argument, Action<INode, object?>? callback);

}
public class TreeMachine<TUserData> : TreeMachine {

    public TUserData UserData { get; }

    public TreeMachine(TUserData userData);

}
public interface INode {

    public TreeMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public INode Root { get; }

    public INode? Parent { get; }
    public IEnumerable<INode> Ancestors { get; }
    public IEnumerable<INode> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IEnumerable<INode> Children { get; }
    public IEnumerable<INode> Descendants { get; }
    public IEnumerable<INode> DescendantsAndSelf { get; }

}
public enum Activity {
    Inactive,
    Activating,
    Active,
    Deactivating,
}
```

# Link

- https://github.com/Denis535/DotNet.Libraries/tree/main/TreeMachine.Pro
- https://nuget.org/packages/TreeMachine.Pro
- https://youtu.be/4LgjHrkxymA