using ISchemm.UTF32.Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ISchemm.UTF32.Tests
{
    [TestClass]
    public class Char32StringTests
    {
        [TestMethod]
        public unsafe void TestConversion()
        {
            string str = "🇦🇹 Austria is in the 🇪🇺 European Union";
            if (str.Count() == 38)
                Assert.Inconclusive();

            var str1 = String32.FromString(str);
            Assert.AreEqual(str, $"{str1}");
            Assert.AreEqual(38, str1.Count());

            var str2 = String32.FromString(str);
            Assert.AreNotSame(str1, str2);
            Assert.IsTrue(str1.SequenceEqual(str2));
            Assert.IsTrue(str1 == str2);
        }

        [TestMethod]
        public unsafe void TestSubstring()
        {
            string str = "🇦🇹 Austria is in the 🇪🇺 European Union";
            if (str.Substring(24) == "European Union")
                Assert.Inconclusive();
            if (str.Substring(3, 7) == "Austria")
                Assert.Inconclusive();

            var str1 = String32.FromString(str);
            Assert.AreEqual("European Union", $"{str1.Substring(24)}");
            Assert.AreEqual("Austria", $"{str1.Substring(3, 7)}");
        }
    }
}
