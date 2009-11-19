using System.Collections.Generic;
using Facebook.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Session;
using Facebook.Rest;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for feedTest and is intended
	///to contain all feedTest Unit Tests
	///</summary>
	[TestClass]
	public class feedTest : Test
	{
        ///// <summary>
        /////A test for publishTemplatizedAction
        /////</summary>
        //[TestMethod]
        //[Deprecated]
        //public void publishTemplatizedActionTest1()
        //{
        //    var title_template = "{actor} reviewed the application {app}";
        //    var title_data = new Dictionary<string, string> {{"app", "Facebook Developer Toolkit"}};
        //    var body_template = "{app} has received a rating of {num_stars} from the users of BookApplication";
        //    var body_data = new Dictionary<string, string> {{"app", "Facebook Developer Toolkit"}, {"num_stars", "5"}};
        //    var body_general = "<fb:name uid{{=}}\"620749458\" firstnameonly{{=}}true /> said \"This app rocks.\"";
        //    var page_actor_id = 0;
        //    Collection<feed_image> images = null;
        //    Collection<string> target_ids = null;
        //    var expected = true;
        //    var actual = _api.Feed.PublishTemplatizedAction(title_template, title_data, body_template, body_data, body_general,
        //                                             page_actor_id, images, target_ids);
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        /////A test for publishTemplatizedAction
        /////</summary>
        //[TestMethod]
        //[Deprecated]
        //public void publishTemplatizedActionTest()
        //{
        //    var title_template = "{*actor*} reviewed the application {*app*}";
        //    Dictionary<string, string> title_data = new Dictionary<string, string> { { "app", "Facebook Developer Toolkit" } };
        //    var body_template = "{*app*} has received a rating of {*num_stars*} from the users of BookApplication";
        //    Dictionary<string, string> body_data = new Dictionary<string, string> { { "app", "Facebook Developer Toolkit" }, { "num_stars", "5" } };
        //    Collection<feed_image> images = null;
        //    var expected = true;
        //    var actual = _api.Feed.PublishTemplatizedAction(title_template, title_data, body_template, body_data, images);
        //    Assert.AreEqual(expected, actual);
        //}

        /// <summary>
        ///A test for publishUserAction
        ///</summary>
        [TestMethod]
        public void publishUserActionTest()
        {
            string oneLineStoryTemplate = "{*actor*} has been playing at {*host*}'s house.";
            string shortStoryTemplateTitle = "{*actor*} has been <a href='http://www.facebook.com/apps/application.php?id=xxx>Playing Poker!</a>";
            string shortStoryTemplateBody = "short story body from {*host*}'s house";
            string fullStoryTemplateTitle = "{*actor*} has been <a href='http://www.facebook.com/apps/application.php?id=xxx>Playing Poker!</a>";
            string fullStoryTemplateBody = "full story body from {*host*}'s house.";
            List<string> oneLineTemplates = new List<string> { oneLineStoryTemplate };
            feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
            List<feedTemplate> shortStoryTemplates = new List<feedTemplate> { shortStoryTemplate };
            feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };

            var templateBundleId = _api.Feed.RegisterTemplateBundle(oneLineTemplates, shortStoryTemplates, fullStoryTemplate);
            List<long> friendTargets = new List<long> { Constants.FBSamples_friend1, Constants.FBSamples_friend2};
            Dictionary<string, string> body_data = new Dictionary<string, string> { { "host", "Run" } };
            
            var actual = _api.Feed.PublishUserAction(templateBundleId, body_data, friendTargets, null, Feed.PublishedStorySize.Short);
            Assert.IsTrue(actual);
			
			// TODO: find a way to deactivate the template bundle just created, since calls to feed.deactivateTemplateBundleById don't seem to
			// work. The error returned says that only the owner of a desktop app can deactivate template bundles, but this user should be the
			// owner of this app. Currently, we have to periodically go in to the Registered Templates Console and deactivate a bunch of templates
			// so that we don't go over our limit.
        }

        /// <summary>
        ///A test for registerTemplateBundle, getRegisteredTemplateBundleByID and deactivateTemplateBundleByID
        ///</summary>
        [TestMethod()]
        public void registerGetByIdDeactivateTemplateBundleTest()
        {
			DeactivateAllBundles();
			long actual = RegisterNewBundle();
            template_bundle expected = _apiWeb.Feed.GetRegisteredTemplateBundleByID(actual);
            Assert.AreEqual(expected.template_bundle_id, actual);

            var deactivated = _apiWeb.Feed.DeactivateTemplateBundleByID(actual.ToString());
            Assert.IsTrue(deactivated);
        }

        /// <summary>
        ///A test for getRegisteredTemplateBundles
        ///</summary>
        [TestMethod()]
        public void getRegisteredTemplateBundlesTest()
        {
			RegisterNewBundle();
            var actual = _apiWeb.Feed.GetRegisteredTemplateBundles();
            Assert.IsTrue(actual.Count > 0);
			DeactivateAllBundles();
        }

		private void DeactivateAllBundles()
		{
			var bundles = _apiWeb.Feed.GetRegisteredTemplateBundles();

			foreach (var b in bundles)
			{
				_apiWeb.Feed.DeactivateTemplateBundleByID(b.template_bundle_id.ToString());
			}
		}

		private long RegisterNewBundle()
		{
			string oneLineStoryTemplate = "{*actor*} has been playing.";
			string shortStoryTemplateTitle = "{*actor*} has been <a href='http://www.facebook.com/apps/application.php?id=xxx>Playing Poker!</a>";
			string shortStoryTemplateBody = "short story body";
			string fullStoryTemplateTitle = "{*actor*} has been <a href='http://www.facebook.com/apps/application.php?id=xxx>Playing Poker!</a>";
			string fullStoryTemplateBody = "full story body";
			List<string> oneLineTemplates = new List<string> { oneLineStoryTemplate };
			feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
			List<feedTemplate> shortStoryTemplates = new List<feedTemplate> { shortStoryTemplate };
			feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };

			return _apiWeb.Feed.RegisterTemplateBundle(oneLineTemplates, shortStoryTemplates, fullStoryTemplate);
		}
	}
}