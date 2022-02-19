using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ISchemm.UTF32.Tests
{
    [TestClass]
    public class Char32Tests
    {
        [TestMethod]
        public unsafe void TestSize()
        {
            Assert.AreEqual(sizeof(int), sizeof(Char32));

            int val = 0x12345678;
            Char32 c = new Char32(val);
            Char32* ptr1 = &c;
            int* ptr2 = (int*)ptr1;
            Assert.AreEqual(val, *ptr2);
        }

        [TestMethod]
        public void TestEquality()
        {
            Char32 a = new Char32(0x1f6a3);
            Char32 b = new Char32(0x1f6a3);
            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.IsTrue(a.GetType().IsValueType);
            Assert.AreNotSame(a, b);
        }

        [TestMethod]
        public void TestComparison()
        {
            Char32 a = new Char32(0x1f3ca);
            Char32 b = new Char32(0x1f6a3);
            Assert.IsTrue(b.CompareTo(a) > 0);
            Assert.IsTrue(a.CompareTo(b) < 0);
            Assert.IsTrue(b.CompareTo(b) == 0);
        }

        [TestMethod]
        public void TestString()
        {
            Char32 a = new Char32(0x1f3ca);
            Assert.AreEqual("🏊", $"{a}");
            Char32 b = Char32.FromString("🏊");
            Assert.AreEqual(a, b);
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void TestTooShort()
        {
            Char32.FromString("");
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void TestTooLong()
        {
            Char32.FromString("🇦🇹");
        }
    }
}
