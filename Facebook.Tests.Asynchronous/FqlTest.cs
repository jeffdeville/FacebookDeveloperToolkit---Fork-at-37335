using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Facebook.Rest;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System.Collections.Generic;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class FqlTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for query
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void queryTest()
		{
			var query = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_UserId + ")";
			_api.Fql.QueryAsync(query, QueryCompleted, null);
		}

		private void QueryCompleted(string result, object state, FacebookException e)
		{
			Assert.IsNotNull(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for query
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void queryTest2()
		{
			var query = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_UserId + ")";
			_apiWeb.Fql.QueryAsync(query, Query2Completed, null);
		}

		private void Query2Completed(string result, object state, FacebookException e)
		{
			Assert.IsNotNull(result);
			EnqueueTestComplete();
		}

		/// <summary>
		/// A test for multiquery
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void multiqueryTest()
		{
			var query1 = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_UserId + ")";
			var query2 = "SELECT uid, name FROM user WHERE uid IN (" + Constants.FBSamples_friend1 + ")";
			var queries = new Dictionary<string, string>();
			queries.Add("firstQuery", query1);
			queries.Add("secondQuery", query2);
			_api.Fql.MultiqueryAsync(queries, MultiqueryCompleted, null);
		}

		private void MultiqueryCompleted(IList<fql_result> results, object state, FacebookException e)
		{
			Assert.IsNotNull(results);
			Assert.IsTrue(results.Count == 2);
			Assert.IsTrue(results[0].name == "firstQuery");
			EnqueueTestComplete();
		}
	}
}
