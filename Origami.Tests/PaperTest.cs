using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cirno;
using static cirno.Tests.Geometry.GeometryTestHelper;

namespace Origami.Tests {
    [TestClass]
    public class PaperTest {
        [TestMethod]
        public void TestFoldHalf() {
            var paper = new Paper();
            paper.Fold(V(0, 1), V(1, 0));
            AssertHalf(paper);

            paper = new Paper();
            paper.FoldFromMark(L(0, 0, 1, 1));
            AssertHalf(paper);

            void AssertHalf(Paper p) {
                Assert.AreEqual(2, p.Layers.Count);

                var l0 = p.Layers[0];
                var l1 = p.Layers[1];

                var s0 = new HashSet<Vector>(l0.Vertices);
                var s1 = new HashSet<Vector>(l1.Vertices);

                var expected = new[] { V(0, 0), V(1, 1), V(1, 0) };

                Assert.IsTrue(s0.SetEquals(expected));
                Assert.IsTrue(s1.SetEquals(expected));
            }
        }

        [TestMethod]
        public void TestFoldQuarter() {
            var paper = new Paper();
            paper.Fold(V(1, 0f), V(0.6f, 0.8f));
            AssertQuarter(paper);

            paper = new Paper();
            paper.FoldFromMark(L(1, 0.5f, 0, 0));
            AssertQuarter(paper);

            void AssertQuarter(Paper p) {

                Assert.AreEqual(2, p.Layers.Count);

                var l0 = p.Layers[0];
                var l1 = p.Layers[1];

                var s0 = new HashSet<Vector>(l0.Vertices);
                var s1 = new HashSet<Vector>(l1.Vertices);

                var expected0 = new[] {
                    V(0, 0),
                    V(0, 1),
                    V(1, 1),
                    V(1, 0.5f),
                };
                var expected1 = new[] {
                    V(0, 0),
                    V(0.6f, 0.8f),
                    V(1, 0.5f),
                };

                Assert.IsTrue(s0.SetEquals(expected0));
                Assert.IsTrue(s1.SetEquals(expected1));
            }
        }

        [TestMethod]
        public void TestFoldEighth() {
            var paper = new Paper();
            paper.Fold(V(0, 0), V(0.5f, 0.5f));
            AssertEighth(paper);

            paper = new Paper();
            paper.FoldFromMark(L(0.5f, 0, 0, 0.5f));
            AssertEighth(paper);

            void AssertEighth(Paper p) {
                Assert.AreEqual(2, p.Layers.Count);

                var l0 = p.Layers[0];
                var l1 = p.Layers[1];

                var s0 = new HashSet<Vector>(l0.Vertices);
                var s1 = new HashSet<Vector>(l1.Vertices);

                var expected0 = new[] {
                    V(0.5f, 0),
                    V(1, 0),
                    V(1, 1),
                    V(1, 0),
                    V(0.5f, 0),
                };
                var expected1 = new[] {
                    V(0.5f, 0),
                    V(0.5f, 0.5f),
                    V(0, 0.5f),
                };

                Assert.IsTrue(s0.SetEquals(expected0));
                Assert.IsTrue(s1.SetEquals(expected1));
            }
        }
    }
}