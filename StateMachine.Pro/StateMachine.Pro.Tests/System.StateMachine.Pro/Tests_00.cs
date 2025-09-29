namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;
    using StateMachine = StateMachine<object?, string>;
    using State = State<object?, string>;
    using ChildableState = ChildableState<object?, string>;
    using ChildrenableState = ChildrenableState<object?, string>;

    public class Tests_00 {

        [Test]
        public void Test_00_a() {
            using var machine = new StateMachine();
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
            {
                // SetState a
                machine.SetRoot( new State( "a" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState b
                machine.SetRoot( new ChildableState( "b" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState c
                machine.SetRoot( new ChildrenableState( "b" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
        }

        [Test]
        public void Test_00_b() {
            using var machine = new StateMachine();
            {
                // SetState null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
            {
                // SetState a
                machine.SetRoot( new State( "a" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState b
                machine.SetRoot( new ChildableState( "b" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
            }
            {
                // SetState c
                machine.SetRoot( new ChildrenableState( "b" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
            }
        }

    }
}
