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
	public class FeedTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for publishUserAction
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void publishUserActionTest()
		{
			DeactivateAllBundles(ContinuePublishUserActionTest);
		}

		private void ContinuePublishUserActionTest()
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

			_api.Feed.RegisterTemplateBundleAsync(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, OnRegisterForPublishUserActionCompleted, null);
		}

		private void OnRegisterForPublishUserActionCompleted(long templateBundleId, object state, FacebookException e)
		{
			List<long> friendTargets = new List<long> { Constants.FBSamples_friend1, Constants.FBSamples_friend2 };
			Dictionary<string, string> body_data = new Dictionary<string, string> { { "host", "Run" } };

			_api.Feed.PublishUserActionAsync(templateBundleId, body_data, friendTargets, null, Feed.PublishedStorySize.Short, OnPublishUserActionCompleted, null);
		}

		private void OnPublishUserActionCompleted(bool actual, object state, FacebookException ex)
		{
			Assert.IsTrue(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for registerTemplateBundle and getRegisteredTemplateBundleByID
		///</summary>
		[TestMethod()]
		[Asynchronous]
		//[DeploymentItem("facebook.dll")]
		public void registerGetByIdTemplateBundleTest()
		{
			DeactivateAllBundles(ContinueRegisterGetTest);
		}

		private void ContinueRegisterGetTest()
		{
			List<string> oneLineTemplates;
			List<feedTemplate> shortStoryTemplates;
			feedTemplate fullStoryTemplate;
			ConstructTestTemplates(out oneLineTemplates, out shortStoryTemplates, out fullStoryTemplate);
			_api.Feed.RegisterTemplateBundleAsync(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, OnRegisterForRegisterGetTestCompleted, null);			
		}

		private void OnRegisterForRegisterGetTestCompleted(long actual, object state, FacebookException e)
		{
			_api.Feed.GetRegisteredTemplateBundleByIDAsync(actual, OnGetForRegisterGetTestCompleted, actual);			
		}

		private void OnGetForRegisterGetTestCompleted(template_bundle expected, object state, FacebookException e)
		{
			var actual = (long)state;
			Assert.AreEqual(expected.template_bundle_id, actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for registerTemplateBundle, getRegisteredTemplateBundleByID and deactivateTemplateBundleByID
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void registerGetByIdDeactivateTemplateBundleTest()
		{
			DeactivateAllBundles(ContinueRegisterGetDeactivateTest);
		}

		private void ContinueRegisterGetDeactivateTest()
		{
			List<string> oneLineTemplates;
			List<feedTemplate> shortStoryTemplates;
			feedTemplate fullStoryTemplate;
			ConstructTestTemplates(out oneLineTemplates, out shortStoryTemplates, out fullStoryTemplate);
			_apiWeb.Feed.RegisterTemplateBundleAsync(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, OnRegisterForDeactivateCompleted, null);
		}

		private void OnRegisterForDeactivateCompleted(long actual, object state, FacebookException e)
		{
			_apiWeb.Feed.GetRegisteredTemplateBundleByIDAsync(actual, OnGetForDeactivateCompleted, actual);
		}

		private void OnGetForDeactivateCompleted(template_bundle expected, object state, FacebookException e)
		{
			var actual = (long)state;
			Assert.AreEqual(expected.template_bundle_id, actual);

			_apiWeb.Feed.DeactivateTemplateBundleByIDAsync(actual.ToString(), OnDeactivateCompleted, null);			
		}

		private void OnDeactivateCompleted(bool deactivated, object state, FacebookException e)
		{
			Assert.IsTrue(deactivated);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getRegisteredTemplateBundles
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getRegisteredTemplateBundlesTest()
		{
			DeactivateAllBundles(ContinueGetBundlesTest);
		}

		private void ContinueGetBundlesTest()
		{
			List<string> oneLineTemplates;
			List<feedTemplate> shortStoryTemplates;
			feedTemplate fullStoryTemplate;
			ConstructTestTemplates(out oneLineTemplates, out shortStoryTemplates, out fullStoryTemplate);
			_apiWeb.Feed.RegisterTemplateBundleAsync(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, OnRegisterForGetBundlesCompleted, null);
		}

		private void OnRegisterForGetBundlesCompleted(long bundleId, object state, FacebookException e)
		{
			_apiWeb.Feed.GetRegisteredTemplateBundlesAsync(OnGetRegisteredTemplateBundlesCompleted, null);
		}

		private void OnGetRegisteredTemplateBundlesCompleted(IList<template_bundle> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}

		private void DeactivateAllBundles(FeedTestCallback callback)
		{
			_apiWeb.Feed.GetRegisteredTemplateBundlesAsync(ContinueDeactivatingBundles, callback);
		}

		private void ContinueDeactivatingBundles(IList<template_bundle> bundles, object state, FacebookException e)
		{
			var callback = (FeedTestCallback)state;

			if (bundles.Count == 0)
			{
				callback();
				return;
			}

			var nextBundle = bundles[0];
			bundles.Remove(nextBundle);
			Facebook.Rest.Feed.DeactivateTemplateBundleByIDCallback deactivateCallback = (result, state2, e2) => ContinueDeactivatingBundles(bundles, state, e);
			_apiWeb.Feed.DeactivateTemplateBundleByIDAsync(nextBundle.template_bundle_id.ToString(), deactivateCallback, null);
		}

		private delegate void FeedTestCallback();

		private void ConstructTestTemplates(out List<string> oneLineTemplates, out List<feedTemplate> shortStoryTemplates, out feedTemplate fullStoryTemplate)
		{
			string oneLineStoryTemplate = "{*actor*} has been playing.";
			string shortStoryTemplateTitle = "{*actor*} has been <a href='http://www.facebook.com/apps/application.php?id=xxx>Playing Poker!</a>";
			string shortStoryTemplateBody = "short story body";
			string fullStoryTemplateTitle = "{*actor*} has been <a href='http://www.facebook.com/apps/application.php?id=xxx>Playing Poker!</a>";
			string fullStoryTemplateBody = "full story body";
			oneLineTemplates = new List<string> { oneLineStoryTemplate };
			feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
			shortStoryTemplates = new List<feedTemplate> { shortStoryTemplate };
			fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
		}
	}
}
