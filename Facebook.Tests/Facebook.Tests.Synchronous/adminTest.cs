using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Facebook.Schema;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    /// <summary>
    ///This is a test class for adminTest and is intended
    ///to contain all adminTest Unit Tests
    ///</summary>
    [TestClass()]
    public class adminTest : Test
    {
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for getAppProperties
        ///</summary>
        [TestMethod()]
        public void getAppPropertiesTest()
        {
            var expectedDict = new Dictionary<string, string>();

            foreach(var prop in _apiWeb.Admin.GetApplicationPropertyNames())
            {
                expectedDict.Add(prop, null);
            }

            var resultDict = _apiWeb.Admin.GetAppProperties(_apiWeb.Admin.GetApplicationPropertyNames());
            Assert.AreEqual(expectedDict.Count, resultDict.Count);
            Assert.IsNotNull(resultDict);
        }

        /// <summary>
        ///A test for setAppProperties
        ///</summary>
        [TestMethod()]
        public void setAppPropertiesTest()
        {
            bool expected = true; 
            bool actual;
            var dict = new Dictionary<string, string>
            {
                {"application_name","Sample IFrame App"}
            };
            actual = _apiWeb.Admin.SetAppProperties(dict);
            var props = _apiWeb.Admin.GetAppProperties(_apiWeb.Admin.GetApplicationPropertyNames());
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(props);
            
        }

        /// <summary>
        ///A test for getMetrics
        ///</summary>
        [TestMethod()]
        public void getMetricsTest()
        {
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
            DateTime endDate = DateTime.Now; 
            Admin.Period period = Admin.Period.Day; 
            IList<metrics> actual;
            actual = this._apiWeb.Admin.GetMetrics(startDate, endDate, period);
            Assert.IsTrue(actual.Count>0);
        }

        /// <summary>
        ///A test for getAllocation
        ///</summary>
        [TestMethod()]
        public void getAllocationTest()
        {
            Admin.IntegrationPointName name = Admin.IntegrationPointName.requests_per_day; // TODO: Initialize to an appropriate value
            int actual;
            actual = _apiWeb.Admin.GetAllocation(name);
            Assert.IsTrue(actual>0);
            
        }

        /// <summary>
        ///A test for getRestrictionInfo
        ///</summary>
        [TestMethod()]
        public void getRestrictionInfoTest()
        {
            string actual = _apiWeb.Admin.GetRestrictionInfo();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for setRestrictionInfo
        ///</summary>
        [TestMethod()]
        public void setRestrictionInfoTest()
        {
            const bool expected = true;
            var dict = new Dictionary<string, string>
            {
                {"age",Constants.AdminTestAgeRestriction}
            };
            bool actual = _apiWeb.Admin.SetRestrictionInfo(dict);
            var resultInfo = _apiWeb.Admin.GetRestrictionInfo();
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(resultInfo);
        }

        /// <summary>
        ///A test for getBannedUsers
        ///</summary>
        [TestMethod()]
        public void getBannedUsersTest()
        {
            var existingBannedUsers = _apiWeb.Admin.GetBannedUsers();
            Assert.IsNotNull(existingBannedUsers);
        }

        /// <summary>
        ///A test for banUsers
        ///</summary>
        [TestMethod()]
        public void banUsersTest()
        {
            List<long> expectedBannedUsers = Constants.FBSamples_uids;
            bool actual = _apiWeb.Admin.BanUsers(expectedBannedUsers);
            var actualBannedUsers = _apiWeb.Admin.GetBannedUsers();
            Assert.IsTrue(actual);
            Assert.AreEqual(actualBannedUsers.Count, expectedBannedUsers.Count);

            // Teardown
            Assert.IsTrue(_apiWeb.Admin.UnbanUsers(expectedBannedUsers), "Unable to teardown (unban) users.");
        }

        /// <summary>
        ///A test for unbanUsers
        ///</summary>
        [TestMethod()]
        public void unbanUsersTest()
        {
            List<long> bannedUsers = Constants.FBSamples_uids;
            Assert.IsTrue(_apiWeb.Admin.BanUsers(bannedUsers), "Unable to initialize (ban) users.");
            bool actual = _apiWeb.Admin.UnbanUsers(bannedUsers);
            var actualBannedUsers = _apiWeb.Admin.GetBannedUsers();
            Assert.IsTrue(actual);
            Assert.AreEqual(actualBannedUsers.Count, 0);
        }
    }
}
