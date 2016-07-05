using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reassemble;

namespace ReassembleTests {

    [TestClass]
    public class ReassembleTests {
        
        [TestMethod]
        public void ExactMatch() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(4, impl.CalculateOverlap("abcd", "abcd").Overlap);
            Assert.AreEqual("abcd", impl.CalculateOverlap("abcd", "abcd").MergedString);
        }

        [TestMethod]
        public void MatchesOnRight() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(2, impl.CalculateOverlap("abcd", "cdef").Overlap);
            Assert.AreEqual("abcdef", impl.CalculateOverlap("abcd", "cdef").MergedString);

            Assert.AreEqual(3, impl.CalculateOverlap("all is well", "ell that en").Overlap);
            Assert.AreEqual("abcd", impl.CalculateOverlap("abcd", "abcd").MergedString);

        }

        [TestMethod]
        public void MatchesOnLeft() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(2, impl.CalculateOverlap("cdef", "abcd").Overlap);
            Assert.AreEqual("abcdef", impl.CalculateOverlap("cdef", "abcd").MergedString);

            Assert.AreEqual(1, impl.CalculateOverlap("defg", "abcd").Overlap);
            Assert.AreEqual("abcdefg", impl.CalculateOverlap("defg", "abcd").MergedString);

            Assert.AreEqual("abcdefg", impl.CalculateOverlap("defg", "abcd").MergedString);
        }

        [TestMethod]
        public void NoMatch_AppendsToRight() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(0, impl.CalculateOverlap("abcd", "zyx").Overlap);
            Assert.AreEqual("abcdzyx", impl.CalculateOverlap("abcd", "zyx").MergedString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanCalculateOffset_Null_ThrowsException() {
            ReassembleImpl impl = new ReassembleImpl();
            impl.CalculateOverlap(null, null);
        }

        [TestMethod]
        public void CanCalculateOffset_Step1() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(6, impl.CalculateOverlap("ell that en", "hat end").Overlap);
            Assert.AreEqual("ell that end", impl.CalculateOverlap("ell that en", "hat end").MergedString);
        }

        [TestMethod]
        public void CanCalculateOffset_Step2() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(5, impl.CalculateOverlap("ell that end", "t ends well").Overlap);
            Assert.AreEqual("ell that ends well", impl.CalculateOverlap("ell that end", "t ends well").MergedString);
        }

        [TestMethod]
        public void CanCalculateOffset_Step3() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(3, impl.CalculateOverlap("all is well", "ell that ends well").Overlap);
            Assert.AreEqual("all is well that ends well", impl.CalculateOverlap("all is well", "ell that ends well").MergedString);
        }

        [TestMethod]
        public void EndToEnd_NoMatches() {
            string[] fragments = { "abcd", "efgh" };
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("abcdefgh", impl.Reassemble(fragments));
        }

        [TestMethod]
        public void EndToEnd_NoInput() {
            string[] fragments = { "" };
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("", impl.Reassemble(fragments));
        }

        [TestMethod]
        public void EndToEnd_NullInput() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("", impl.Reassemble(null));
        }

        [TestMethod]
        public void EndToEnd_AssignmentExample() {
            string[] fragments = { "all is well", "ell that en", "hat end", "t ends well" };
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("all is well that ends well", impl.Reassemble(fragments));
        }

    }
}

