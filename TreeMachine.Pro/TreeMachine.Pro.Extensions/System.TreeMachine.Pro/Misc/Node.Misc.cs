#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial class Node<T> {

        // Attach
        void INode<T>.Attach(ITreeMachine<T> machine, object? argument) {
            this.Attach( machine, argument );
        }
        void INode<T>.Attach(T parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void INode<T>.Detach(ITreeMachine<T> machine, object? argument) {
            this.Detach( machine, argument );
        }
        void INode<T>.Detach(T parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void INode<T>.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void INode<T>.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

    }
}
