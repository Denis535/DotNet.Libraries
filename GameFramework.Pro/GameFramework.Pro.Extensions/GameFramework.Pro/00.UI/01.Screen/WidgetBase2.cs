#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class WidgetBase2 : WidgetBase {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Widget {this} must be alive", !this.Node.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public WidgetBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }

    public abstract class ViewableWidgetBase2<TView> : ViewableWidgetBase<TView>
        where TView : notnull {

        protected IDependencyProvider Provider {
            get {
                Check.Operation.Alive( $"Widget {this} must be alive", !this.Node.IsDisposed );
                return IDependencyProvider.Instance;
            }
        }

        public ViewableWidgetBase2() {
        }
        private protected override void OnDisposeInternal() {
            base.OnDisposeInternal();
        }

    }
}
