using System.ComponentModel;
using NUnit.Framework;

namespace UnitTestsSamples.Sample1
{
    public class MathsHelper
    {
        public int Add(int a, int b)
        {
            int x = a + b;
            return x;
        }

        public int Subtract(int a, int b)
        {
            int x = a - b;
            return x;
        }
    }

    [TestFixture]
    public class MathsHelperTest
    {

        [TestCase]
        public void TestAdd()
        {
            MathsHelper helper = new MathsHelper();
            int result = helper.Add(20, 10);
            Assert.AreEqual(30, result);
        }

        [TestCase]
        public void TestSubtract()
        {
            MathsHelper helper = new MathsHelper();
            int result = helper.Subtract(20, 10);
            Assert.AreEqual(10, result);
        }

        [TestCase(10, 10, 20)]
        [TestCase(10, 20, 30)]
        public void TestAdd(int p1, int p2, int result)
        {
            MathsHelper helper = new MathsHelper();
            Assert.AreEqual(helper.Add(p1, p2), result);
        }

        [TestCase(30, 20, 10)]
        [TestCase(20, 30, -10)]
        public void TestSubtract(int p1, int p2, int result)
        {
            MathsHelper helper = new MathsHelper();
            Assert.AreEqual(helper.Subtract(p1, p2), result);
        }
    }
}
