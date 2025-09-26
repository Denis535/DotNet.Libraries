#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TreeMachine : TreeMachineBase {

        // Root
        public new INode? Root => base.Root;

        // Constructor
        public TreeMachine() {
        }
        public override void Dispose() {
            if (this.Root != null) {
                this.Root.Dispose();
            }
            base.Dispose();
        }

        // SetRoot
        public new void SetRoot(INode? root, object? argument, Action<INode, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
    public class TreeMachine<TUserData> : TreeMachine, IUserData<TUserData> {

        private TUserData m_UserData = default!;

        // UserData
        public TUserData UserData {
            get {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                return this.m_UserData;
            }
            set {
                Assert.Operation.NotDisposed( $"TreeMachine {this} must be non-disposed", !this.IsDisposed );
                this.m_UserData = value;
            }
        }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }
        public override void Dispose() {
            (this.UserData as IDisposable)?.Dispose();
            base.Dispose();
        }

    }
}
