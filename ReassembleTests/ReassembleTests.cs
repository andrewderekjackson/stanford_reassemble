using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reassemble;

namespace ReassembleTests {

    [TestClass]
    public class ReassembleTests {
        private string[] fragments = { "all is well", "ell that en", "hat end", "tends well" };


        [TestMethod]
        public void CanCalculateOffset_ExactMatch() {

            ReassembleImpl impl = new ReassembleImpl();
            var result = impl.CalculateOffset("abcd", "abcd");

            Assert.AreEqual(result.Overlap, 4);
            Assert.AreEqual(result.AlignmentIndex, 0);
        }

        [TestMethod]
        public void CanCalculateOffset_Substring() {

            ReassembleImpl impl = new ReassembleImpl();
            var result = impl.CalculateOffset("abcd", "bcd");

            Assert.AreEqual(result.Overlap, 3);
            Assert.AreEqual(result.AlignmentIndex, 1);
        }
    }
}
