#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial interface INode2<TThis> where TThis : class, INode<TThis> {

        // OnDescendantAttach
        public Action<TThis, object?>? OnBeforeDescendantAttachCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantAttachCallback { get; set; }
        public Action<TThis, object?>? OnBeforeDescendantDetachCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantDetachCallback { get; set; }

        // OnDescendantAttach
        protected internal void OnBeforeDescendantAttach(TThis descendant, object? argument);
        protected internal void OnAfterDescendantAttach(TThis descendant, object? argument);
        protected internal void OnBeforeDescendantDetach(TThis descendant, object? argument);
        protected internal void OnAfterDescendantDetach(TThis descendant, object? argument);

    }
    public partial interface INode2<TThis> {

        // OnDescendantActivate
        public Action<TThis, object?>? OnBeforeDescendantActivateCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantActivateCallback { get; set; }
        public Action<TThis, object?>? OnBeforeDescendantDeactivateCallback { get; set; }
        public Action<TThis, object?>? OnAfterDescendantDeactivateCallback { get; set; }

        // OnDescendantActivate
        protected internal void OnBeforeDescendantActivate(TThis descendant, object? argument);
        protected internal void OnAfterDescendantActivate(TThis descendant, object? argument);
        protected internal void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
        protected internal void OnAfterDescendantDeactivate(TThis descendant, object? argument);

    }
}
