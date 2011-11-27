using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mstum.utils.tests
{
    [TestClass]
    public class CircularBufferTests
    {
        [TestMethod]
        public void Add_MoreThanCapacity_ProperlyCircles()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            Assert.AreEqual(2, buffer.Skip(0).First());
            Assert.AreEqual(3, buffer.Skip(1).First());
            Assert.AreEqual(4, buffer.Skip(2).First());
        }

        [TestMethod]
        public void Add_WithinCapacity_ProperlyAdds()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);

            Assert.AreEqual(1, buffer.Skip(0).First());
            Assert.AreEqual(2, buffer.Skip(1).First());
        }

        [TestMethod]
        public void Add_MoreThanCapacityTwice_ProperlyCircles()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            buffer.Add(5);
            buffer.Add(6);
            buffer.Add(7);
            buffer.Add(8);

            Assert.AreEqual(6, buffer.Skip(0).First());
            Assert.AreEqual(7, buffer.Skip(1).First());
            Assert.AreEqual(8, buffer.Skip(2).First());
        }

        [TestMethod]
        public void Add_ToArray_ProperlyWorks()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            buffer.Add(5);
            buffer.Add(6);
            buffer.Add(7);
            buffer.Add(8);

            var result = buffer.ToArray();

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(6, result.Skip(0).First());
            Assert.AreEqual(7, result.Skip(1).First());
            Assert.AreEqual(8, result.Skip(2).First());
        }

        [TestMethod]
        public void Sum_ProperlySums()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            buffer.Add(5);
            buffer.Add(6);
            buffer.Add(7);
            buffer.Add(8);

            var result = buffer.Sum();
            Assert.AreEqual(21 /*6+7+8*/, result);
        }

        [TestMethod]
        public void ContainsInt_ValueExists_ReturnsTrue()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            buffer.Add(5);
            buffer.Add(6);
            buffer.Add(7);
            buffer.Add(8);

            Assert.IsTrue(buffer.Contains(6));
            Assert.IsTrue(buffer.Contains(7));
            Assert.IsTrue(buffer.Contains(8));
        }

        [TestMethod]
        public void ContainsInt_ValueDoesNotExist_ReturnsFalse()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            buffer.Add(5);
            buffer.Add(6);
            buffer.Add(7);
            buffer.Add(8);

            var result = buffer.Contains(3);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsDefaultInt_NotYetFull_ReturnsFalse()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);

            var result = buffer.Contains(default(int));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsDefaultRefType_NotYetFull_ReturnsFalse()
        {
            var buffer = new CircularBuffer<TestRefType>(3);
            buffer.Add(new TestRefType(Guid.NewGuid()));
            buffer.Add(new TestRefType(Guid.NewGuid()));

            var result = buffer.Contains(default(TestRefType));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsRefType_ValueDoesNotExist_ReturnsFalse()
        {
            var buffer = new CircularBuffer<TestRefType>(3);
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();

            buffer.Add(new TestRefType(id1));
            buffer.Add(new TestRefType(id2));
            buffer.Add(new TestRefType(id3));
            buffer.Add(new TestRefType(id4));
    

            var result = buffer.Contains(new TestRefType(id1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsRefType_ValueExists_ReturnsTrue()
        {
            var buffer = new CircularBuffer<TestRefType>(3);
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();

            buffer.Add(new TestRefType(id1));
            buffer.Add(new TestRefType(id2));
            buffer.Add(new TestRefType(id3));
            buffer.Add(new TestRefType(id4));


            Assert.IsTrue(buffer.Contains(new TestRefType(id2)));
            Assert.IsTrue(buffer.Contains(new TestRefType(id3)));
            Assert.IsTrue(buffer.Contains(new TestRefType(id4)));
        }

        [TestMethod]
        public void ContainsRefType_NullExists_ReturnsTrue()
        {
            var buffer = new CircularBuffer<TestRefType>(3);
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            buffer.Add(new TestRefType(id1));
            buffer.Add(new TestRefType(id2));
            buffer.Add(null);
            buffer.Add(new TestRefType(id3));


            Assert.IsTrue(buffer.Contains(null));
        }

        [TestMethod]
        public void ContainsRefType_NullDoesNotExist_ReturnsTrue()
        {
            var buffer = new CircularBuffer<TestRefType>(3);
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            buffer.Add(new TestRefType(id1));
            buffer.Add(new TestRefType(id2));
            buffer.Add(new TestRefType(id3));


            Assert.IsFalse(buffer.Contains(null));
        }

        [TestMethod]
        public void Enumerate_ForEach_ProperlyEnumerates()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            var i = 2;
            foreach (var item in buffer)
            {
                Assert.AreEqual(i, item);
                i++;
            }
        }

        [TestMethod]
        public void Enumerate_Modify_ThrowsException()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            bool thrown = false;

            try
            {
                foreach (var item in buffer)
                {
                    buffer.Add(5);
                }
            }
            catch (InvalidOperationException)
            {
                if (thrown) Assert.Fail("Thrown was set more than once");
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void Enumerate_Reset_ProperlyResets()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);

            var en = buffer.GetEnumerator();
            en.MoveNext();
            Assert.AreEqual(1, en.Current);
            en.MoveNext();
            Assert.AreEqual(2, en.Current);
            en.Reset();
            en.MoveNext();
            Assert.AreEqual(1, en.Current);
            en.MoveNext();
            Assert.AreEqual(2, en.Current);
        }

        [TestMethod]
        public void CopyTo_WithinCapacity_OnlyCopiesAddedItems()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);

            var result = new int[2];

            buffer.CopyTo(result, 0);

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
        }

        [TestMethod]
        public void CopyTo_OverCapacity_RetainsOrder()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            var result = new int[3];

            buffer.CopyTo(result, 0);

            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3, result[1]);
            Assert.AreEqual(4, result[2]);
        }


        [TestMethod]
        public void CopyToWithIndex_OverCapacity_RetainsOrder()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            var result = new int[5];

            buffer.CopyTo(result, 2);

            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(0, result[1]);
            Assert.AreEqual(2, result[2]);
            Assert.AreEqual(3, result[3]);
            Assert.AreEqual(4, result[4]);
        }

        [TestMethod]
        [Ignore] // This test runs for ages, only run if needed
        public void Add_HugeNumber_WorksCorrectly()
        {
            var buffer = new CircularBuffer<long>(100000);
            var random = new Random();

            long lastValue = 0;

            for (long i = 0; i < int.MaxValue + 1000L; i++)
            {
                lastValue++;
                buffer.Add(lastValue);
            }

            long testValue = lastValue - buffer.Count + 1;
            foreach (var item in buffer)
            {
                Assert.AreEqual(testValue, item);
                testValue++;
            }
        }

        private class TestRefType
        {
            public Guid Id { get; private set; }

            public TestRefType(Guid id)
            {
                Id = id;
            }

            public override bool Equals(object obj)
            {
                return Id.Equals((obj as TestRefType).Id);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
        }
    }
}
