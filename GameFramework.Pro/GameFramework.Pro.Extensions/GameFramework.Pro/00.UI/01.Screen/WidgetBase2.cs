#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class WidgetBase2 : WidgetBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public WidgetBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class ViewableWidgetBase2<TView> : ViewableWidgetBase<TView>
        where TView : notnull {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public ViewableWidgetBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
