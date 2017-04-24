using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FASTProcess;
using Common;
using Provider.Cryptography;



namespace FastrackUnitTest.ProcessTests
{
    [TestClass]
    public class UserProcessTest
    {
        UserProcess _userProcess = new UserProcess();

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void TestRegistrationNotExist()
        {
            int expected = FASTConstant.RETURN_VAL_NOT_FOUND;
            int actual = _userProcess.RegisterUser(11222222);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestRegistrationExist()
        {
            int expected = FASTConstant.RETURN_VAL_DUPLICATE;
            int actual = _userProcess.RegisterUser(114560);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestRegistrationSuccess()
        {
            int expected = FASTConstant.RETURN_VAL_SUCCESS;
            int actual = _userProcess.RegisterUser(318387);

            Assert.AreEqual(expected, actual);

            Repository.UnitsOfWork.GenericUnitOfWork<Registration> reg =
                new Repository.UnitsOfWork.GenericUnitOfWork<Registration>();
            
            Registration result = reg.Repository.GetAllQueryable().Where(m => m.EmployeeID == 318387).First();

            result = reg.Repository.Delete(result);
            reg.Save();

        }

        [TestMethod]
        public void TestLogin()
        {
            int expected = FASTConstant.RETURN_VAL_SUCCESS;

            Repository.UnitsOfWork.GenericUnitOfWork<Registration> reg =
               new Repository.UnitsOfWork.GenericUnitOfWork<Registration>();

            Registration result = reg.Repository.GetAllQueryable().Where(m => m.EmployeeID == 114560).First();

            int actual = _userProcess.LoginUser(114560, result.Password);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestChangePassword()
        {
            string expected = MD5HashProvider.CreateMD5Hash("111111");
            string origPassword = MD5HashProvider.CreateMD5Hash("14560");
            Repository.UnitsOfWork.GenericUnitOfWork<Registration> reg =
               new Repository.UnitsOfWork.GenericUnitOfWork<Registration>();
            
            Registration result = reg.Repository.GetAllQueryable().Where(m => m.EmployeeID == 114560).First();

            int actual = _userProcess.ChangePassword(114560, expected, result.Password);
            
            
            Assert.AreEqual(FASTConstant.RETURN_VAL_SUCCESS,actual);

            _userProcess.ChangePassword(114560, origPassword, result.Password);


        }

    }
}
