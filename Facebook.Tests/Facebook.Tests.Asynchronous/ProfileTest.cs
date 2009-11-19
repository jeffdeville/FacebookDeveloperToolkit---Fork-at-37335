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
	public class ProfileTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for setFBML
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void setFBMLTest()
		{
			var uid = Constants.FBSamples_UserId;
			var profile = Constants.FBSamples_setFBML;
			var profile_main = Constants.FBSamples_setFBML;
			var mobile_profile = Constants.FBSamples_setFBML;
			_api.Profile.SetFBMLAsync(uid, profile, profile_main, mobile_profile, SetFBMLCompleted, null);
		}

		private void SetFBMLCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getFBML
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getFBMLTest()
		{
			var uid = Constants.FBSamples_UserId;
			var type = 2;			
			_api.Profile.GetFBMLAsync(uid, type, GetFBMLCompleted, null);
		}

		private void GetFBMLCompleted(string fbml, object state, FacebookException e)
		{
			Assert.IsTrue(fbml.Contains(Constants.FBSamples_setFBML));
			EnqueueTestComplete();
		}

		/// <summary>
		/// A test for setInfo
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void setInfoTest()
		{
			var item = new info_item();
			item.label = "Unit test info item";
			item.link = "www.claritycon.com";

			var field = new info_field();
			field.items = new info_fieldItems();
			field.items.info_item.Add(item);
			field.field = "Unit test field";
			_api.Profile.SetInfoAsync("Unit test title", 5, new List<info_field>() { field }, Constants.FBSamples_UserId, SetInfoCompleted, null);
		}

		private void SetInfoCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		/// A test for getInfo
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void getInfoTest()
		{
			_api.Profile.GetInfoAsync(Constants.FBSamples_UserId, GetInfoCompleted, null);
		}

		private void GetInfoCompleted(user_info userInfo, object state, FacebookException e)
		{
			Assert.IsNotNull(userInfo);
			Assert.IsTrue(userInfo.info_fields.info_field.Count > 0);
			Assert.IsTrue(userInfo.info_fields.info_field[0].field == "Unit test field");
			EnqueueTestComplete();
		}

		/// <summary>
		/// A test for setInfoOptions
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void setInfoOptionsTest()
		{
			var item1 = new info_item();
			item1.link = "www.claritycon.com";
			item1.label = "SetInfoOptions test 1";

			var item2 = new info_item();
			item2.link = "blogs.claritycon.com";
			item2.label = "SetInfoOptions test 2";

			_api.Profile.SetInfoOptionsAsync("SetInfoOptions Field", new List<info_item>() { item1, item2 }, SetInfoOptionsCompleted, null);
		}

		private void SetInfoOptionsCompleted(bool result, object state, FacebookException e)
		{
			EnqueueTestComplete();
			Assert.IsTrue(result);
		}

		/// <summary>
		/// A test for getInfoOptions
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void getInfoOptionsTest()
		{
			_api.Profile.GetInfoOptionsAsync("SetInfoOptions Field", GetInfoOptionsCompleted, null);
		}

		private void GetInfoOptionsCompleted(IList<info_item> infoItems, object state, FacebookException e)
		{
			Assert.IsNotNull(infoItems);
			Assert.IsTrue(infoItems.Count > 0);
			Assert.IsTrue(infoItems[0].label == "SetInfoOptions test 1");
			EnqueueTestComplete();
		}
	}
}
