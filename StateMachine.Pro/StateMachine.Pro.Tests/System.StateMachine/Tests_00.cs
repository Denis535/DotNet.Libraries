namespace System.StateMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var machine = new StateMachine();
            var a = new A();
            var b = new B();
            {
                // AddState a
                machine.AddState( a, null );
                Assert.That( machine.State, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // RemoveState a
                machine.RemoveState( a, null, null );
                Assert.That( machine.State, Is.Null );
                Assert.That( a.Machine, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            }
            {
                // AddState b
                machine.AddState( b, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // RemoveState b
                machine.RemoveState( b, null, null );
                Assert.That( machine.State, Is.Null );
                Assert.That( b.Machine, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Inactive ) );
            }
        }

        [Test]
        public void Test_01() {
            var machine = new StateMachine();
            var a = new A();
            var b = new B();
            {
                // SetState null
                machine.SetState( null, null, null );
                Assert.That( machine.State, Is.Null );
            }
            {
                // SetState null
                machine.SetState( null, null, null );
                Assert.That( machine.State, Is.Null );
            }
            {
                // SetState a
                machine.SetState( a, null, null );
                Assert.That( machine.State, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // SetState a
                machine.SetState( a, null, null );
                Assert.That( machine.State, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // SetState b
                machine.SetState( b, null, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // SetState b
                machine.SetState( b, null, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( State.Activity_.Active ) );
            }
            {
                // SetState null
                machine.SetState( null, null, null );
                Assert.That( machine.State, Is.Null );
            }
            {
                // SetState null
                machine.SetState( null, null, null );
                Assert.That( machine.State, Is.Null );
            }
        }

    }
}
