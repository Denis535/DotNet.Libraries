#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal enum Lifecycle {
        Alive,
        Disposing,
        Disposed,
    }
}
