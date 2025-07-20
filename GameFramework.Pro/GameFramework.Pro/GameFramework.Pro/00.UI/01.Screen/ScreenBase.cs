namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {
        private sealed class TreeMachine : TreeMachineBase<WidgetBase.Node_>, IDisposable {

            public new WidgetBase? Root => base.Root?.Widget;

            public TreeMachine() {
            }
            public void Dispose() {
                Assert.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null );
            }

            public void AddRoot(WidgetBase root, object? argument) {
                base.AddRoot( root.Node, argument );
            }
            public void RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback) {
                base.RemoveRoot( root.Node, argument, (root, arg) => callback?.Invoke( root.Widget, arg ) );
            }
            public void RemoveRoot(object? argument, Action<WidgetBase, object?>? callback) {
                base.RemoveRoot( argument, (root, arg) => callback?.Invoke( root.Widget, arg ) );
            }

        }

        private TreeMachine Machine { get; }

        protected WidgetBase? Root => this.Machine.Root;

        public ScreenBase() {
            this.Machine = new TreeMachine();
        }
        public override void Dispose() {
            this.Machine.Dispose();
            base.Dispose();
        }

        protected virtual void AddRoot(WidgetBase root, object? argument) {
            this.Machine.AddRoot( root, argument );
        }
        protected virtual void RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback) {
            this.Machine.RemoveRoot( root, argument, callback );
        }
        protected virtual void RemoveRoot(object? argument, Action<WidgetBase, object?>? callback) {
            this.Machine.RemoveRoot( argument, callback );
        }

    }
}
