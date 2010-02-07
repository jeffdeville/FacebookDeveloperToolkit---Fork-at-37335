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
	public class BatchTest : AsyncFacebookTest
	{
		private object _lock = new object();

		private bool _firstMethodReturned;

		/// <summary>
		///A test for executeBatch
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void executeBatchTest()
		{
			_firstMethodReturned = false;
			_api.Batch.BeginBatch();
			_api.Friends.GetAsync(GetFriendsCompleted, null);
			_api.Events.GetAsync(GetEventsCompleted, null);
			_api.Batch.ExecuteBatchAsync(true);
		}

		private void GetFriendsCompleted(IList<long> friendIds, object state, FacebookException e)
		{
			Assert.IsNotNull(friendIds);
			CheckEndTest();
		}

		private void GetEventsCompleted(IList<facebookevent> events, object state, FacebookException e)
		{
			Assert.IsNotNull(events);
			CheckEndTest();
		}

		private void CheckEndTest()
		{
			lock (_lock)
			{
				if (_firstMethodReturned)
				{
					EnqueueTestComplete();
				}
				else
				{
					_firstMethodReturned = true;
				}
			}
		}
	}
}
