using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ISchemm.UTF32.Tests
{
    [TestClass]
    public class SplitTests
    {
        public class Entity : String32Replacement.ISegment
        {
            public int StartIndex { get; }
            public int EndIndex { get; }

            public Entity(int startIndex, int endIndex)
            {
                StartIndex = startIndex;
                EndIndex = endIndex;
            }

            public String32 ReplacementValue => String32.FromString($"({StartIndex}-{EndIndex})");
        }

        public class EmptyEntity : String32Replacement.ISegment
        {
            public int StartIndex { get; }
            public int EndIndex { get; }

            public EmptyEntity(int startIndex, int endIndex)
            {
                StartIndex = startIndex;
                EndIndex = endIndex;
            }

            public String32 ReplacementValue => String32.Empty;
        }

        [TestMethod]
        public void TestNoReplace()
        {
            string str = Guid.NewGuid().ToString();
            String32 splitResult = String32Replacement.Replace(Enumerable.Empty<String32Replacement.ISegment>(), String32.FromString(str));
            Assert.AreEqual(str, String32.ToString(splitResult));
        }

        [TestMethod]
        public void TestSimpleReplace()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(4, 8),
                new Entity(12, 15),
                new Entity(10, 11),
            };
            String32 splitResult = String32Replacement.Replace(
                entities,
                String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            Assert.AreEqual("ABCD(4-8)IJ(10-11)L(12-15)PQRSTUVWXYZ", String32.ToString(splitResult));
        }

        [TestMethod]
        public void TestMultipleZeroLengthReplace()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(10, 10),
                new Entity(10, 10),
                new Entity(10, 10),
            };
            String32 splitResult = String32Replacement.Replace(
                entities,
                String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            Assert.AreEqual("ABCDEFGHIJ(10-10)(10-10)(10-10)KLMNOPQRSTUVWXYZ", String32.ToString(splitResult));
        }

        [TestMethod]
        public void TestOverlap()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(5, 15),
                new Entity(10, 20),
            };
            Assert.AreEqual(
                String32.FromString("ABCDE(5-15)(10-20)UVWXYZ"),
            String32Replacement.Replace(
                entities,
                String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
        }

        [TestMethod]
        public void TestOutOfBounds1()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(-10, 3),
            };
            Assert.AreEqual(
                String32.FromString("(-10-3)DEFGHIJKLMNOPQRSTUVWXYZ"),
                String32Replacement.Replace(
                    entities,
                    String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
        }

        [TestMethod]
        public void TestOutOfBounds2()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(5, 50),
            };
            Assert.AreEqual(
                String32.FromString("ABCDE(5-50)"),
                String32Replacement.Replace(
                    entities,
                    String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
        }

        [TestMethod]
        public void TestOutOfBounds3()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(-10, -5),
                new Entity(100, 200)
            };
            Assert.AreEqual(
                String32.FromString("(-10--5)ABCDEFGHIJKLMNOPQRSTUVWXYZ(100-200)"),
                String32Replacement.Replace(
                    entities,
                    String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
        }

        [TestMethod]
        public void TestEmptyOutOfBounds()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new EmptyEntity(-10, 3),
                new EmptyEntity(5, 50),
            };
            String32 result = String32Replacement.Replace(
                entities,
                String32.FromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            Assert.AreEqual("DE", String32.ToString(result));
        }

        [TestMethod]
        public void TestReplace32()
        {
            var entities = new String32Replacement.ISegment[]
            {
                new Entity(2, 5),
                new Entity(6, 9),
            };
            String32 result = String32Replacement.Replace(
                entities,
                String32.FromString("ABC🥺DEF🧐GHI"));
            Assert.AreEqual($"AB{entities[0].ReplacementValue}E{entities[1].ReplacementValue}HI", String32.ToString(result));
        }

        [TestMethod]
        public void TestVisibleRange()
        {
            var str = "ABC🥺DEF🧐GHI";
            var entities = String32Replacement.DisplayRange(1, 10).ToList();

            var entity = new Entity(5, 6);
            entities.Add(entity);

            String32 result = String32Replacement.Replace(entities, String32.FromString(str));
            Assert.AreEqual($"BC🥺D{entity.ReplacementValue}F🧐GH", String32.ToString(result));
        }
    }
}
