using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mstum.utils.tests
{
    // ReSharper disable InconsistentNaming
    [TestClass]
    public class Base36Tests
    {
        [TestMethod]

        public void Encode_1000000000000_CRE66I9S()
        {
            const long input = 1000000000000L;
            string result = Base36.Encode(input);
            Assert.AreEqual("CRE66I9S", result,true);
        }

        [TestMethod]
        public void Encode_1000000000_GJDGXS()
        {
            const long input = 1000000000L;
            string result = Base36.Encode(input);
            Assert.AreEqual("GJDGXS", result, true);
        }

        [TestMethod]
        public void Encode_1000000_LFLS()
        {
            const long input = 1000000L;
            string result = Base36.Encode(input);
            Assert.AreEqual("LFLS", result, true);
        }

        [TestMethod]
        public void Encode_100000_255S()
        {
            const long input = 100000L;
            string result = Base36.Encode(input);
            Assert.AreEqual("255S", result, true);
        }

        [TestMethod]
        public void Decode_255S_100000()
        {
            const string input = "255S";
            long result = Base36.Decode(input);
            Assert.AreEqual(100000L, result);
        }

        [TestMethod]
        public void Decode_CRE66I9S_1000000000000()
        {
            const string input = "CRE66I9S";
            long result = Base36.Decode(input);
            Assert.AreEqual(1000000000000L, result);
        }

        [TestMethod]
        public void Decode_100000000_2821109907456()
        {
            const string input = "100000000";
            long result = Base36.Decode(input);
            Assert.AreEqual(2821109907456L, result);
        }

        [TestMethod]
        public void Decode_0H_17()
        {
            const string input = "0H";
            long result = Base36.Decode(input);
            Assert.AreEqual(17L, result);
        }
    }
    // ReSharper restore InconsistentNaming
}
