using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("linh", Program.CreateMessage1());
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual("yen", Program.CreateMessage2());
        }
        
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual("ngoc", Program.CreateMessage3());
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual("nhi", Program.CreateMessage4());
        }
        
        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual("huong", Program.CreateMessage5());
        }
    }
}
