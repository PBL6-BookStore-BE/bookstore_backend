using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        public string CreateMessage1()
        {
            return "linh";
        }

        public string CreateMessage2()
        {
            return "yen";
        }

        public string CreateMessage3()
        {
            return "ngoc";
        }

        public string CreateMessage4()
        {
            return "nhi";
        }

        public string CreateMessage5()
        {
            return "huong";
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("linh", CreateMessage1());
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual("yen", CreateMessage2());
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual("ngoc", CreateMessage3());
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual("nhi", CreateMessage4());
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual("huong", CreateMessage5());
        }
    }
}