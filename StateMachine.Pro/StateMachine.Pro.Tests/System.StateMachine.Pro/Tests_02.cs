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
            var machine = new StateMachine<object?>( null );
            var a = new ChildrenableState<string>( "a" );
            var b = new ChildrenableState<string>( "b" );
            {
                // AddState a
                machine.AddState( a, null );
                Assert.That( machine.State, Is.EqualTo( a ) );
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
                // RemoveState a
                machine.RemoveState( a, null, null );
                Assert.That( machine.State, Is.Null );
                Assert.That( a.Machine, Is.Null );
                Assert.That( a.IsRoot, Is.True );
                Assert.That( a.Root, Is.EqualTo( a ) );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Ancestors.Count(), Is.Zero );
                Assert.That( a.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( a.Activity, Is.EqualTo( Activity.Inactive ) );
                Assert.That( a.Children, Is.Empty );
                Assert.That( a.Descendants.Count(), Is.Zero );
                Assert.That( a.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
            {
                // AddState b
                machine.AddState( b, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
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
                // RemoveState b
                machine.RemoveState( b, null, null );
                Assert.That( machine.State, Is.Null );
                Assert.That( b.Machine, Is.Null );
                Assert.That( b.IsRoot, Is.True );
                Assert.That( b.Root, Is.EqualTo( b ) );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Ancestors.Count(), Is.Zero );
                Assert.That( b.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( b.Activity, Is.EqualTo( Activity.Inactive ) );
                Assert.That( b.Children, Is.Empty );
                Assert.That( b.Descendants.Count(), Is.Zero );
                Assert.That( b.DescendantsAndSelf.Count(), Is.EqualTo( 1 ) );
            }
        }

        [Test]
        public void Test_01() {
            var machine = new StateMachine<object?>( null );
            var a = new ChildrenableState<string>( "a" );
            var b = new ChildrenableState<string>( "b" );
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
                machine.SetState( a, null, null );
                Assert.That( machine.State, Is.EqualTo( a ) );
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
                machine.SetState( b, null, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
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
                machine.SetState( b, null, null );
                Assert.That( machine.State, Is.EqualTo( b ) );
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
