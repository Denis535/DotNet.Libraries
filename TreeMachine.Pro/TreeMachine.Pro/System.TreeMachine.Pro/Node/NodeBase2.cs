#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public abstract partial class NodeBase2<TThis> : NodeBase<TThis> where TThis : notnull, NodeBase2<TThis> {

        // OnAttach
        protected override void OnBeforeAttach(object? argument) {
            foreach (var ancestor in this.Ancestors.Reverse()) {
                ancestor.OnBeforeDescendantAttach( (TThis) this, argument );
            }
            base.OnBeforeAttach( argument );
        }
        protected override void OnAfterAttach(object? argument) {
            base.OnAfterAttach( argument );
            foreach (var ancestor in this.Ancestors) {
                ancestor.OnAfterDescendantAttach( (TThis) this, argument );
            }
        }

        // OnDetach
        protected override void OnBeforeDetach(object? argument) {
            foreach (var ancestor in this.Ancestors.Reverse()) {
                ancestor.OnBeforeDescendantDetach( (TThis) this, argument );
            }
            base.OnBeforeDetach( argument );
        }
        protected override void OnAfterDetach(object? argument) {
            base.OnAfterDetach( argument );
            foreach (var ancestor in this.Ancestors) {
                ancestor.OnAfterDescendantDetach( (TThis) this, argument );
            }
        }

        // OnDescendantAttach
        protected abstract void OnBeforeDescendantAttach(TThis descendant, object? argument);
        protected abstract void OnAfterDescendantAttach(TThis descendant, object? argument);
        protected abstract void OnBeforeDescendantDetach(TThis descendant, object? argument);
        protected abstract void OnAfterDescendantDetach(TThis descendant, object? argument);

    }
    public abstract partial class NodeBase2<TThis> {

        // OnActivate
        protected override void OnBeforeActivate(object? argument) {
            foreach (var ancestor in this.Ancestors.Reverse()) {
                ancestor.OnBeforeDescendantActivate( (TThis) this, argument );
            }
            base.OnBeforeActivate( argument );
        }
        protected override void OnAfterActivate(object? argument) {
            base.OnAfterActivate( argument );
            foreach (var ancestor in this.Ancestors) {
                ancestor.OnAfterDescendantActivate( (TThis) this, argument );
            }
        }

        // OnDeactivate
        protected override void OnBeforeDeactivate(object? argument) {
            foreach (var ancestor in this.Ancestors.Reverse()) {
                ancestor.OnBeforeDescendantDeactivate( (TThis) this, argument );
            }
            base.OnBeforeDeactivate( argument );
        }
        protected override void OnAfterDeactivate(object? argument) {
            base.OnAfterDeactivate( argument );
            foreach (var ancestor in this.Ancestors) {
                ancestor.OnAfterDescendantDeactivate( (TThis) this, argument );
            }
        }

        // OnDescendantActivate
        protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
        protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
        protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
        protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

    }
}
