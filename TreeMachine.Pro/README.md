# Overview
The library that allows you to easily implement a hierarchical object.

# Reference
```
namespace System.TreeMachine.Pro;
public abstract class TreeMachineBase<T> where T : notnull, NodeBase<T> {

    protected T? Root { get; private set; }

    public TreeMachineBase();

    protected virtual void AddRoot(T root, object? argument);
    protected internal virtual void RemoveRoot(T root, object? argument, Action<T, object?>? callback);
    protected void RemoveRoot(object? argument, Action<T, object?>? callback);

}
public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

    private object? Owner { get; set; }

    public TreeMachineBase<TThis>? Machine { get; }
    internal TreeMachineBase<TThis>? Machine_NoRecursive { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity Activity { get; private set; }

    public IReadOnlyList<TThis> Children { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    public NodeBase();

}
public abstract partial class NodeBase<TThis> {

    public event Action<object?>? OnBeforeAttachCallback;
    public event Action<object?>? OnAfterAttachCallback;
    public event Action<object?>? OnBeforeDetachCallback;
    public event Action<object?>? OnAfterDetachCallback;

    internal void Attach(TreeMachineBase<TThis> machine, object? argument);
    private void Attach(TThis parent, object? argument);
    internal void Detach(TreeMachineBase<TThis> machine, object? argument);
    private void Detach(TThis parent, object? argument);

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class NodeBase<TThis> {

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
public partial interface INode2<TThis> where TThis : notnull, NodeBase<TThis> {

    public Action<TThis, object?>? OnBeforeDescendantAttachCallback { get; set; }
    public Action<TThis, object?>? OnAfterDescendantAttachCallback { get; set; }
    public Action<TThis, object?>? OnBeforeDescendantDetachCallback { get; set; }
    public Action<TThis, object?>? OnAfterDescendantDetachCallback { get; set; }

    protected internal void OnBeforeDescendantAttach(TThis descendant, object? argument);
    protected internal void OnAfterDescendantAttach(TThis descendant, object? argument);
    protected internal void OnBeforeDescendantDetach(TThis descendant, object? argument);
    protected internal void OnAfterDescendantDetach(TThis descendant, object? argument);

}
public partial interface INode2<TThis> {

    public Action<TThis, object?>? OnBeforeDescendantActivateCallback { get; set; }
    public Action<TThis, object?>? OnAfterDescendantActivateCallback { get; set; }
    public Action<TThis, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
    public Action<TThis, object?>? OnAfterDescendantDeactivateCallback { get; set; }

    protected internal void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected internal void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected internal void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected internal void OnAfterDescendantDeactivate(TThis descendant, object? argument);

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
- https://www.nuget.org/packages/TreeMachine.Pro
