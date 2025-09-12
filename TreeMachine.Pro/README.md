# Overview
The library that allows you to easily implement a hierarchical object.

# Reference

###### System.TreeMachine.Pro

```
namespace System.TreeMachine.Pro;
public abstract class TreeMachineBase {
}
public abstract class TreeMachineBase<TRoot> : TreeMachineBase where TRoot : notnull, NodeBase {

    protected TRoot? Root { get; }

    public TreeMachineBase();

    protected virtual void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback);

}
public enum Activity {
    Inactive,
    Activating,
    Active,
    Deactivating,
}
// NodeBase
public abstract partial class NodeBase {

    public TreeMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public NodeBase Root { get; }

    public NodeBase? Parent { get; }
    public IEnumerable<NodeBase> Ancestors { get; }
    public IEnumerable<NodeBase> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IReadOnlyList<NodeBase> Children { get; }
    public IEnumerable<NodeBase> Descendants { get; }
    public IEnumerable<NodeBase> DescendantsAndSelf { get; }

    public NodeBase();

}
public abstract partial class NodeBase {

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class NodeBase {

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class NodeBase {

    protected virtual void AddChild(NodeBase child, object? argument);
    protected void AddChildren(IEnumerable<NodeBase> children, object? argument);
    protected virtual void RemoveChild(NodeBase child, object? argument, Action<NodeBase, object?>? callback);
    protected bool RemoveChild(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback);
    protected int RemoveChildren(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback);
    protected int RemoveChildren(object? argument, Action<NodeBase, object?>? callback);
    protected void RemoveSelf(object? argument, Action<NodeBase, object?>? callback);

    protected virtual void Sort(List<NodeBase> children);

}
// NodeBase2
public abstract partial class NodeBase2 : NodeBase {

    protected override void OnBeforeAttach(object? argument);
    protected override void OnAfterAttach(object? argument);
    protected override void OnBeforeDetach(object? argument);
    protected override void OnAfterDetach(object? argument);

    protected abstract void OnBeforeDescendantAttach(NodeBase descendant, object? argument);
    protected abstract void OnAfterDescendantAttach(NodeBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDetach(NodeBase descendant, object? argument);
    protected abstract void OnAfterDescendantDetach(NodeBase descendant, object? argument);

}
public abstract partial class NodeBase2 {

    protected override void OnBeforeActivate(object? argument);
    protected override void OnAfterActivate(object? argument);
    protected override void OnBeforeDeactivate(object? argument);
    protected override void OnAfterDeactivate(object? argument);

    protected abstract void OnBeforeDescendantActivate(NodeBase descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(NodeBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(NodeBase descendant, object? argument);

}
```

```
namespace System.TreeMachine.Pro;
public sealed class TreeMachine<TRoot, TUserData> : TreeMachineBase<TRoot> where TRoot : notnull, NodeBase {

    public new TRoot? Root { get; }

    public TUserData UserData { get; }

    public TreeMachine(TUserData userData);

    public new void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback);

}
// Node
public sealed class Node<TUserData> : NodeBase {

    public TUserData UserData { get; }

    public Action<List<NodeBase>>? SortDelegate { get; init; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;
    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public Node(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);
    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

    public new void AddChild(NodeBase child, object? argument);
    public new void AddChildren(IEnumerable<NodeBase> children, object? argument);
    public new void RemoveChild(NodeBase child, object? argument, Action<NodeBase, object?>? callback);
    public new bool RemoveChild(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback);
    public new int RemoveChildren(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback);
    public new int RemoveChildren(object? argument, Action<NodeBase, object?>? callback);
    public new void RemoveSelf(object? argument, Action<NodeBase, object?>? callback);

    protected override void Sort(List<NodeBase> children);

}
// Node2
public sealed class Node2<TUserData> : NodeBase2 {

    public TUserData UserData { get; }

    public Action<List<NodeBase>>? SortDelegate { get; init; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;
    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public event Action<NodeBase, object?>? OnBeforeDescendantAttachCallback;
    public event Action<NodeBase, object?>? OnAfterDescendantAttachCallback;
    public event Action<NodeBase, object?>? OnBeforeDescendantDetachCallback;
    public event Action<NodeBase, object?>? OnAfterDescendantDetachCallback;

    public event Action<NodeBase, object?>? OnBeforeDescendantActivateCallback;
    public event Action<NodeBase, object?>? OnAfterDescendantActivateCallback;
    public event Action<NodeBase, object?>? OnBeforeDescendantDeactivateCallback;
    public event Action<NodeBase, object?>? OnAfterDescendantDeactivateCallback;

    public Node2(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);
    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

    protected override void OnBeforeDescendantAttach(NodeBase descendant, object? argument);
    protected override void OnAfterDescendantAttach(NodeBase descendant, object? argument);
    protected override void OnBeforeDescendantDetach(NodeBase descendant, object? argument);
    protected override void OnAfterDescendantDetach(NodeBase descendant, object? argument);

    protected override void OnBeforeDescendantActivate(NodeBase descendant, object? argument);
    protected override void OnAfterDescendantActivate(NodeBase descendant, object? argument);
    protected override void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument);
    protected override void OnAfterDescendantDeactivate(NodeBase descendant, object? argument);

    public new void AddChild(NodeBase child, object? argument);
    public new void AddChildren(IEnumerable<NodeBase> children, object? argument);
    public new void RemoveChild(NodeBase child, object? argument, Action<NodeBase, object?>? callback);
    public new bool RemoveChild(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback);
    public new int RemoveChildren(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback);
    public new int RemoveChildren(object? argument, Action<NodeBase, object?>? callback);
    public new void RemoveSelf(object? argument, Action<NodeBase, object?>? callback);

    protected override void Sort(List<NodeBase> children);

}
```

# Link

- https://github.com/Denis535/DotNet.Libraries/tree/main/TreeMachine.Pro
- https://nuget.org/packages/TreeMachine.Pro
- https://youtu.be/4LgjHrkxymA