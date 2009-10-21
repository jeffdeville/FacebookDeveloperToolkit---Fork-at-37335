using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Feed API methods.
	/// </summary>
	public class Feed : RestBase, Facebook.Rest.IFeed
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Feed
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Feed(FacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Publishes a Mini-Feed story to the Facebook Page corresponding to the page_actor_id parameter.
        /// </summary>
        /// <param name="title_template">The templatized markup displayed in the feed story's title section. This template must contain the token {actor} somewhere in it.</param>
        /// <param name="title_data">Optional - A collection of the values that should be substituted into the templates in the title_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special tokens and should not be included in this array. If your title_template contains tokens besides 'actor' and 'target', then this is a required parameter.</param>
        /// <param name="body_template">Optional - The markup displayed in the feed story's body section.</param>
        /// <param name="body_data">Optional - A collection of the values that should be substituted into the templates in the body_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special token and should not be included in this array.</param>
        /// <param name="images">Optional - A collection of images to be displayed in the Feed story. Similar to body_general, the image displayed is not aggregated -- the image from any one of the aggregated stories may be displayed.</param>
        /// <returns>The function returns true on success, false on permissions error, or an error response.</returns>
        /// <remarks>This method is deprecated for actions taken by users only; it still works for actions taken by Facebook Pages.</remarks>
        public bool PublishTemplatizedAction(string title_template, Dictionary<string, string> title_data,
											   string body_template, Dictionary<string, string> body_data,
											   Collection<feed_image> images)
		{
			return PublishTemplatizedAction(title_template, title_data, body_template, body_data, null, 0, images, null);
		}

        /// <summary>
        /// Publishes a Mini-Feed story to the Facebook Page corresponding to the page_actor_id parameter.
        /// </summary>
        /// <param name="title_template">The templatized markup displayed in the feed story's title section. This template must contain the token {actor} somewhere in it.</param>
        /// <param name="title_data">Optional - A collection of the values that should be substituted into the templates in the title_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special tokens and should not be included in this array. If your title_template contains tokens besides 'actor' and 'target', then this is a required parameter.</param>
        /// <param name="body_template">Optional - The markup displayed in the feed story's body section.</param>
        /// <param name="body_data">Optional - A collection of the values that should be substituted into the templates in the body_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special token and should not be included in this array.</param>
        /// <param name="body_general">Optional - Additional markup displayed in the feed story's body section. This markup is not required to be identical for two stories to be aggregated. One of the two will be chosen at random.</param>
        /// <param name="page_actor_id">Optional - if publishing a story to a Facebook Page, use this parameter as the page who performed the action. If you use this parameter, the application must be added to that Page's Feed. A session key is not required to do this.</param>
        /// <param name="images">Optional - A collection of images to be displayed in the Feed story. Similar to body_general, the image displayed is not aggregated -- the image from any one of the aggregated stories may be displayed.</param>
        /// <param name="target_ids">Optional - A collection of IDs of friends of the actor, used for stories about a direct action between the actor and these targets of his/her action. This is required if either the title_template or body_template includes the token {target}.</param>
        /// <returns>The function returns true on success, false on permissions error, or an error response.</returns>
        /// <remarks>This method is deprecated for actions taken by users only; it still works for actions taken by Facebook Pages.</remarks>
        public bool PublishTemplatizedAction(string title_template, Dictionary<string, string> title_data,
									   string body_template, Dictionary<string, string> body_data,
									   string body_general, int page_actor_id,
									   Collection<feed_image> images, Collection<string> target_ids)
		{
			return PublishTemplatizedAction(title_template, title_data, body_template, body_data, body_general, page_actor_id,
									images, target_ids, false, null, null);
		}

        /// <summary>
        /// Deactivates a previously registered template bundle.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Feed.DeactivateTemplateBundleByID(Constants.TemplateBundleId);
        /// </code>
        /// </example>
        /// <param name="template_bundle_id">The template bundle ID used to identify a previously registered template bundle. The ID is the one returned by a previous call to Feed.RegisterTemplateBundle.</param>
        /// <returns>This method returns true if the specified template was deactivated.</returns>
		public bool DeactivateTemplateBundleByID(string template_bundle_id)
		{
			return DeactivateTemplateBundleByID(template_bundle_id, false, null, null);
		}

        /// <summary>
        /// Retrieves information about a specified template bundle previously registered by the requesting application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Feed.GetRegisteredTemplateBundleByID(long.Parse(Constants.TemplateBundleId));
        /// </code>
        /// </example>
        /// <param name="template_bundle_id">The template bundle ID used to identify a previously registered template bundle. The ID is the one returned by a previous call to Feed.RegisterTemplateBundle.</param>
        /// <returns>This method returns a template_bundle containing information on the specified template bundle ID.</returns>
        public template_bundle GetRegisteredTemplateBundleByID(long template_bundle_id)
		{
			return GetRegisteredTemplateBundleByID(template_bundle_id, false, null, null);
		}

        /// <summary>
        /// Retrieves the full list of all the template bundles registered by the requesting application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Feed.GetRegisteredTemplateBundles();
        /// </code>
        /// </example>
        /// <returns>This method returns a List of template_bundles.</returns>
        public IList<template_bundle> GetRegisteredTemplateBundles()
		{
			return GetRegisteredTemplateBundles(false, null, null);
		}

        /// <summary>
        /// Builds a template bundle around the specified templates, registers them on Facebook, and responds with a template bundle ID that can be used to identify your template bundle to other Feed-related API calls.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///
        /// string oneLineStoryTemplate = "{*actor*} has been playing.";
        /// string shortStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        /// string shortStoryTemplateBody = "short story body";
        /// string fullStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        /// string fullStoryTemplateBody = "full story body";
        /// List&lt;string&gt; oneLineTemplates = new List&lt;string&gt; { oneLineStoryTemplate };
        /// feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
        /// List&lt;feedTemplate&gt; shortStoryTemplates = new List&lt;feedTemplate&gt; { shortStoryTemplate };
        /// feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
        ///
        /// var result = api.Feed.RegisterTemplateBundle(oneLineTemplates, shortStoryTemplates, fullStoryTemplate);
        /// </code>
        /// </example>
        /// <param name="oneLineStoryTemplates">array containing one FBML template that can be used to render one line Feed stories</param>
        /// <param name="shortStoryTemplates">Array of short story templates</param>
        /// <param name="fullStoryTemplate">template for a single full story</param>
        /// <returns>This method returns an identifier that the developer can use to publish actual stories using that template bundle.</returns>
		public long RegisterTemplateBundle(List<string> oneLineStoryTemplates, List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate)
		{
			return RegisterTemplateBundle(oneLineStoryTemplates, shortStoryTemplates, fullStoryTemplate, null);
		}

        /// <summary>
        /// Builds a template bundle around the specified templates, registers them on Facebook, and responds with a template bundle ID that can be used to identify your template bundle to other Feed-related API calls.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///
        /// string oneLineStoryTemplate = "{*actor*} has been playing.";
        /// string shortStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        /// string shortStoryTemplateBody = "short story body";
        /// string fullStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        /// string fullStoryTemplateBody = "full story body";
        /// List&lt;string&gt; oneLineTemplates = new List&lt;string&gt; { oneLineStoryTemplate };
        /// feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
        /// List&lt;feedTemplate&gt; shortStoryTemplates = new List&lt;feedTemplate&gt; { shortStoryTemplate };
        /// feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
        /// List&lt;action_link&gt; actionLinks = new List&lt;action_link&gt;();
        /// actionLinks.Add(new action_link() { href = "http://www.facebook.com", text = "facebook link" });
        ///
        /// var result = api.Feed.RegisterTemplateBundle(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, actionLinks);
        /// </code>
        /// </example>
        /// <param name="oneLineStoryTemplates">array containing one FBML template that can be used to render one line Feed stories</param>
        /// <param name="shortStoryTemplates">Array of short story templates</param>
        /// <param name="fullStoryTemplate">template for a single full story</param>
        /// <param name="actionLinks">Array of action link records</param>
        /// <returns>This method returns an identifier that the developer can use to publish actual stories using that template bundle.</returns>
        public long RegisterTemplateBundle(List<string> oneLineStoryTemplates, List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate, IList<action_link> actionLinks)
		{
			return RegisterTemplateBundle(oneLineStoryTemplates, shortStoryTemplates, fullStoryTemplate, actionLinks, false, null, null);
		}

        /// <summary>
        /// Publishes a story on behalf of the user owning the session, using the specified template bundle.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// string oneLineStoryTemplate = "{*actor*} is at {*host*}'s house.";
        /// string shortStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing&lt;/a&gt;";
        /// string shortStoryTemplateBody = "short story body from {*host*}'s house";
        /// string fullStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing&lt;/a&gt;";
        /// string fullStoryTemplateBody = "full story body from {*host*}'s house.";
        /// List&lt;string&gt; oneLineTemplates = new List&lt;string&gt; { oneLineStoryTemplate };
        /// feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
        /// List&lt;feedTemplate&gt; shortStoryTemplates = new List&lt;feedTemplate&gt; { shortStoryTemplate };
        /// feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
        /// var templateBundleId = api.Feed.RegisterTemplateBundle(oneLineTemplates, shortStoryTemplates, fullStoryTemplate);
        /// List&lt;long&gt; friendTargets = new List&lt;long&gt; { Constants.Friend_UserId1, Constants.Friend_UserId2 };
        /// Dictionary&lt;string, string&gt; body_data = new Dictionary&lt;string, string&gt; { { "host", "Run" } };
        /// var result = api.Feed.PublishUserAction(templateBundleId, body_data, friendTargets, null, Feed.PublishedStorySize.Short);
        /// </code> 
        /// </example>
        /// <param name="template_bundle_id">The template bundle ID used to identify a previously registered template bundle. The ID is the one returned by a previous call to feed.registerTemplateBundle or when you registered the bundle using the Feed Template Console.</param>
        /// <param name="template_data">A collection of the values that should be substituted into the templates held by the specified template bundle. For information on forming the template_data object, see Facebook API Documentation.</param>
        /// <param name="target_ids">A list of IDs of friends of the actor, used for stories about a direct action between the actor and the targets of his or her action. This parameter is required if one or more templates in the template bundle makes use of the {*target*} token. It should only include the IDs of friends of the actor, and it should not contain the actor's ID.</param>
        /// <param name="body_general">Additional markup that extends the body of a short story.</param>
        /// <param name="story_size">The size of the Feed story.  The one line story is the default, and users have to opt into using short stories at time of publication or through their privacy settings. Otherwise, if the user has not allowed that particular size to be published through the API, then the story size will be demoted to match the user's preference.</param>
        /// <returns>The function returns true on success or an error response.</returns>
        /// <remarks>
        /// http://wiki.developers.facebook.com/index.php/Feed.publishUserAction
        /// Publishes a story on behalf of the user owning the session, using the specified template bundle. 
        /// An application can publish a maximum of 10 stories per user per day
        /// You can test your Feed templates using the Feed preview console (cf above wiki post).
        /// 
        /// Use JSONHelper.ConvertToJSONArray and/or JSONHelper.ConvertToJSONAssociativeArray to add 'subarrays' in template_data
        /// 
        /// Reserved tokens in template_data: 
        ///     actor
        ///     target
        ///     
        /// Special tokens in template_data:
        ///     images: array of image. image: src, (optional)href
        ///     flash: swfsrc, imgsrc, (optional)expanded_width, (optional)expanded_height
        ///     mp3: src, (optional)title, (optional)artist, (optional)album
        ///     video: video_src, preview_img, (optional)video_title, (optional)video_link, (optional)video_type (default:application/x-shockwave-flash)
        /// </remarks>
        public bool PublishUserAction(long template_bundle_id, Dictionary<string, string> template_data, List<long> target_ids, string body_general, PublishedStorySize story_size)
		{
			return PublishUserAction(template_bundle_id, template_data, target_ids, body_general, story_size, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Publishes a Mini-Feed story to the Facebook Page corresponding to the page_actor_id parameter.
        /// </summary>
        /// <param name="title_template">The templatized markup displayed in the feed story's title section. This template must contain the token {actor} somewhere in it.</param>
        /// <param name="title_data">Optional - A collection of the values that should be substituted into the templates in the title_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special tokens and should not be included in this array. If your title_template contains tokens besides 'actor' and 'target', then this is a required parameter.</param>
        /// <param name="body_template">Optional - The markup displayed in the feed story's body section.</param>
        /// <param name="body_data">Optional - A collection of the values that should be substituted into the templates in the body_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special token and should not be included in this array.</param>
        /// <param name="images">Optional - A collection of images to be displayed in the Feed story. Similar to body_general, the image displayed is not aggregated -- the image from any one of the aggregated stories may be displayed.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The function returns true on success, false on permissions error, or an error response.</returns>
        /// <remarks>This method is deprecated for actions taken by users only; it still works for actions taken by Facebook Pages.</remarks>
        public void PublishTemplatizedActionAsync(string title_template, Dictionary<string, string> title_data,
											   string body_template, Dictionary<string, string> body_data,
											   Collection<feed_image> images, PublishTemplatizedActionCallback callback,
			                                   Object state)
		{
			PublishTemplatizedActionAsync(title_template, title_data, body_template, body_data, null, 0, images, null, callback, state);
		}

        /// <summary>
        /// Publishes a Mini-Feed story to the Facebook Page corresponding to the page_actor_id parameter.
        /// </summary>
        /// <param name="title_template">The templatized markup displayed in the feed story's title section. This template must contain the token {actor} somewhere in it.</param>
        /// <param name="title_data">Optional - A collection of the values that should be substituted into the templates in the title_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special tokens and should not be included in this array. If your title_template contains tokens besides 'actor' and 'target', then this is a required parameter.</param>
        /// <param name="body_template">Optional - The markup displayed in the feed story's body section.</param>
        /// <param name="body_data">Optional - A collection of the values that should be substituted into the templates in the body_template markup string. The keys of this array are the tokens, and their associated values are the desired substitutions. 'actor' and 'target' are special token and should not be included in this array.</param>
        /// <param name="body_general">Optional - Additional markup displayed in the feed story's body section. This markup is not required to be identical for two stories to be aggregated. One of the two will be chosen at random.</param>
        /// <param name="page_actor_id">Optional - if publishing a story to a Facebook Page, use this parameter as the page who performed the action. If you use this parameter, the application must be added to that Page's Feed. A session key is not required to do this.</param>
        /// <param name="images">Optional - A collection of images to be displayed in the Feed story. Similar to body_general, the image displayed is not aggregated -- the image from any one of the aggregated stories may be displayed.</param>
        /// <param name="target_ids">Optional - A collection of IDs of friends of the actor, used for stories about a direct action between the actor and these targets of his/her action. This is required if either the title_template or body_template includes the token {target}.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The function returns true on success, false on permissions error, or an error response.</returns>
        /// <remarks>This method is deprecated for actions taken by users only; it still works for actions taken by Facebook Pages.</remarks>
        public void PublishTemplatizedActionAsync(string title_template, Dictionary<string, string> title_data,
									   string body_template, Dictionary<string, string> body_data,
									   string body_general, int page_actor_id,
									   Collection<feed_image> images, Collection<string> target_ids,
									   PublishTemplatizedActionCallback callback, Object state)
		{
			PublishTemplatizedAction(title_template, title_data, body_template, body_data, body_general, page_actor_id,
									images, target_ids, true, callback, state);
		}
        
        /// <summary>
        /// Deactivates a previously registered template bundle.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Feed.DeactivateTemplateBundleByIDAsync(Constants.TemplateBundleId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="template_bundle_id">The template bundle ID used to identify a previously registered template bundle. The ID is the one returned by a previous call to Feed.RegisterTemplateBundle.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the specified template was deactivated.</returns>
        public void DeactivateTemplateBundleByIDAsync(string template_bundle_id, DeactivateTemplateBundleByIDCallback callback, Object state)
		{
            DeactivateTemplateBundleByID(template_bundle_id, true, callback, state);
		}

        /// <summary>
        /// Retrieves information about a specified template bundle previously registered by the requesting application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Feed.GetRegisteredTemplateBundleByIDAsync(long.Parse(Constants.TemplateBundleId), AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(template_bundle result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="template_bundle_id">The template bundle ID used to identify a previously registered template bundle. The ID is the one returned by a previous call to Feed.RegisterTemplateBundle.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a template_bundle containing information on the specified template bundle ID.</returns>
        public void GetRegisteredTemplateBundleByIDAsync(long template_bundle_id, GetRegisteredTemplateBundleByIDCallback callback, Object state)
		{
            GetRegisteredTemplateBundleByID(template_bundle_id, true, callback, state);
		}

        /// <summary>
        /// Retrieves the full list of all the template bundles registered by the requesting application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Feed.GetRegisteredTemplateBundlesAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;template_bundle&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List of template_bundles.</returns>
        public void GetRegisteredTemplateBundlesAsync(GetRegisteredTemplateBundlesCallback callback, Object state)
		{
			GetRegisteredTemplateBundles(true, callback, state);
		}
        
		/// <summary>
        /// Publishes a story on behalf of the user owning the session, using the specified template bundle.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     string oneLineStoryTemplate = "{*actor*} is at {*host*}'s house.";
        ///     string shortStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing&lt;/a&gt;";
        ///     string shortStoryTemplateBody = "short story body from {*host*}'s house";
        ///     string fullStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing&lt;/a&gt;";
        ///     string fullStoryTemplateBody = "full story body from {*host*}'s house.";
        ///     List&lt;string&gt; oneLineTemplates = new List&lt;string&gt; { oneLineStoryTemplate };
        ///     feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
        ///     List&lt;feedTemplate&gt; shortStoryTemplates = new List&lt;feedTemplate&gt; { shortStoryTemplate };
        ///     feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
        ///     var templateBundleId = api.Feed.RegisterTemplateBundle(oneLineTemplates, shortStoryTemplates, fullStoryTemplate);
        ///     List&lt;long&gt; friendTargets = new List&lt;long&gt; { Constants.Friend_UserId1, Constants.Friend_UserId2 };
        ///     Dictionary&lt;string, string&gt; body_data = new Dictionary&lt;string, string&gt; { { "host", "Run" } };
        ///     api.Feed.PublishUserActionAsync(templateBundleId, body_data, friendTargets, null, Feed.PublishedStorySize.Short, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="template_bundle_id">The template bundle ID used to identify a previously registered template bundle. The ID is the one returned by a previous call to feed.registerTemplateBundle or when you registered the bundle using the Feed Template Console.</param>
        /// <param name="template_data">A collection of the values that should be substituted into the templates held by the specified template bundle. For information on forming the template_data object, see Facebook API Documentation.</param>
        /// <param name="target_ids">A list of IDs of friends of the actor, used for stories about a direct action between the actor and the targets of his or her action. This parameter is required if one or more templates in the template bundle makes use of the {*target*} token. It should only include the IDs of friends of the actor, and it should not contain the actor's ID.</param>
        /// <param name="body_general">Additional markup that extends the body of a short story.</param>
        /// <param name="story_size">The size of the Feed story.  The one line story is the default, and users have to opt into using short stories at time of publication or through their privacy settings. Otherwise, if the user has not allowed that particular size to be published through the API, then the story size will be demoted to match the user's preference.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>The function returns true on success or an error response.</returns>
        /// <remarks>
        /// http://wiki.developers.facebook.com/index.php/Feed.publishUserAction
        /// Publishes a story on behalf of the user owning the session, using the specified template bundle. 
        /// An application can publish a maximum of 10 stories per user per day
        /// You can test your Feed templates using the Feed preview console (cf above wiki post).
        /// 
        /// Use JSONHelper.ConvertToJSONArray and/or JSONHelper.ConvertToJSONAssociativeArray to add 'subarrays' in template_data
        /// 
        /// Reserved tokens in template_data: 
        ///     actor
        ///     target
        ///     
        /// Special tokens in template_data:
        ///     images: array of image. image: src, (optional)href
        ///     flash: swfsrc, imgsrc, (optional)expanded_width, (optional)expanded_height
        ///     mp3: src, (optional)title, (optional)artist, (optional)album
        ///     video: video_src, preview_img, (optional)video_title, (optional)video_link, (optional)video_type (default:application/x-shockwave-flash)
        /// </remarks>
        public void PublishUserActionAsync(long template_bundle_id, Dictionary<string, string> template_data, List<long> target_ids, string body_general, PublishedStorySize story_size, PublishUserActionCallback callback, Object state)
		{
            PublishUserAction(template_bundle_id, template_data, target_ids, body_general, story_size, true, callback, state);
		}

        /// <summary>
        /// Builds a template bundle around the specified templates, registers them on Facebook, and responds with a template bundle ID that can be used to identify your template bundle to other Feed-related API calls.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///
        ///     string oneLineStoryTemplate = "{*actor*} has been playing.";
        ///     string shortStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        ///     string shortStoryTemplateBody = "short story body";
        ///     string fullStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        ///     string fullStoryTemplateBody = "full story body";
        ///     List&lt;string&gt; oneLineTemplates = new List&lt;string&gt; { oneLineStoryTemplate };
        ///     feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
        ///     List&lt;feedTemplate&gt; shortStoryTemplates = new List&lt;feedTemplate&gt; { shortStoryTemplate };
        ///     feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
        ///
        ///     api.Feed.RegisterTemplateBundleAsync(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(long result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="oneLineStoryTemplates">array containing one FBML template that can be used to render one line Feed stories</param>
        /// <param name="shortStoryTemplates">Array of short story templates</param>
        /// <param name="fullStoryTemplate">template for a single full story</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an identifier that the developer can use to publish actual stories using that template bundle.</returns>
        public void RegisterTemplateBundleAsync(List<string> oneLineStoryTemplates, List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate, RegisterTemplateBundleCallback callback, Object state)
		{
			RegisterTemplateBundleAsync(oneLineStoryTemplates, shortStoryTemplates, fullStoryTemplate, null, callback, state);
		}

        /// <summary>
        /// Builds a template bundle around the specified templates, registers them on Facebook, and responds with a template bundle ID that can be used to identify your template bundle to other Feed-related API calls.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///
        ///     string oneLineStoryTemplate = "{*actor*} has been playing.";
        ///     string shortStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        ///     string shortStoryTemplateBody = "short story body";
        ///     string fullStoryTemplateTitle = "{*actor*} has been &lt;a href='http://www.facebook.com/apps/application.php?id=xxx&gt;testing!&lt;/a&gt;";
        ///     string fullStoryTemplateBody = "full story body";
        ///     List&lt;string&gt; oneLineTemplates = new List&lt;string&gt; { oneLineStoryTemplate };
        ///     feedTemplate shortStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = shortStoryTemplateBody, TemplateTitle = shortStoryTemplateTitle };
        ///     List&lt;feedTemplate&gt; shortStoryTemplates = new List&lt;feedTemplate&gt; { shortStoryTemplate };
        ///     feedTemplate fullStoryTemplate = new feedTemplate { PreferredLayout = "1", TemplateBody = fullStoryTemplateBody, TemplateTitle = fullStoryTemplateTitle };
        ///     List&lt;action_link&gt; actionLinks = new List&lt;action_link&gt;();
        ///     actionLinks.Add(new action_link() { href = "http://www.facebook.com", text = "facebook link"});
        ///
        ///     api.Feed.RegisterTemplateBundleAsync(oneLineTemplates, shortStoryTemplates, fullStoryTemplate, actionLinks, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(long result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="oneLineStoryTemplates">array containing one FBML template that can be used to render one line Feed stories</param>
        /// <param name="shortStoryTemplates">Array of short story templates</param>
        /// <param name="fullStoryTemplate">template for a single full story</param>
        /// <param name="actionLinks">Array of action link records</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an identifier that the developer can use to publish actual stories using that template bundle.</returns>
        public void RegisterTemplateBundleAsync(List<string> oneLineStoryTemplates, List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate,
			IList<action_link> actionLinks, RegisterTemplateBundleCallback callback, Object state)
		{
			RegisterTemplateBundle(oneLineStoryTemplates, shortStoryTemplates, fullStoryTemplate, actionLinks, true, callback, state);
		}

		#endregion Asynchronous Methods

		/// <summary>
		/// Adds image, image link key value pairs to the request parameter collection.
		/// </summary>
		/// <param name="dict">Request paramenter collection.</param>
		/// <param name="images">A collection of images.</param>
		public static void AddFeedImages(IDictionary<string, string> dict, IEnumerable<feed_image> images)
		{
			if (Equals(images, null)) return;
			var i = 0;
			foreach (var image in images)
			{
				if (string.IsNullOrEmpty(image.image_url)) continue;
				dict.Add("image_" + (i + 1), image.image_url);
				dict.Add("image_" + (i + 1) + "_link", image.image_link_url);
				i++;
			}
		}

		#endregion Public Methods
        
		#region Private Methods

		private bool PublishTemplatizedAction(string title_template, Dictionary<string, string> title_data,
											   string body_template, Dictionary<string, string> body_data,
											   string body_general, int page_actor_id,
											   Collection<feed_image> images, Collection<string> target_ids,
											   bool isAsync, PublishTemplatizedActionCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.feed.publishTemplatizedAction" } };
			Utilities.AddRequiredParameter(parameterList, "title_template", title_template);
			Utilities.AddOptionalParameter(parameterList, "page_actor_id", page_actor_id);
			Utilities.AddJSONAssociativeArray(parameterList, "title_data", title_data);
			Utilities.AddJSONAssociativeArray(parameterList, "body_data", body_data);
			Utilities.AddOptionalParameter(parameterList, "body_template", body_template);
			Utilities.AddOptionalParameter(parameterList, "body_general", body_general);
			Utilities.AddCollection(parameterList, "target_ids", target_ids);
			AddFeedImages(parameterList, images);

			if (isAsync)
			{
                SendRequestAsync<feed_publishTemplatizedAction_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

            var response = SendRequest<feed_publishTemplatizedAction_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		private bool DeactivateTemplateBundleByID(string template_bundle_id, bool isAsync, DeactivateTemplateBundleByIDCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.feed.deactivateTemplateBundleByID" } };
			Utilities.AddRequiredParameter(parameterList, "template_bundle_id", template_bundle_id);

			if (isAsync)
			{
                SendRequestAsync<feed_deactivateTemplateBundleByID_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

            var response = SendRequest<feed_deactivateTemplateBundleByID_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		private template_bundle GetRegisteredTemplateBundleByID(long template_bundle_id, bool isAsync, GetRegisteredTemplateBundleByIDCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.feed.getRegisteredTemplateBundleByID" } };
			Utilities.AddRequiredParameter(parameterList, "template_bundle_id", template_bundle_id);

			if (isAsync)
			{
                SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<feed_getRegisteredTemplateBundleByID_response>(callback), state);
				return null;
			}

            return SendRequest<feed_getRegisteredTemplateBundleByID_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
		}

		private IList<template_bundle> GetRegisteredTemplateBundles(bool isAsync, GetRegisteredTemplateBundlesCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.feed.getRegisteredTemplateBundles" } };

			if (isAsync)
			{
                SendRequestAsync<feed_getRegisteredTemplateBundles_response, IList<template_bundle>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<template_bundle>>(callback), state, "template_bundle");
				return null;
			}

            var response = SendRequest<feed_getRegisteredTemplateBundles_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.template_bundle;
		}

		private long RegisterTemplateBundle(List<string> oneLineStoryTemplates, List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate, IList<action_link> actionLinks, bool isAsync, RegisterTemplateBundleCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.feed.registerTemplateBundle" } };

			Utilities.AddJSONArray(parameterList, "one_line_story_templates", oneLineStoryTemplates);

			var list = new List<string>();
			foreach (var item in shortStoryTemplates)
			{
				var dict = new Dictionary<string, string>{
                    {"template_title", item.TemplateTitle},
                    {"template_body", item.TemplateBody},
                    {"preferred_layout", item.PreferredLayout}
                };
				list.Add(JSONHelper.ConvertToJSONAssociativeArray(dict));
			}
			Utilities.AddJSONArray(parameterList, "short_story_templates", list);

			if (actionLinks != null)
			{
				var alist = new List<string>();
				foreach (action_link al in actionLinks)
				{
					var dict = new Dictionary<string, string>{
								{"text", al.text},
								{"href", al.href}
							};
					alist.Add(JSONHelper.ConvertToJSONAssociativeArray(dict));
				}
				Utilities.AddJSONArray(parameterList, "action_links", alist);
			}

			var full_story_template = new Dictionary<string, string>();
			full_story_template.Add("template_title", fullStoryTemplate.TemplateTitle);
			full_story_template.Add("template_body", fullStoryTemplate.TemplateBody);
			Utilities.AddJSONAssociativeArray(parameterList, "full_story_template", full_story_template);

			if (isAsync)
			{
                SendRequestAsync<feed_registerTemplateBundle_response, long>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<long>(callback), state);
				return 0;
			}

            var response = SendRequest<feed_registerTemplateBundle_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? 0 : response.TypedValue;
		}

		private bool PublishUserAction(long template_bundle_id, Dictionary<string, string> template_data, List<long> target_ids, string body_general, PublishedStorySize story_size, bool isAsync, PublishUserActionCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.feed.publishUserAction" } };
			Utilities.AddRequiredParameter(parameterList, "template_bundle_id", template_bundle_id);
			Utilities.AddJSONAssociativeArray(parameterList, "template_data", template_data);
			Utilities.AddList(parameterList, "target_ids", target_ids);
			Utilities.AddOptionalParameter(parameterList, "body_general", body_general);
			Utilities.AddOptionalParameter(parameterList, "story_size", (int)story_size);

			if (isAsync)
			{
				SendRequestAsync<feed_publishUserAction_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state, "feed_publishUserAction_response_elt");
				return true;
			}

			var response = SendRequest<feed_publishUserAction_response>(parameterList);
			return response == null ? true : response.feed_publishUserAction_response_elt;
		}

		#endregion Private Methods
        
        #endregion Methods

		#region Delegates

        /// <summary>
        /// Callback for async call to PublishTemplatizedAction
        /// </summary>
        public delegate void PublishTemplatizedActionCallback(bool result, Object state, FacebookException e);
        /// <summary>
        /// Callback for async call to DeactivateTemplateBundleByID
        /// </summary>
        public delegate void DeactivateTemplateBundleByIDCallback(bool result, Object state, FacebookException e);
        /// <summary>
        /// Callback for async call to GetRegisteredTemplateBundleByID
        /// </summary>
        public delegate void GetRegisteredTemplateBundleByIDCallback(template_bundle bundle, Object state, FacebookException e);
        /// <summary>
        /// Callback for async call to GetRegisteredTemplateBundles
        /// </summary>
        public delegate void GetRegisteredTemplateBundlesCallback(IList<template_bundle> bundles, Object state, FacebookException e);
        /// <summary>
        /// Callback for async call to RegisterTemplateBundle
        /// </summary>
        public delegate void RegisterTemplateBundleCallback(long template_id, Object state, FacebookException e);
        /// <summary>
        /// Callback for async call to PublishUserAction
        /// </summary>
        public delegate void PublishUserActionCallback(bool result, Object state, FacebookException e);

		#endregion Delegates

		#region Enums

		/// <summary>
		/// Different story sizes to use when publishing user actions to their feed.
		/// </summary>
		public enum PublishedStorySize
		{
            /// <summary>
            /// OneLine = 1
            /// </summary>
            OneLine = 1,
            /// <summary>
            /// Short = 2
            /// </summary>
            Short = 2,
            /// <summary>
            /// Full = 4
            /// </summary>
            Full = 4
		}

		#endregion
	}

	/// <summary>
	/// Contains the different parts of a Facebook feed template.
	/// </summary>
	public class feedTemplate
	{
		/// <summary>
		/// The title of the template
		/// </summary>
		public string TemplateTitle { get; set; }

		/// <summary>
		/// The body of the template.
		/// </summary>
		public string TemplateBody { get; set; }

		/// <summary>
		/// The preferred layout for the template.
		/// </summary>
		public string PreferredLayout { get; set; }

	}
}
