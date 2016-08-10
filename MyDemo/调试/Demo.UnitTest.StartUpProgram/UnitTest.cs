using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demo.Net.StartUpProgram;
using System.IO;
namespace Demo.UnitTest.StartUpProgram
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestConsole.exe");
            Demo.Net.StartUpProgram.StartUpProgram startUp = new Net.StartUpProgram.StartUpProgram();
            // Assert.IsTrue(startUp.Begin(path), "成功!");
            Demo.Net.StartUpProgram.StartUpProgram.WinExec(path, 3);
            var count = startUp.GetProgramCount();
            Assert.IsTrue(true, count.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            Demo.Net.ClickBombbox.ClickBomb.Click();
            Assert.IsTrue(true, "成功");
        }
    }
}
