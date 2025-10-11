#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class WidgetBase2 : WidgetBase {

        private readonly IDependencyProvider m_Provider;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.Valid( $"Node '{this.Node}' must be disposed", this.Node.IsDisposed );
                return this.m_Provider;
            }
        }

        public WidgetBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
        }
        protected override void OnBeforeDispose() {
            base.OnBeforeDispose();
        }
        protected override void OnAfterDispose() {
            base.OnAfterDispose();
        }

    }
    public abstract class ViewableWidgetBase2<TView> : ViewableWidgetBase<TView>
        where TView : notnull {

        private readonly IDependencyProvider m_Provider;

        protected IDependencyProvider Provider {
            get {
                Assert.Operation.Valid( $"Node '{this.Node}' must be disposed", this.Node.IsDisposed );
                return this.m_Provider;
            }
        }

        public ViewableWidgetBase2(IDependencyProvider provider) {
            this.m_Provider = provider ?? throw new ArgumentNullException( nameof( provider ) );
        }
        protected override void OnBeforeDispose() {
            base.OnBeforeDispose();
        }
        protected override void OnAfterDispose() {
            base.OnAfterDispose();
        }

    }
}
