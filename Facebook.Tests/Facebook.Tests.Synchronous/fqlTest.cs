using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;
using System.Collections.Generic;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for fqlTest and is intended
	///to contain all fqlTest Unit Tests
	///</summary>
	[TestClass]
	public class fqlTest : Test
	{
		/// <summary>
		///A test for query
		///</summary>
		[TestMethod]
		public void queryTest()
		{
			var query = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_UserId + ")";
			var actual = _api.Fql.Query(query);
			Assert.IsNotNull(actual);
		}
        /// <summary>
        ///A test for query
        ///</summary>
        [TestMethod]
        public void queryTest2()
        {
            var query = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_UserId + ")";
            var actual = _apiWeb.Fql.Query(query);
            Assert.IsNotNull(actual);
        }

		/// <summary>
		/// A test for multiquery
		/// </summary>
		[TestMethod]
		public void multiqueryTest()
		{
			var query1 = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_UserId + ")";
			var query2 = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_friend1 + ")";
			var queries = new Dictionary<string, string>();
			queries.Add("firstQuery", query1);
			queries.Add("secondQuery", query2);
			var results = _api.Fql.Multiquery(queries);
			Assert.IsNotNull(results);
			Assert.IsTrue(results.Count == 2);
			Assert.IsTrue(results[0].name == "firstQuery");
		}
	}
}