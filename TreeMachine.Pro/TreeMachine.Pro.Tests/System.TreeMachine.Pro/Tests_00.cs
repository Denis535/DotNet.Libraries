namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var machine = new TreeMachine<object?>( null );
            var root = new Node<string>( "root" );
            var a = new Node<string>( "a" );
            var b = new Node<string>( "b" );
            {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
            }
            {
                // machine.Root.AddChildren a, b
                ((Node) machine.Root).AddChildren( [ a, b ], null );
                Assert.That( machine.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in machine.Root.Children) {
                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
                    Assert.That( child.Machine, Is.EqualTo( machine ) );
                    Assert.That( child.Machine_NoRecursive, Is.Null );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( root ) );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
            {
                // machine.Root.RemoveChildren a, b
                _ = ((Node) machine.Root).RemoveChildren( null, null );
                Assert.That( machine.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                foreach (var child in new Node<string>[] { a, b }) {
                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
                    Assert.That( child.Machine, Is.Null );
                    Assert.That( child.Machine_NoRecursive, Is.Null );
                    Assert.That( child.IsRoot, Is.True );
                    Assert.That( child.Root, Is.EqualTo( child ) );
                    Assert.That( child.Parent, Is.Null );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 0 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.Activity, Is.EqualTo( Activity.Inactive ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
            {
                // machine.SetRoot null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
                Assert.That( root.Machine, Is.Null );
                Assert.That( root.Machine_NoRecursive, Is.Null );
                Assert.That( root.IsRoot, Is.True );
                Assert.That( root.Root, Is.EqualTo( root ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Ancestors.Count(), Is.Zero );
                Assert.That( root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( root.Activity, Is.EqualTo( Activity.Inactive ) );
                Assert.That( root.Children.Count, Is.Zero );
                Assert.That( root.Descendants.Count, Is.Zero );
                Assert.That( root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
            }
        }

        [Test]
        public void Test_01() {
            var machine = new TreeMachine<object?>( null );
            var root = new Node<string>( "root" );
            var a = new Node<string>( "a" );
            var b = new Node<string>( "b" );
            {
                // root.AddChildren a, b
                root.AddChildren( [ a, b ], null );
            }
            {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in machine.Root.Children) {
                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
                    Assert.That( child.Machine, Is.EqualTo( machine ) );
                    Assert.That( child.Machine_NoRecursive, Is.Null );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( root ) );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
            {
                // machine.SetRoot null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
        }

        [Test]
        public void Test_02() {
            var machine = new TreeMachine<object?>( null );
            var root = new Node<string>( "root" );
            {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.Zero );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.Zero );
                Assert.That( machine.Root.Descendants.Count, Is.Zero );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
            }
            {
                // machine.SetRoot null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
        }

        [Test]
        public void Test_03() {
            var root = new Node<string>( "root" );
            var a = new Node<string>( "a" );
            var b = new Node<string>( "b" );
            {
                // root.AddChildren a, b
                root.AddChildren( [ a, b ], null );
                Assert.That( root.Machine, Is.Null );
                Assert.That( root.Machine_NoRecursive, Is.Null );
                Assert.That( root.IsRoot, Is.True );
                Assert.That( root.Root, Is.EqualTo( root ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Ancestors.Count(), Is.Zero );
                Assert.That( root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( root.Activity, Is.EqualTo( Activity.Inactive ) );
                Assert.That( root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in root.Children) {
                    Assert.That( child.Machine, Is.Null );
                    Assert.That( child.Machine_NoRecursive, Is.Null );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( root ) );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Activity.Inactive ) );
                    Assert.That( child.Children.Count, Is.Zero );
                    Assert.That( child.Descendants.Count, Is.Zero );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
            {
                // root.RemoveChildren a, b
                _ = root.RemoveChildren( null, null );
                Assert.That( root.Machine, Is.Null );
                Assert.That( root.Machine_NoRecursive, Is.Null );
                Assert.That( root.IsRoot, Is.True );
                Assert.That( root.Root, Is.EqualTo( root ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( root.Activity, Is.EqualTo( Activity.Inactive ) );
                Assert.That( root.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( root.Descendants.Count, Is.EqualTo( 0 ) );
                Assert.That( root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
            }
        }

    }
}
