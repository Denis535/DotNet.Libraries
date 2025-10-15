namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;
    using TreeMachine = TreeMachine<object?, string>;
    using Node = Node<object?, string>;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var machine = new TreeMachine( null );
            var root = new Node( "root" );
            using (machine) {
                // machine.SetRoot root
                machine.SetRoot( new Node( "root" ), null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                {
                    // machine.Root.AddChildren a, b
                    ((Node) machine.Root).AddChildren( [ new Node( "a" ), new Node( "b" ) ], null );
                    Assert.That( machine.Root, Is.Not.Null );
                    Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.IsRoot, Is.True );
                    Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                    Assert.That( machine.Root.Parent, Is.Null );
                    Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                    Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                    Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                    Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                    Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                    foreach (var child in machine.Root.Children) {
                        Assert.That( child.Owner, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Machine, Is.EqualTo( machine ) );
                        Assert.That( child.IsRoot, Is.False );
                        Assert.That( child.Root, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Parent, Is.EqualTo( machine.Root ) );
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
                    _ = ((Node) machine.Root).RemoveChildren( i => true, null, null );
                    Assert.That( machine.Root, Is.Not.Null );
                    Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.IsRoot, Is.True );
                    Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                    Assert.That( machine.Root.Parent, Is.Null );
                    Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                    Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                    Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( machine.Root.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
                // machine.SetRoot null
                machine.SetRoot( null, null, null );
                Assert.That( machine.Root, Is.Null );
            }
        }

        [Test]
        public void Test_01() {
            var machine = (TreeMachine?) default;
            machine = new TreeMachine( null ) {
                OnDisposeCallback = () => {
                    machine!.Root!.Dispose();
                }
            };
            var root = (Node?) default;
            root = new Node( "root" ) {
                OnDisposeCallback = () => {
                    foreach (var child in root!.Children) {
                        child.Dispose();
                    }
                }
            };
            using (machine) {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                {
                    // machine.Root.AddChildren a, b
                    ((Node) machine.Root).AddChildren( [ new Node( "a" ), new Node( "b" ) ], null );
                    Assert.That( machine.Root, Is.Not.Null );
                    Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.IsRoot, Is.True );
                    Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                    Assert.That( machine.Root.Parent, Is.Null );
                    Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                    Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                    Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                    Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                    Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                    foreach (var child in machine.Root.Children) {
                        Assert.That( child.Owner, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Machine, Is.EqualTo( machine ) );
                        Assert.That( child.IsRoot, Is.False );
                        Assert.That( child.Root, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Parent, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                        Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                        Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
                        Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                        Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                        Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                    }
                }
            }
        }

        [Test]
        public void Test_02() {
            var machine = (TreeMachine?) default;
            machine = new TreeMachine( null ) {
                OnDisposeCallback = () => {
                    machine!.SetRoot( null, null, null );
                }
            };
            var root = (Node?) default;
            root = new Node( "root" ) {
                OnDisposeCallback = () => {
                    _ = root!.RemoveChildren( i => true, null, null );
                }
            };
            using (machine) {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 0 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                {
                    // machine.Root.AddChildren a, b
                    ((Node) machine.Root).AddChildren( [ new Node( "a" ), new Node( "b" ) ], null );
                    Assert.That( machine.Root, Is.Not.Null );
                    Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                    Assert.That( machine.Root.IsRoot, Is.True );
                    Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                    Assert.That( machine.Root.Parent, Is.Null );
                    Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                    Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                    Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                    Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                    Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                    foreach (var child in machine.Root.Children) {
                        Assert.That( child.Owner, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Machine, Is.EqualTo( machine ) );
                        Assert.That( child.IsRoot, Is.False );
                        Assert.That( child.Root, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Parent, Is.EqualTo( machine.Root ) );
                        Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                        Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                        Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
                        Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                        Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                        Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                    }
                }
            }
        }

        [Test]
        public void Test_10() {
            var machine = (TreeMachine?) default;
            machine = new TreeMachine( null ) {
                OnDisposeCallback = () => {
                    machine!.SetRoot( null, null, null );
                }
            };
            var root = (Node?) default;
            root = new Node( "root" ) {
                OnDisposeCallback = () => {
                },
                OnAttachCallback = arg => {
                    root!.AddChildren( [ new Node( "a" ), new Node( "b" ) ], null );
                },
                OnDetachCallback = arg => {
                    _ = root!.RemoveChildren( i => true, null, null );
                }
            };
            using (machine) {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in machine.Root.Children) {
                    Assert.That( child.Owner, Is.EqualTo( machine.Root ) );
                    Assert.That( child.Machine, Is.EqualTo( machine ) );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( machine.Root ) );
                    Assert.That( child.Parent, Is.EqualTo( machine.Root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
        }

        [Test]
        public void Test_11() {
            var machine = (TreeMachine?) default;
            machine = new TreeMachine( null ) {
                OnDisposeCallback = () => {
                    machine!.SetRoot( null, null, null );
                }
            };
            var root = (Node?) default;
            root = new Node( "root" ) {
                OnDisposeCallback = () => {
                },
                OnActivateCallback = arg => {
                    root!.AddChildren( [ new Node( "a" ), new Node( "b" ) ], null );
                },
                OnDeactivateCallback = arg => {
                    _ = root!.RemoveChildren( i => true, null, null );
                }
            };
            using (machine) {
                // machine.SetRoot root
                machine.SetRoot( root, null, null );
                Assert.That( machine.Root, Is.Not.Null );
                Assert.That( machine.Root.Owner, Is.EqualTo( machine ) );
                Assert.That( machine.Root.Machine, Is.EqualTo( machine ) );
                Assert.That( machine.Root.IsRoot, Is.True );
                Assert.That( machine.Root.Root, Is.EqualTo( machine.Root ) );
                Assert.That( machine.Root.Parent, Is.Null );
                Assert.That( machine.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( machine.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( machine.Root.Activity, Is.EqualTo( Activity.Active ) );
                Assert.That( machine.Root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( machine.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in machine.Root.Children) {
                    Assert.That( child.Owner, Is.EqualTo( machine.Root ) );
                    Assert.That( child.Machine, Is.EqualTo( machine ) );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( machine.Root ) );
                    Assert.That( child.Parent, Is.EqualTo( machine.Root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Activity.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
        }

    }
}
