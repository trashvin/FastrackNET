using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Repository;
using Common;
using FASTProcess;
using Provider.Cryptography;

namespace FastrackUnitTest.RepositoryTests
{
    [TestClass]
    public class SettingsTest
    {
        [TestMethod]
        public void TestBasicCrypto()
        {
            GenericProcess<Configuration> configProcess =
                new GenericProcess<Configuration>();

            Configuration newConfig = configProcess.GetAll().Where(i => i.Name == "MailPassword").FirstOrDefault();

            Assert.AreEqual("Fujitsu.1351", BasicCrypto.DecryptString(newConfig.Value));
        }

        [TestMethod]
        public void TestUpdateActiveEmail()
        {
            GenericProcess<Configuration> configProcess =
                new GenericProcess<Configuration>();

            Configuration newConfig = configProcess.GetAll().Where(i => i.Name == "ActiveEmail").FirstOrDefault();

            newConfig.Value = "3";

            int result = configProcess.Update(newConfig);

            Assert.AreEqual(FASTConstant.RETURN_VAL_SUCCESS, result);


        }


    }
}
