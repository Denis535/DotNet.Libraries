#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine.Pro;

    public abstract class ScreenBase : DisposableBase {
        private sealed class TreeMachine : TreeMachineBase<WidgetBase.Node_>, IDisposable {

            public new WidgetBase.Node_? Root => base.Root;

            public TreeMachine() {
            }
            public void Dispose() {
                Assert.Operation.Valid( $"TreeMachine {this} must have no {this.Root} root", this.Root == null );
            }

            public new void AddRoot(WidgetBase.Node_ root, object? argument) {
                base.AddRoot( root, argument );
            }
            public new void RemoveRoot(WidgetBase.Node_ root, object? argument, Action<WidgetBase.Node_, object?>? callback) {
                base.RemoveRoot( root, argument, callback );
            }
            public new void RemoveRoot(object? argument, Action<WidgetBase.Node_, object?>? callback) {
                base.RemoveRoot( argument, callback );
            }

        }

        private TreeMachine Machine { get; }

        protected WidgetBase? Root => this.Machine.Root?.Owner;

        public ScreenBase() {
            this.Machine = new TreeMachine();
        }
        public override void Dispose() {
            this.Machine.Dispose();
            base.Dispose();
        }

        protected virtual void AddRoot(WidgetBase root, object? argument) {
            this.Machine.AddRoot( root.Node, argument );
        }
        protected virtual void RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback) {
            this.Machine.RemoveRoot( root.Node, argument, (root, arg) => callback?.Invoke( root.Owner, arg ) );
        }
        protected virtual void RemoveRoot(object? argument, Action<WidgetBase, object?>? callback) {
            this.Machine.RemoveRoot( argument, (root, arg) => callback?.Invoke( root.Owner, arg ) );
        }

    }
}
