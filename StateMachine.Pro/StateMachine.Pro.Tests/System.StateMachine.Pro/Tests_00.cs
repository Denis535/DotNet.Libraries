namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            using var machine = new StateMachine<object?>( null );
            var a = new State<string>( "a" );
            var b = new State<string>( "b" );
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
            {
                // SetState a
                machine.SetRoot( a, null, null );
                Assert.That( machine.Root, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState a
                machine.SetRoot( a, null, null );
                Assert.That( machine.Root, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState b
                machine.SetRoot( b, null, null );
                Assert.That( machine.Root, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState b
                machine.SetRoot( b, null, null );
                Assert.That( machine.Root, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
        }

    }
}
