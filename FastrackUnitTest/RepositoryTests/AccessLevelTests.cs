using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.UnitsOfWork;
using Repository.Repositories;
using Common;


namespace FastrackUnitTest
{
    [TestClass]
    public class AccessLevelTests
    {
    
        GenericUnitOfWork<AccessLevel> unitOfwork =
            new GenericUnitOfWork<AccessLevel>();

        [TestInitialize]
        public void Init()
        {
            unitOfwork.Repository = new GenericRepository<AccessLevel>();
            

        }

        [TestMethod]
        public void TestGetAll()
        {
            
            int expected = 4;
            int actual = unitOfwork.Repository.GetAll().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetByID()
        {
            string expected = "Admin";

            string actual = unitOfwork.Repository.GetByID(1).AccessMode;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDelete()
        {
            AccessLevel access = new AccessLevel() { AccessMode = "Test", Description = "Test Desc" };
            AccessLevel newAccess = unitOfwork.Repository.Insert(access);
            unitOfwork.Save();

            //Assert.AreEqual(access.AccessMode, newAccess.AccessMode);

            AccessLevel deletedAccess = unitOfwork.Repository.Delete(newAccess.AccessLevelID);
            unitOfwork.Save();

            Assert.AreEqual(newAccess.AccessLevelID, deletedAccess.AccessLevelID);

        }
    }
}
