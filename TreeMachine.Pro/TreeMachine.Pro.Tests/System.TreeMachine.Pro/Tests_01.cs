//namespace System.TreeMachine.Pro {
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using NUnit.Framework;
//    using Assert = NUnit.Framework.Assert;

//    public class Tests_01 {

//        [Test]
//        public void Test_00() {
//            var machine = new TreeMachine();
//            var root = new Root();
//            var a = new A();
//            var b = new B();

//            root.OnBeforeAttachCallback += arg => {
//                root.AddChildren( [ a, b ], null );
//            };
//            root.OnAfterDetachCallback += arg => {
//                root.RemoveChildren( null, null );
//            };

//            {
//                // machine.AddRoot root
//                machine.AddRoot( root, null );
//                Assert.That( machine.Root, Is.EqualTo( root ) );
//                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
//                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
//                Assert.That( machine.Root.IsRoot, Is.True );
//                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
//                Assert.That( machine.Root.Parent, Is.Null );
//                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
//                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
//                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
//                Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
//                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
//                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
//                foreach (var child in machine.Root.Children) {
//                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
//                    Assert.That( child.Machine, Is.EqualTo( machine ) );
//                    Assert.That( child.Machine_NoRecursive, Is.Null );
//                    Assert.That( child.IsRoot, Is.False );
//                    Assert.That( child.Root, Is.EqualTo( root ) );
//                    Assert.That( child.Parent, Is.EqualTo( root ) );
//                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
//                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
//                    Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
//                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
//                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
//                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
//                }
//            }
//            {
//                // machine.RemoveRoot
//                machine.RemoveRoot( null, null );
//                Assert.That( machine.Root, Is.Null );
//            }
//        }

//        [Test]
//        public void Test_02() {
//            var machine = new TreeMachine();
//            var root = new Root();
//            var a = new A();
//            var b = new B();

//            root.OnAfterAttachCallback += arg => {
//                root.AddChildren( [ a, b ], null );
//            };
//            root.OnBeforeDetachCallback += arg => {
//                root.RemoveChildren( null, null );
//            };

//            {
//                // machine.AddRoot root
//                machine.AddRoot( root, null );
//                Assert.That( machine.Root, Is.EqualTo( root ) );
//                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
//                Assert.That( machine.Root.Machine_NoRecursive, Is.EqualTo( machine ) );
//                Assert.That( machine.Root.IsRoot, Is.True );
//                Assert.That( machine.Root.Root, Is.EqualTo( root ) );
//                Assert.That( machine.Root.Parent, Is.Null );
//                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
//                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
//                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
//                Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
//                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
//                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
//                foreach (var child in machine.Root.Children) {
//                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
//                    Assert.That( child.Machine, Is.EqualTo( machine ) );
//                    Assert.That( child.Machine_NoRecursive, Is.Null );
//                    Assert.That( child.IsRoot, Is.False );
//                    Assert.That( child.Root, Is.EqualTo( root ) );
//                    Assert.That( child.Parent, Is.EqualTo( root ) );
//                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
//                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
//                    Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
//                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
//                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
//                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
//                }
//            }
//            {
//                // machine.RemoveRoot
//                machine.RemoveRoot( null, null );
//                Assert.That( machine.Root, Is.Null );
//            }
//        }

//    }
//}
