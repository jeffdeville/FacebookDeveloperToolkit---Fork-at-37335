using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;
using Facebook.Schema;
using System.Collections.Generic;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for profileTest and is intended
	///to contain all profileTest Unit Tests
	///</summary>
	[TestClass]
	public class profileTest : Test
	{
		/// <summary>
		///A test for setFBML
		///</summary>
		[TestMethod]
		public void setFBMLTest()
		{
			var uid = Constants.FBSamples_UserId;
			var profile = Constants.FBSamples_setFBML;
			var profile_main = Constants.FBSamples_setFBML;
			var mobile_profile = Constants.FBSamples_setFBML;
			var expected = true;
			var actual = _api.Profile.SetFBML(uid, profile, profile_main, mobile_profile);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for getFBML
		///</summary>
		[TestMethod]
		public void getFBMLTest()
		{
			var uid = Constants.FBSamples_UserId;
			var type = 2;
			var expected = Constants.FBSamples_setFBML;
            var actual = _api.Profile.GetFBML(uid, type);
			Assert.IsTrue(actual.Contains(expected));
		}

		/// <summary>
		/// A test for setInfo
		/// </summary>
		[TestMethod]
		public void setInfoTest()
		{
			var item = new info_item();
			item.label = "Unit test info item";
			item.link = "www.claritycon.com";

			var field = new info_field();
			field.items = new info_fieldItems();
			field.items.info_item.Add(item);
			field.field = "Unit test field";
			var response = _api.Profile.SetInfo("Unit test title", 5, new List<info_field>(){ field }, Constants.FBSamples_UserId);
			Assert.IsTrue(response);
		}

		/// <summary>
		/// A test for getInfo
		/// </summary>
		[TestMethod]
		public void getInfoTest()
		{
			var userInfo = _api.Profile.GetInfo(Constants.FBSamples_UserId);
			Assert.IsNotNull(userInfo);
			Assert.IsTrue(userInfo.info_fields.info_field.Count > 0);
			Assert.IsTrue(userInfo.info_fields.info_field[0].field == "Unit test field");
		}

		/// <summary>
		/// A test for setInfoOptions
		/// </summary>
		[TestMethod]
		public void setInfoOptionsTest()
		{
			var item1 = new info_item();
			item1.link = "www.claritycon.com";
			item1.label = "SetInfoOptions test 1";

			var item2 = new info_item();
			item2.link = "blogs.claritycon.com";
			item2.label = "SetInfoOptions test 2";

			var response = _api.Profile.SetInfoOptions("SetInfoOptions Field", new List<info_item>() { item1, item2 });
			Assert.IsTrue(response);
		}

		/// <summary>
		/// A test for getInfoOptions
		/// </summary>
		[TestMethod]
		public void getInfoOptionsTest()
		{
			var infoItems = _api.Profile.GetInfoOptions("SetInfoOptions Field");
			Assert.IsNotNull(infoItems);
			Assert.IsTrue(infoItems.Count > 0);
			Assert.IsTrue(infoItems[0].label == "SetInfoOptions test 1");
		}
	}
}