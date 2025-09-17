#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class EntityBase : DisposableBase {

        public EntityBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    //// CharacterBase
    //public abstract class CharacterBase : EntityBase {
    //}
    //// MachineBase
    //public abstract class MachineBase : EntityBase {
    //}
    //// InteractiveBase
    //public abstract class InteractiveBase : EntityBase {
    //}
    //// CameraBase
    //public abstract class CameraBase : EntityBase {
    //}
    //// WorldBase
    //public abstract class WorldBase : EntityBase {
    //}
}
