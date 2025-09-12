# Overview
The library that allows you to easily implement a hierarchical object.

# Reference

###### System.TreeMachine.Pro

```
namespace System.TreeMachine.Pro;
public abstract class TreeMachineBase {
}
public abstract class TreeMachineBase<TNode> : TreeMachineBase where TNode : notnull, NodeBase<TNode> {

    protected TNode? Root { get; }

    public TreeMachineBase();

    protected virtual void AddRoot(TNode root, object? argument);
    protected virtual void RemoveRoot(TNode root, object? argument, Action<TNode, object?>? callback);
    protected void RemoveRoot(object? argument, Action<TNode, object?>? callback);

}
public enum Activity {
    Inactive,
    Activating,
    Active,
    Deactivating,
}
// NodeBase
public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

    public TreeMachineBase? Machine { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IReadOnlyList<TThis> Children { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    public NodeBase();

}
public abstract partial class NodeBase<TThis> {

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class NodeBase<TThis> {

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class NodeBase<TThis> {

    protected virtual void AddChild(TThis child, object? argument);
    protected void AddChildren(IEnumerable<TThis> children, object? argument);
    protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(object? argument, Action<TThis, object?>? callback);
    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

    protected virtual void Sort(List<TThis> children);

}
// NodeBase2
public abstract partial class NodeBase2<TThis> : NodeBase<TThis> where TThis : notnull, NodeBase2<TThis> {

    protected override void OnBeforeAttach(object? argument);
    protected override void OnAfterAttach(object? argument);
    protected override void OnBeforeDetach(object? argument);
    protected override void OnAfterDetach(object? argument);

    protected abstract void OnBeforeDescendantAttach(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantAttach(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDetach(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDetach(TThis descendant, object? argument);

}
public abstract partial class NodeBase2<TThis> {

    protected override void OnBeforeActivate(object? argument);
    protected override void OnAfterActivate(object? argument);
    protected override void OnBeforeDeactivate(object? argument);
    protected override void OnAfterDeactivate(object? argument);

    protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

}
```

```
namespace System.TreeMachine.Pro;
public sealed class TreeMachine<TNode, TUserData> : TreeMachineBase<TNode> where TNode : notnull, NodeBase<TNode> {

    public new TNode? Root { get; }

    public TUserData UserData { get; }

    public TreeMachine(TUserData userData);

    public new void AddRoot(TNode root, object? argument);
    public new void RemoveRoot(TNode root, object? argument, Action<TNode, object?>? callback);
    public new void RemoveRoot(object? argument, Action<TNode, object?>? callback);

}
// Node
public sealed class Node<TUserData> : NodeBase<Node<TUserData>> {

    public TUserData UserData { get; }

    public Action<List<Node<TUserData>>>? SortDelegate { get; init; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;
    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public Node(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);
    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

    public new void AddChild(Node<TUserData> child, object? argument);
    public new void AddChildren(IEnumerable<Node<TUserData>> children, object? argument);
    public new void RemoveChild(Node<TUserData> child, object? argument, Action<Node<TUserData>, object?>? callback);
    public new bool RemoveChild(Func<Node<TUserData>, bool> predicate, object? argument, Action<Node<TUserData>, object?>? callback);
    public new int RemoveChildren(Func<Node<TUserData>, bool> predicate, object? argument, Action<Node<TUserData>, object?>? callback);
    public new int RemoveChildren(object? argument, Action<Node<TUserData>, object?>? callback);
    public new void RemoveSelf(object? argument, Action<Node<TUserData>, object?>? callback);

    protected override void Sort(List<Node<TUserData>> children);

}
// Node2
public sealed class Node2<TUserData> : NodeBase2<Node2<TUserData>> {

    public TUserData UserData { get; }

    public Action<List<Node2<TUserData>>>? SortDelegate { get; init; }

    public event Action<object?>? OnAttachCallback;
    public event Action<object?>? OnDetachCallback;
    public event Action<object?>? OnActivateCallback;
    public event Action<object?>? OnDeactivateCallback;

    public event Action<Node2<TUserData>, object?>? OnBeforeDescendantAttachCallback;
    public event Action<Node2<TUserData>, object?>? OnAfterDescendantAttachCallback;
    public event Action<Node2<TUserData>, object?>? OnBeforeDescendantDetachCallback;
    public event Action<Node2<TUserData>, object?>? OnAfterDescendantDetachCallback;

    public event Action<Node2<TUserData>, object?>? OnBeforeDescendantActivateCallback;
    public event Action<Node2<TUserData>, object?>? OnAfterDescendantActivateCallback;
    public event Action<Node2<TUserData>, object?>? OnBeforeDescendantDeactivateCallback;
    public event Action<Node2<TUserData>, object?>? OnAfterDescendantDeactivateCallback;

    public Node2(TUserData userData);

    protected override void OnAttach(object? argument);
    protected override void OnDetach(object? argument);
    protected override void OnActivate(object? argument);
    protected override void OnDeactivate(object? argument);

    protected override void OnBeforeDescendantAttach(Node2<TUserData> descendant, object? argument);
    protected override void OnAfterDescendantAttach(Node2<TUserData> descendant, object? argument);
    protected override void OnBeforeDescendantDetach(Node2<TUserData> descendant, object? argument);
    protected override void OnAfterDescendantDetach(Node2<TUserData> descendant, object? argument);

    protected override void OnBeforeDescendantActivate(Node2<TUserData> descendant, object? argument);
    protected override void OnAfterDescendantActivate(Node2<TUserData> descendant, object? argument);
    protected override void OnBeforeDescendantDeactivate(Node2<TUserData> descendant, object? argument);
    protected override void OnAfterDescendantDeactivate(Node2<TUserData> descendant, object? argument);

    public new void AddChild(Node2<TUserData> child, object? argument);
    public new void AddChildren(IEnumerable<Node2<TUserData>> children, object? argument);
    public new void RemoveChild(Node2<TUserData> child, object? argument, Action<Node2<TUserData>, object?>? callback);
    public new bool RemoveChild(Func<Node2<TUserData>, bool> predicate, object? argument, Action<Node2<TUserData>, object?>? callback);
    public new int RemoveChildren(Func<Node2<TUserData>, bool> predicate, object? argument, Action<Node2<TUserData>, object?>? callback);
    public new int RemoveChildren(object? argument, Action<Node2<TUserData>, object?>? callback);
    public new void RemoveSelf(object? argument, Action<Node2<TUserData>, object?>? callback);

    protected override void Sort(List<Node2<TUserData>> children);

}
```

# Link

- https://github.com/Denis535/DotNet.Libraries/tree/main/TreeMachine.Pro
- https://nuget.org/packages/TreeMachine.Pro
- https://youtu.be/4LgjHrkxymA