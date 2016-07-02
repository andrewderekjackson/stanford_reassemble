using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reassemble;

namespace ReassembleTests {

    [TestClass]
    public class ReassembleTests {
        private string[] fragments = { "all is well", "ell that en", "hat end", "t ends well" };


        [TestMethod]
        public void CanCalculateOffset_ExactMatch() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(4, impl.CalculateOverlap("abcd", "abcd"));
        }

        [TestMethod]
        public void CanMerge_ExactMatch() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("abcd", impl.Merge("abcd", "abcd"));
        }


        [TestMethod]
        public void CanCalculateOffset_NoMatch() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(0, impl.CalculateOverlap("abcd", "zyx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanCalculateOffset_Null() {
            ReassembleImpl impl = new ReassembleImpl();
            impl.CalculateOverlap(null, null);
        }


        [TestMethod]
        public void CanCalculateOffset_MatchesOnRight() {

            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(2, impl.CalculateOverlap("abcd", "cdef"));
        }

        [TestMethod]
        public void CanMerge_MatchesOnRight() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("abcdef", impl.Merge("abcd", "cdef"));
        }

        [TestMethod]
        public void CanCalculateOffset_MatchesOnLeft() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(1, impl.CalculateOverlap("defg", "abcd"));
        }

        [TestMethod]
        public void CanMerge_MatchesOnLeft() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("abcdefg", impl.Merge("defg", "abcd"));
        }


        [TestMethod]
        public void CanCalculateOffset_Step1() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(6, impl.CalculateOverlap("ell that en", "hat end"));
        }

        [TestMethod]
        public void CanMerge_Step1() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("ell that end", impl.Merge("ell that en", "hat end"));
        }

        [TestMethod]
        public void CanCalculateOffset_Step2() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(5, impl.CalculateOverlap("ell that end", "t ends well"));
        }

        [TestMethod]
        public void CanMerge_Step2() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("ell that ends well", impl.Merge("ell that end", "t ends well"));
        }


        [TestMethod]
        public void CanCalculateOffset_Step3() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual(11, impl.CalculateOverlap("all is well", "ell that ends well"));
        }

        [TestMethod]
        public void CanMerge_Step3() {
            ReassembleImpl impl = new ReassembleImpl();
            Assert.AreEqual("all is well that ends well", impl.Merge("all is well", "ell that ends well"));
        }


    }
}
