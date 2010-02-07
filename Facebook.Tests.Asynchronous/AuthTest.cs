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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Facebook.Rest;
using Facebook.Schema;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class AuthTest : AsyncFacebookTest
	{
		[TestMethod]
		[Asynchronous]
		public void CreateTokenTest()
		{
			_apiWeb.Auth.CreateTokenAsync(OnCreateTokenCompleted, null);
		}

		private void OnCreateTokenCompleted(string result, object state, FacebookException e)
		{
			Assert.IsNotNull(result);
			Assert.AreNotEqual(result, string.Empty);
			EnqueueTestComplete();
		}
		
		/// <summary>
        ///A test for getSession
        ///</summary>
		[TestMethod]
		[Asynchronous]
		public void GetSessionTest()
		{
			// TODO: implement this test (sync version below)
			EnqueueTestComplete();

            //var authToken = _apiWeb.Auth.CreateToken();
            // TODO: Get Session giving invalid parameter error.  May be affected by login process requirements.
            //var result = _apiWeb.Auth.GetSession(authToken);
            //Assert.IsNotNull(result);
            //Assert.AreNotEqual(result, string.Empty);
        }

		/// <summary>
		///A test for promoteSession
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void PromoteSessionTest()
		{
			_api.Auth.PromoteSessionAsync(PromoteSessionCompleted, null);			
		}

		private void PromoteSessionCompleted(string result, object state, FacebookException e)
		{
			Assert.IsNotNull(result);
			EnqueueTestComplete();
		}

		// TODO: find a better way to test this method, since currently it revokes this app's authorization for this user, and we have to redo all of the auth
		// and permissions before we can run any more tests
		///// <summary>
		/////A test for revokeAuthorization
		/////</summary>
		//[TestMethod]
		//[Asynchronous]
		//public void RevokeAuthorizationTest()
		//{
		//    _apiWeb.Auth.RevokeAuthorizationAsync(RevokeAuthorizationCompleted, null);
		//}

		//private void RevokeAuthorizationCompleted(bool result, object state, FacebookException e)
		//{
		//    Assert.IsTrue(result);
		//    EnqueueTestComplete();
		//}

		/// <summary>
		///A test for revokeExtendedPermission
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void RevokeExtendedPermissionTest()
		{
			_apiWeb.Auth.RevokeExtendedPermissionAsync(Enums.ExtendedPermissions.sms, RevokeExtendedPermissionCompleted, null);
		}

		private void RevokeExtendedPermissionCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}
