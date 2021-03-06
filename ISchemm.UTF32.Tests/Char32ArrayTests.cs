using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ISchemm.UTF32.Tests
{
    [TestClass]
    public class Char32ArrayTests
    {
        [TestMethod]
        public unsafe void TestConversion()
        {
            var arr1 = Char32Array.FromString("🇦🇹 Austria is in the 🇪🇺 European Union");
            var arr2 = Char32Array.FromString("🇦🇹 Austria is in the 🇪🇺 European Union");
            Assert.AreNotSame(arr1, arr2);
            Assert.AreNotEqual(arr1, arr2);
            Assert.IsTrue(arr1.SequenceEqual(arr2));
            Assert.AreEqual(Char32Array.GetString(arr1), Char32Array.GetString(arr2));
        }
    }
}
