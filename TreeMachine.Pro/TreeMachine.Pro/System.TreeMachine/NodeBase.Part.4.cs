﻿#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> {

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine", child.Machine_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            Assert.Operation.Valid( $"Node {this} must have no {child} child", !this.Children.Contains( child ) );
            this.children.Add( child );
            this.Sort( this.children );
            child.Attach( (TThis) this, argument );
        }
        protected void AddChildren(TThis[] children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity_.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity_.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            }
            Assert.Operation.Valid( $"Node {this} must have {child} child", this.Children.Contains( child ) );
            child.Detach( (TThis) this, argument );
            this.children.Remove( child );
            callback?.Invoke( child, argument );
        }
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected int RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            if (this.Parent != null) {
                this.Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Assert.Operation.Valid( $"Node {this} must have machine", this.Machine_NoRecursive != null );
                this.Machine_NoRecursive.RemoveRoot( (TThis) this, argument, callback );
            }
        }

        // Sort
        protected virtual void Sort(List<TThis> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}
