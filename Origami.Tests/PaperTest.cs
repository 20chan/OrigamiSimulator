using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cirno;
using V = cirno.Vector;

namespace Origami.Tests {
    [TestClass]
    public class PaperTest {
        [TestMethod, TestCategory("Fold")]
        public void TestFoldHalf() {
            var paper = new Paper();
            paper.Fold(new V(0, 0), new V(1, 1));

            Assert.AreEqual(2, paper.Layers.Count);

            var l0 = paper.Layers[0];
            var l1 = paper.Layers[1];

            var s0 = new HashSet<Vector>(l0.Vertices);
            var s1 = new HashSet<Vector>(l1.Vertices);

            var expected = new[] {new V(1, 0), new V(1, 1), new V(0, 1)};

            Assert.IsTrue(s0.SetEquals(expected));
            Assert.IsTrue(s1.SetEquals(expected));
        }

        [TestMethod, TestCategory("Fold")]
        public void TestFoldQuarter() {
            var paper = new Paper();
            paper.Fold(new V(0, 0), new V(0.5f, 0.5f));

            Assert.AreEqual(2, paper.Layers.Count);

            var l0 = paper.Layers[0];
            var l1 = paper.Layers[1];

            var s0 = new HashSet<Vector>(l0.Vertices);
            var s1 = new HashSet<Vector>(l1.Vertices);

            var expected0 = new[] {
                new V(0.5f, 0),
                new V(1, 0),
                new V(1, 1),
                new V(1, 0),
                new V(0.5f, 0),
            };
            var expected1 = new[] {
                new V(0.5f, 0),
                new V(0.5f, 0.5f),
                new V(0, 0.5f),
            };

            Assert.IsTrue(s0.SetEquals(expected0));
            Assert.IsTrue(s1.SetEquals(expected1));
        }
    }
}