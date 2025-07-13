# Overview
The library that allows you to easily implement a hierarchical object.

# Reference
```
namespace System.TreeMachine.Pro;
public interface ITreeMachine<T> where T : notnull, NodeBase<T> {

    protected T? Root { get; set; }

    protected void AddRoot(T root, object? argument);
    protected void RemoveRoot(T root, object? argument, Action<T, object?>? callback);
    protected void RemoveRoot(object? argument, Action<T, object?>? callback);

}
public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public ITreeMachine<TThis>? Machine { get; }

    public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity_ Activity { get; }

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
    
    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class NodeBase<TThis> {

    protected virtual void AddChild(TThis child, object? argument);
    protected void AddChildren(TThis[] children, object? argument);
    protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(object? argument, Action<TThis, object?>? callback);
    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

    protected virtual void Sort(List<TThis> children);

}
public abstract partial class NodeBase2<TThis> : NodeBase<TThis> where TThis : notnull, NodeBase2<TThis> {

    public event Action<TThis, object?>? OnBeforeDescendantAttachCallback;
    public event Action<TThis, object?>? OnAfterDescendantAttachCallback;
    public event Action<TThis, object?>? OnBeforeDescendantDetachCallback;
    public event Action<TThis, object?>? OnAfterDescendantDetachCallback;

    public NodeBase2() {
    }

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

    public event Action<TThis, object?>? OnBeforeDescendantActivateCallback;
    public event Action<TThis, object?>? OnAfterDescendantActivateCallback;
    public event Action<TThis, object?>? OnBeforeDescendantDeactivateCallback;
    public event Action<TThis, object?>? OnAfterDescendantDeactivateCallback;

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

# Link
- https://github.com/Denis535/DotNet.Libraries/tree/main/TreeMachine.Pro
- https://www.nuget.org/packages/TreeMachine.Pro
