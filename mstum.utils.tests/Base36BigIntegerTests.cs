using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace mstum.utils.tests
{
    [TestClass]
    public class Base36BigIntegerTests
    {

        [TestMethod]
        public void Encode_129127208515967000000_R91Q77F6XM8JK()
        {
            var input = BigInteger.Multiply(129127208515967, 1000000);
            string result = Base36Big.Encode(input);
            Assert.AreEqual("R91Q77F6XM8JK", result, true);
        }

        [TestMethod]
        public void Encode_100000000000000000000_L3R41IFS0Q5TS()
        {
            var input = BigInteger.Multiply(100000000000000, 1000000);
            string result = Base36Big.Encode(input);
            Assert.AreEqual("L3R41IFS0Q5TS", result, true);
        }

        [TestMethod]
        public void Encode_1000000000000_CRE66I9S()
        {
            var input = new BigInteger(1000000000000L);
            string result = Base36Big.Encode(input);
            Assert.AreEqual("CRE66I9S", result, true);
        }

        [TestMethod]
        public void Encode_1000000000_GJDGXS()
        {
            var input = new BigInteger(1000000000L);
            string result = Base36Big.Encode(input);
            Assert.AreEqual("GJDGXS", result, true);
        }

        [TestMethod]
        public void Encode_1000000_LFLS()
        {
            var input = new BigInteger(1000000L);
            string result = Base36Big.Encode(input);
            Assert.AreEqual("LFLS", result, true);
        }

        [TestMethod]
        public void Encode_100000_255S()
        {
            var input = new BigInteger(100000L);
            string result = Base36Big.Encode(input);
            Assert.AreEqual("255S", result, true);
        }

        [TestMethod]
        public void Decode_255S_100000()
        {
            const string input = "255S";
            BigInteger result = Base36Big.Decode(input);
            Assert.AreEqual(new BigInteger(100000L), result);
        }

        [TestMethod]
        public void Decode_CRE66I9S_1000000000000()
        {
            const string input = "CRE66I9S";
            BigInteger result = Base36Big.Decode(input);
            Assert.AreEqual(new BigInteger(1000000000000L), result);
        }

        [TestMethod]
        public void Decode_100000000_2821109907456()
        {
            const string input = "100000000";
            BigInteger result = Base36Big.Decode(input);
            Assert.AreEqual(new BigInteger(2821109907456L), result);
        }

        [TestMethod]
        public void Decode_0H_17()
        {
            const string input = "0H";
            BigInteger result = Base36Big.Decode(input);
            Assert.AreEqual(new BigInteger(17L), result);
        }

        [TestMethod]
        public void Decode_R91Q77F6XM8JK_129127208515967000000()
        {
            const string input = "R91Q77F6XM8JK";
            var expected = BigInteger.Multiply(129127208515967, 1000000);

            BigInteger result = Base36Big.Decode(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decode_L3R41IFS0Q5TS_100000000000000000000()
        {
            const string input = "L3R41IFS0Q5TS";
            var expected = BigInteger.Multiply(100000000000000, 1000000);

            BigInteger result = Base36Big.Decode(input);

            Assert.AreEqual(expected, result);
        }
    }
}
