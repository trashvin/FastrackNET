using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FASTProcess;

namespace FastrackUnitTest.ProcessTests
{
    [TestClass]
    public class EmployeeProcessTest
    {
        EmployeeProcess employeeProcess = new EmployeeProcess();

        [TestMethod]
        public void TestGetEmployeeByID()
        {
            int result = employeeProcess.GetEmployeeByID(332534).EmployeeID;

            Assert.AreEqual(result, 332534);
        }

        [TestMethod]
        public void TestGetEmployeeProfileByID()
        {
            int result = employeeProcess.GetEmployeeProfileByID(332534).EmployeeID;

            Assert.AreEqual(result, 332534);
        }
    }
}
