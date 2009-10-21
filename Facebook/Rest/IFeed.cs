using System;
namespace Facebook.Rest
{
	public interface IFeed : IRestBase
	{
		bool DeactivateTemplateBundleByID(string template_bundle_id);
		void DeactivateTemplateBundleByIDAsync(string template_bundle_id, Feed.DeactivateTemplateBundleByIDCallback callback, object state);
		Facebook.Schema.template_bundle GetRegisteredTemplateBundleByID(long template_bundle_id);
		void GetRegisteredTemplateBundleByIDAsync(long template_bundle_id, Feed.GetRegisteredTemplateBundleByIDCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.template_bundle> GetRegisteredTemplateBundles();
		void GetRegisteredTemplateBundlesAsync(Feed.GetRegisteredTemplateBundlesCallback callback, object state);
		bool PublishTemplatizedAction(string title_template, System.Collections.Generic.Dictionary<string, string> title_data, string body_template, System.Collections.Generic.Dictionary<string, string> body_data, System.Collections.ObjectModel.Collection<Facebook.Schema.feed_image> images);
		bool PublishTemplatizedAction(string title_template, System.Collections.Generic.Dictionary<string, string> title_data, string body_template, System.Collections.Generic.Dictionary<string, string> body_data, string body_general, int page_actor_id, System.Collections.ObjectModel.Collection<Facebook.Schema.feed_image> images, System.Collections.ObjectModel.Collection<string> target_ids);
		void PublishTemplatizedActionAsync(string title_template, System.Collections.Generic.Dictionary<string, string> title_data, string body_template, System.Collections.Generic.Dictionary<string, string> body_data, System.Collections.ObjectModel.Collection<Facebook.Schema.feed_image> images, Feed.PublishTemplatizedActionCallback callback, object state);
		void PublishTemplatizedActionAsync(string title_template, System.Collections.Generic.Dictionary<string, string> title_data, string body_template, System.Collections.Generic.Dictionary<string, string> body_data, string body_general, int page_actor_id, System.Collections.ObjectModel.Collection<Facebook.Schema.feed_image> images, System.Collections.ObjectModel.Collection<string> target_ids, Feed.PublishTemplatizedActionCallback callback, object state);
		bool PublishUserAction(long template_bundle_id, System.Collections.Generic.Dictionary<string, string> template_data, System.Collections.Generic.List<long> target_ids, string body_general, Feed.PublishedStorySize story_size);
		void PublishUserActionAsync(long template_bundle_id, System.Collections.Generic.Dictionary<string, string> template_data, System.Collections.Generic.List<long> target_ids, string body_general, Feed.PublishedStorySize story_size, Feed.PublishUserActionCallback callback, object state);
		long RegisterTemplateBundle(System.Collections.Generic.List<string> oneLineStoryTemplates, System.Collections.Generic.List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate);
		long RegisterTemplateBundle(System.Collections.Generic.List<string> oneLineStoryTemplates, System.Collections.Generic.List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate, System.Collections.Generic.IList<Facebook.Schema.action_link> actionLinks);
		void RegisterTemplateBundleAsync(System.Collections.Generic.List<string> oneLineStoryTemplates, System.Collections.Generic.List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate, Feed.RegisterTemplateBundleCallback callback, object state);
		void RegisterTemplateBundleAsync(System.Collections.Generic.List<string> oneLineStoryTemplates, System.Collections.Generic.List<feedTemplate> shortStoryTemplates, feedTemplate fullStoryTemplate, System.Collections.Generic.IList<Facebook.Schema.action_link> actionLinks, Feed.RegisterTemplateBundleCallback callback, object state);
	}
}
