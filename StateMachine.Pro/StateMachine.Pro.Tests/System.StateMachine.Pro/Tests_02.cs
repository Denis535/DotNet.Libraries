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
            var a = new ChildrenableState<string>( "a" );
            var b = new ChildrenableState<string>( "b" );
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
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( a.Children, Is.Empty );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState a
                machine.SetRoot( a, null, null );
                Assert.That( machine.Root, Is.EqualTo( a ) );
                Assert.That( a.Machine, Is.EqualTo( machine ) );
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( a.Children, Is.Empty );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState b
                machine.SetRoot( b, null, null );
                Assert.That( machine.Root, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( b.Children, Is.Empty );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // SetState b
                machine.SetRoot( b, null, null );
                Assert.That( machine.Root, Is.EqualTo( b ) );
                Assert.That( b.Machine, Is.EqualTo( machine ) );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( b.Children, Is.Empty );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
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
