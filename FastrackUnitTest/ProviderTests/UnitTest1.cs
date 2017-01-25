using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Provider.Cryptography;
using log4net;
using log4net.Config;

namespace FastrackUnitTest.ProviderTests
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly ILog tracer = LogManager.GetLogger(typeof(UnitTest1));
        [TestMethod]
        public void TestMethod1()
        {
            tracer.Debug("Test1111");
            tracer.Error("Error1111");
            tracer.Info("Infor00000");
            string s = BasicCrypto.EncryptString("Test");
            Assert.Fail();

        }
    }
}
