namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_02 {

        [Test]
        public void Test_00() {
            using var machine = new StateMachine<object?>( null );
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
                machine.SetRoot( new ChildrenableState<string>( "a" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.Zero );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count(), Is.Zero );
                Assert.That( machine.Root.Descendants.Count(), Is.Zero );
                Assert.That( machine.Root.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState b
                machine.SetRoot( new ChildrenableState<string>( "b" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.Zero );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count(), Is.Zero );
                Assert.That( machine.Root.Descendants.Count(), Is.Zero );
                Assert.That( machine.Root.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
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
