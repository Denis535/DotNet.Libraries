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
        public void Test_00() {
            var machine = new StateMachine( null );
            using (machine) {
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
                    machine.SetRoot( new ChildrenableState( "c" ), null, null );
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
        }

        [Test]
        public void Test_01() {
            var machine = (StateMachine?) default;
            machine = new StateMachine( null ) {
                OnDisposeCallback = () => {
                    machine!.SetRoot( null, null, null );
                }
            };
            using (machine) {
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
                    machine.SetRoot( new ChildrenableState( "c" ), null, null );
                    Assert.That( machine.Root, Is.Not.Null );
                    Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                }
            }
        }

        [Test]
        public void Test_02() {
            var machine = (StateMachine?) default;
            machine = new StateMachine( null ) {
                OnDisposeCallback = () => {
                    machine!.Root!.Dispose();
                }
            };
            using (machine) {
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
                    machine.SetRoot( new ChildrenableState( "c" ), null, null );
                    Assert.That( machine.Root, Is.Not.Null );
                    Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                }
            }
        }

    }
}
