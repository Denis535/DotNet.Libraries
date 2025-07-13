namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.TreeMachine;

    public abstract class ScreenBase : DisposableBase, ITreeMachine<WidgetBase> {

        WidgetBase? ITreeMachine<WidgetBase>.Root { get => this.Root; set => this.Root = value; }

        protected WidgetBase? Root { get; private set; }

        public ScreenBase() {
        }
        public override void Dispose() {
            Assert.Operation.Valid( $"Screen {this} must have no {this.Root} root", this.Root == null );
            base.Dispose();
        }

        void ITreeMachine<WidgetBase>.AddRoot(WidgetBase root, object? argument) {
            this.AddRoot( root, argument );
        }
        void ITreeMachine<WidgetBase>.RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback) {
            this.RemoveRoot( root, argument, callback );
        }
        void ITreeMachine<WidgetBase>.RemoveRoot(object? argument, Action<WidgetBase, object?>? callback) {
            this.RemoveRoot( argument, callback );
        }

        public void AddRoot(WidgetBase root, object? argument) {
            ITreeMachine<WidgetBase>.AddRoot( this, root, argument );
        }
        public void RemoveRoot(WidgetBase root, object? argument, Action<WidgetBase, object?>? callback) {
            ITreeMachine<WidgetBase>.RemoveRoot( this, root, argument, callback );
        }
        public void RemoveRoot(object? argument, Action<WidgetBase, object?>? callback) {
            ITreeMachine<WidgetBase>.RemoveRoot( this, argument, callback );
        }

    }
}
