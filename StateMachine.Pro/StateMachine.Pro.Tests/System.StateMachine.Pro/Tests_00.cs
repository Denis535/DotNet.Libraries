namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var machine = new StateMachine<State<string>, object?>( null );
            var a = new State<string>( "a" );
            var b = new State<string>( "b" );
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
                Assert.That( a.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState a
                machine.SetState( a, null, null );
                Assert.That( machine.State, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState b
                machine.SetState( b, null, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState b
                machine.SetState( b, null, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Active ) );
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
