#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class ViewBase : DisposableBase, IView {

        public ViewBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
