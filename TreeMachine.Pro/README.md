# Overview
The library that allows you to easily implement a hierarchical object.

# Reference
```
namespace System.TreeMachine.Pro;
public interface ITreeMachine<T> where T : class, INode<T> {

    protected T? Root { get; set; }

    protected void AddRoot(T root, object? argument);
    protected internal void RemoveRoot(T root, object? argument, Action<T, object?>? callback);
    protected void RemoveRoot(object? argument, Action<T, object?>? callback);

}
public partial interface INode<TThis> where TThis : class, INode<TThis> {

    protected object? Owner { get; }

    public ITreeMachine<TThis>? Machine { get; }
    protected internal ITreeMachine<TThis>? Machine_NoRecursive { get; }

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity Activity { get; }

    public IReadOnlyList<TThis> Children { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

}
public partial interface INode<TThis> {

    public Action<object?>? OnBeforeAttachCallback { get; set; }
    public Action<object?>? OnAfterAttachCallback { get; set; }
    public Action<object?>? OnBeforeDetachCallback { get; set; }
    public Action<object?>? OnAfterDetachCallback { get; set; }

    protected internal void Attach(ITreeMachine<TThis> machine, object? argument);
    protected internal void Attach(TThis parent, object? argument);
    protected internal void Detach(ITreeMachine<TThis> machine, object? argument);
    protected internal void Detach(TThis parent, object? argument);

    protected void OnAttach(object? argument);
    protected void OnBeforeAttach(object? argument);
    protected void OnAfterAttach(object? argument);

    protected void OnDetach(object? argument);
    protected void OnBeforeDetach(object? argument);
    protected void OnAfterDetach(object? argument);

}
public partial interface INode<TThis> {

    public Action<object?>? OnBeforeActivateCallback { get; set; }
    public Action<object?>? OnAfterActivateCallback { get; set; }
    public Action<object?>? OnBeforeDeactivateCallback { get; set; }
    public Action<object?>? OnAfterDeactivateCallback { get; set; }

    protected void Activate(object? argument);
    protected void Deactivate(object? argument);

    protected void OnActivate(object? argument);
    protected void OnBeforeActivate(object? argument);
    protected void OnAfterActivate(object? argument);

    protected void OnDeactivate(object? argument);
    protected void OnBeforeDeactivate(object? argument);
    protected void OnAfterDeactivate(object? argument);

}
public partial interface INode<TThis> {

    protected void AddChild(TThis child, object? argument);
    protected void AddChildren(IEnumerable<TThis> children, object? argument);

    protected void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(object? argument, Action<TThis, object?>? callback);

    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

    protected void Sort(List<TThis> children);

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
