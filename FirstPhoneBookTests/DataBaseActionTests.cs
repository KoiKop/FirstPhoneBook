using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstPhoneBookTests
{
    [TestClass]
    public class DataBaseActionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestsHelper testsHelper = new TestsHelper();

            testsHelper.SetupPhoneBookContentTestsTable();
            testsHelper.DropPhoneBookContentTestsTable();
        }
    }
}
