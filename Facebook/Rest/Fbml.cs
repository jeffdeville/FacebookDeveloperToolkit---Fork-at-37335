using System;
using System.Collections.Generic;
using System.Globalization;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Fbml API methods.
    /// </summary>
    public class Fbml : BaseAuthenticatedService, Facebook.Rest.IFbml
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Fbml
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Fbml(IFacebookNetworkWrapper networkWrapper, IFacebookSession session)
            : base(networkWrapper, session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT

        #region Synchronous Methods

        /// <summary>
        /// Fetches and re-caches the image stored at the given URL.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var url = "http://facebook.claritycon.com/Tests/Clarity.jpg";
        /// var results = api.Fbml.RefreshImgSrc(url);
        /// </code>
        /// </example>
        /// <param name="url">The absolute URL from which to refresh the image.</param>
        /// <returns>This method returns 1 if Facebook found a cached version of your image and successfully refreshed the image. It returns a blank response if Facebook was unable to find any previously cached version to refresh, or the image was unable to be re-fetched from your site and cached successfully. In such instances, whatever images were cached before remain as they were cached.</returns>
        public bool RefreshImgSrc(string url)
		{
            return RefreshImgSrc(url, false, null, null);
		}

        /// <summary>
        /// Fetches and re-caches the content stored at the given URL.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var url = "http://facebook.claritycon.com/Tests/FBML.html";
        /// var results = api.Fbml.RefreshRefUrl(url);
        /// </code>
        /// </example>
        /// <param name="url">The absolute URL from which to fetch content. This URL should be used in a fb:ref FBML tag.</param>
        /// <returns>This method returns true if the content was fetched and re-cached from the specified URL.</returns>
        public bool RefreshRefUrl(string url)
        {
            return RefreshRefUrl(url, false, null, null);
		}

        /// <summary>
        /// Associates a given "handle" with FBML markup so that the handle can be used within the fb:ref FBML tag.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// string handle = "my handle";
        /// string fbml = "dummy fbml";
        /// var results = api.Fbml.SetRefHandle(handle, fbml);
        /// </code>
        /// </example>
        /// <param name="handle">The handle to associate with the given FBML.</param>
        /// <param name="fbml">The FBML to associate with the given handle.</param>
        /// <returns>This method returns true if the given handle was associated with FBML markup.</returns>
        public bool SetRefHandle(string handle, string fbml)
        {
            return SetRefHandle(handle, fbml, false, null, null);
		}

        /// <summary>
        /// Lets you insert text strings into the Facebook Translations database so they can be translated.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var native_strings = new Dictionary&lt;string, string&gt; { { "text", "(Testing uploadNativeStrings) Do you want to add a friend?" }, { "description", "text string in a popup dialog" } };
        /// var results = api.Fbml.UploadNativeStrings(native_strings);
        /// </code>
        /// </example>
        /// <param name="native_strings">A collection of strings to translate. Each element of the collection is an object, with 'text' storing the actual string, 'description' storing the description of the text.</param>
        /// <returns>If successful, this method returns the number of strings uploaded.</returns>
        public bool UploadNativeStrings(Dictionary<string, string> native_strings)
        {
            return UploadNativeStrings(native_strings, false, null, null);
		}

        /// <summary>
        /// Deletes one or more custom tags you previously registered for the calling application with fbml.registerCustomTags.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;string&gt; tags = new List&lt;string&gt; { "video", "gallery" };
        /// var results = api.Fbml.DeleteCustomTags(tags);
        /// </code>
        /// </example>
        /// <param name="names">A List of strings containing the names of the tags you want to delete. If this attribute is missing, all the application's custom tags will be deleted.</param>
        /// <returns>This method returns true if custom tag deletes were successful.</returns>
        public bool DeleteCustomTags(List<string> names)
        {
            return DeleteCustomTags(names, false, null, null);
        }

        /// <summary>
        /// Returns the custom tag definitions for tags that were previously defined using fbml.registerCustomTags.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var results = api.Fbml.GetCustomTags();
        /// </code>
        /// </example>
        /// <returns>This method returns the custom tag definitions for previously defined tags.</returns>
        public IList<custom_tag> GetCustomTags()
        {
            return GetCustomTags(null);
        }

        /// <summary>
        /// Returns the custom tag definitions for tags that were previously defined using fbml.registerCustomTags.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var results = api.Fbml.GetCustomTags(Constants.OtherApplicationId);
        /// </code>
        /// </example>
        /// <param name="app_id">The ID of the application whose custom tags you want to get. If the ID is the calling application's ID, all the application's custom tags are returned. Otherwise, only the application's public custom tags are returned</param>
        /// <returns>This method returns the custom tag definitions for previously defined tags.</returns>
        public IList<custom_tag> GetCustomTags(string app_id)
        {
            return GetCustomTags(app_id, false, null, null);
        }

        /// <summary>
        /// Registers custom tags you can include in your that applications' FBML markup. Custom tags consist of FBML snippets that are rendered during parse time on the containing page that references the custom tag.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// List&lt;CustomTag&gt; tags = new List&lt;CustomTag&gt;();
        /// tags.Add(new CustomTag()
        ///     {
        ///         Name="video",
        ///         Type="leaf",
        ///         Description="Renders a fb:swf tag that shows a video from my-video-site.tv. The video  is 425 pixels wide and 344 pixels tall.",
        ///         Attributes=new List&lt;CustomTagAttribute&gt;(){new CustomTagAttribute(){Name="id",Description="the id of the video",DefaultValue="1234"}},
        ///         FBML="&lt;div class=\"my_videos_element\"&gt;&lt;fb:swf swfsrc=\"http://my-video-site.tv/videos/${id}\" width=\"425\" height=\"344\"/&gt;&lt;/div&gt;",
        ///         HeaderFBML="&lt;style&gt;div.my_videos_element { border: black solid 1px; padding: 5px;}&lt;/style&gt;"
        ///     });
        ///
        /// tags.Add(new CustomTag()
        ///     {
        ///         Name = "gallery",
        ///         Type = "container",
        ///         Description = "Renders a standard header and footer around one or more \"video\" tags. The header contains the gallery's title, which the user can specify",
        ///         Attributes = new List&lt;CustomTagAttribute&gt;() { new CustomTagAttribute() { Name = "title", Description = "the title of the gallery" } },
        ///         OpenTagFBML = "&lt;div class=\"my_videos_element\"&gt;&lt;div class=\"video_gallery_title\"&gt;${title}&lt;/div&gt;&lt;div class=\"my_videos_gallery\"&gt;",
        ///         CloseTagFBML = "&lt;/div&gt;&lt;/div&gt;",
        ///         HeaderFBML = "&lt;style&gt;div.my_videos_element { border: black solid 1px; padding: 5px;}&lt;/style&gt;"
        ///     });
        /// 
        /// var results = api.Fbml.RegisterCustomTags(tags);
        /// </code>
        /// </example>
        /// <param name="tags">a List of CustomTag objects (See remarks for more detail.)</param>
        /// <returns>This method returns the identifier of the custom tag.</returns>
        /// <remarks>
        /// Each tag object is an object with the following properties:
        ///   name (required) (string): the name of the tag. The name must be a string up to 30 characters. Only letters, numbers, underscores ('_') and hyphens ('-') are allowed.
        ///   type (optional) (string): Specify either leaf or container. Leaf tags can't contain any other tags (similar to &lt;fb:name/&gt;). Container tags may contain children between their open and close tags (like &lt;fb:editor&gt; &lt;/fb:editor&gt;). (Default value is leaf.).
        ///   description (optional) (string): A full description of the tag's functionality. This is used for documentation only, and is especially useful for public tags.
        ///   is_public (optional) (string): Specify either true or false. Specifying true indicates that other applications can use this tag. You can have a mix of public and private tags within the same array. (Default value is false.).
        ///   attributes (optional) (mixed): A list of attribute objects. Attributes are used to add dynamic elements to tags. The values of those attributes are substituted into the tag's FBML before it's parsed. Each attribute has the following fields:
        ///   name (required) (string): The attribute's name. The name must be a string up to 30 characters in length. Only letters, numbers, underscores ('_') and hyphens ('-') are allowed.
        ///   description (optional) (string): The attribute's description. This is used for documentation only, and is especially useful for public tags.
        ///   default_value (optional) (string): The value to use when the attribute is missing. If an attribute doesn't have a default value, it is considered to be required and the developer will see an error message if the attribute is missing.
        ///   fbml (required) (string): The FBML markup to substitute into the page where the tag is encountered. This property is required only for leaf tags.
        ///   open_tag_fbml (required) (string): The FBML markup to substitute into the page where the open tag appears. This property is required for container tags only.
        ///   close_tag_fbml (required) (string): The FBML markup to substitute into the page where the close tag appears. This property is required for container tags only. Note: Facebook recommends you do not include &lt;script&gt; tags in this FBML snippet.
        ///   header_fbml: An FBML snippet that is rendered once on the page before the first tag that defined it. If multiple tags define the same value for header_fbml, and more than one of them appear on a page, then header_fbml is rendered only once. You should only use this for including CSS and initializing any JavaScript variables, not for rendering visible content. Facebook recommends you avoid including heavy JavaScript libraries and performing expensive JavaScript operations in header_fbml for performance reasons. Instead, use footer_fbml.
        ///   footer_fbml: Similar to header_fbml, it's an FBML snippet that gets rendered after all custom tags are rendered. Facebook recommends you include heavy JavaScript libraries and perform any expensive JavaScript operations in footer_fbml, and avoid them in fbml, open_tag_fbml, close_tag_fbml, and header_fbml.
        /// </remarks>
        public int RegisterCustomTags(List<CustomTag> tags)
        {
            return RegisterCustomTags(tags, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Deletes one or more custom tags you previously registered for the calling application with fbml.registerCustomTags.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     List&lt;string&gt; tags = new List&lt;string&gt; { "video", "gallery" };
        ///     api.Fbml.DeleteCustomTagsAsync(tags, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="names">A List of strings containing the names of the tags you want to delete. If this attribute is missing, all the application's custom tags will be deleted.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if custom tag deletes were successful.</returns>
        public void DeleteCustomTagsAsync(List<string> names, DeleteCustomTagsCallback callback, Object state)
        {
            DeleteCustomTags(names, true, callback, state);
        }

        /// <summary>
        /// Returns the custom tag definitions for tags that were previously defined using fbml.registerCustomTags.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Fbml.GetCustomTagsAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;custom_tag&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the custom tag definitions for previously defined tags.</returns>
        public void GetCustomTagsAsync(GetCustomTagsCallback callback, Object state)
		{
			GetCustomTagsAsync(null, callback, state);
		}

        /// <summary>
        /// Returns the custom tag definitions for tags that were previously defined using fbml.registerCustomTags.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Fbml.GetCustomTagsAsync(Constants.OtherApplicationId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;custom_tag&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="app_id">The ID of the application whose custom tags you want to get. If the ID is the calling application's ID, all the application's custom tags are returned. Otherwise, only the application's public custom tags are returned</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the custom tag definitions for previously defined tags.</returns>
        public void GetCustomTagsAsync(string app_id, GetCustomTagsCallback callback, Object state)
        {
            GetCustomTags(app_id, true, callback, state);
        }
        
        /// <summary>
        /// Registers custom tags you can include in your that applications' FBML markup. Custom tags consist of FBML snippets that are rendered during parse time on the containing page that references the custom tag.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///
        ///     List&lt;CustomTag&gt; tags = new List&lt;CustomTag&gt;();
        ///     tags.Add(new CustomTag()
        ///     {
        ///         Name = "video",
        ///         Type = "leaf",
        ///         Description = "Renders a fb:swf tag that shows a video from my-video-site.tv. The video  is 425 pixels wide and 344 pixels tall.",
        ///         Attributes = new List&lt;CustomTagAttribute&gt;() { new CustomTagAttribute() { Name = "id", Description = "the id of the video", DefaultValue = "1234" } },
        ///         FBML = "&lt;div class=\"my_videos_element\"&gt;&lt;fb:swf swfsrc=\"http://my-video-site.tv/videos/${id}\" width=\"425\" height=\"344\"/&gt;&lt;/div&gt;",
        ///         HeaderFBML = "&lt;style&gt;div.my_videos_element { border: black solid 1px; padding: 5px;}&lt;/style&gt;"
        ///     });
        ///
        ///     tags.Add(new CustomTag()
        ///     {
        ///         Name = "gallery",
        ///         Type = "container",
        ///         Description = "Renders a standard header and footer around one or more \"video\" tags. The header contains the gallery's title, which the user can specify",
        ///         Attributes = new List&lt;CustomTagAttribute&gt;() { new CustomTagAttribute() { Name = "title", Description = "the title of the gallery" } },
        ///         OpenTagFBML = "&lt;div class=\"my_videos_element\"&gt;&lt;div class=\"video_gallery_title\"&gt;${title}&lt;/div&gt;&lt;div class=\"my_videos_gallery\"&gt;",
        ///         CloseTagFBML = "&lt;/div&gt;&lt;/div&gt;",
        ///         HeaderFBML = "&lt;style&gt;div.my_videos_element { border: black solid 1px; padding: 5px;}&lt;/style&gt;"
        ///     });
        ///
        ///     api.Fbml.RegisterCustomTagsAsync(tags, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(int result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="tags">a List of CustomTag objects (See remarks for more detail.)</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the identifier of the custom tag.</returns>
        /// <remarks>
        /// Each tag object is an object with the following properties:
        ///   name (required) (string): the name of the tag. The name must be a string up to 30 characters. Only letters, numbers, underscores ('_') and hyphens ('-') are allowed.
        ///   type (optional) (string): Specify either leaf or container. Leaf tags can't contain any other tags (similar to &lt;fb:name/&gt;). Container tags may contain children between their open and close tags (like &lt;fb:editor&gt; &lt;/fb:editor&gt;). (Default value is leaf.).
        ///   description (optional) (string): A full description of the tag's functionality. This is used for documentation only, and is especially useful for public tags.
        ///   is_public (optional) (string): Specify either true or false. Specifying true indicates that other applications can use this tag. You can have a mix of public and private tags within the same array. (Default value is false.).
        ///   attributes (optional) (mixed): A list of attribute objects. Attributes are used to add dynamic elements to tags. The values of those attributes are substituted into the tag's FBML before it's parsed. Each attribute has the following fields:
        ///   name (required) (string): The attribute's name. The name must be a string up to 30 characters in length. Only letters, numbers, underscores ('_') and hyphens ('-') are allowed.
        ///   description (optional) (string): The attribute's description. This is used for documentation only, and is especially useful for public tags.
        ///   default_value (optional) (string): The value to use when the attribute is missing. If an attribute doesn't have a default value, it is considered to be required and the developer will see an error message if the attribute is missing.
        ///   fbml (required) (string): The FBML markup to substitute into the page where the tag is encountered. This property is required only for leaf tags.
        ///   open_tag_fbml (required) (string): The FBML markup to substitute into the page where the open tag appears. This property is required for container tags only.
        ///   close_tag_fbml (required) (string): The FBML markup to substitute into the page where the close tag appears. This property is required for container tags only. Note: Facebook recommends you do not include &lt;script&gt; tags in this FBML snippet.
        ///   header_fbml: An FBML snippet that is rendered once on the page before the first tag that defined it. If multiple tags define the same value for header_fbml, and more than one of them appear on a page, then header_fbml is rendered only once. You should only use this for including CSS and initializing any JavaScript variables, not for rendering visible content. Facebook recommends you avoid including heavy JavaScript libraries and performing expensive JavaScript operations in header_fbml for performance reasons. Instead, use footer_fbml.
        ///   footer_fbml: Similar to header_fbml, it's an FBML snippet that gets rendered after all custom tags are rendered. Facebook recommends you include heavy JavaScript libraries and perform any expensive JavaScript operations in footer_fbml, and avoid them in fbml, open_tag_fbml, close_tag_fbml, and header_fbml.
        /// </remarks>
        public void RegisterCustomTagsAsync(List<CustomTag> tags, RegisterCustomTagsCallback callback, Object state)
        {
            RegisterCustomTags(tags, true, callback, state);
        }

        /// <summary>
        /// Fetches and re-caches the image stored at the given URL.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var url = "http://facebook.claritycon.com/Tests/Clarity.jpg";
        ///     api.Fbml.RefreshImgSrcAsync(url, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;fql_result&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="url">The absolute URL from which to refresh the image.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns 1 if Facebook found a cached version of your image and successfully refreshed the image. It returns a blank response if Facebook was unable to find any previously cached version to refresh, or the image was unable to be re-fetched from your site and cached successfully. In such instances, whatever images were cached before remain as they were cached.</returns>
        public void RefreshImgSrcAsync(string url, RefreshImgSrcCallback callback, Object state)
        {
            RefreshImgSrc(url, true, callback, state);
        }

        /// <summary>
        /// Fetches and re-caches the content stored at the given URL.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var url = "http://facebook.claritycon.com/Tests/FBML.html";
        ///     api.Fbml.RefreshRefUrlAsync(url, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="url">The absolute URL from which to fetch content. This URL should be used in a fb:ref FBML tag.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the content was fetched and re-cached from the specified URL.</returns>
        public void RefreshRefUrlAsync(string url, RefreshRefUrlCallback callback, Object state)
        {
            RefreshRefUrl(url, true, callback, state);
        }

        /// <summary>
        /// Associates a given "handle" with FBML markup so that the handle can be used within the fb:ref FBML tag.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     string handle = "my handle";
        ///     string fbml = string.Format("&lt;fb:switch&gt;&lt;fb:profile-pic uid=\"{0}\" /&gt;&lt;fb:default&gt;Unable to show profile pic&lt;/fb:default&gt;&lt;/fb:switch&gt;", Constants.UserId);
        ///     api.Fbml.SetRefHandleAsync(handle, fbml, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="handle">The handle to associate with the given FBML.</param>
        /// <param name="fbml">The FBML to associate with the given handle.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the given handle was associated with FBML markup.</returns>
        public void SetRefHandleAsync(string handle, string fbml, SetRefHandleCallback callback, Object state)
        {
            SetRefHandle(handle, fbml, true, callback, state);
        }

        /// <summary>
        /// Lets you insert text strings into the Facebook Translations database so they can be translated.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     var native_strings = new Dictionary&lt;string, string&gt; { { "text", "(Testing uploadNativeStrings) Do you want to add a friend?" }, { "description", "text string in a popup dialog" } };
        ///     api.Fbml.UploadNativeStringsAsync(native_strings, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="native_strings">A collection of strings to translate. Each element of the collection is an object, with 'text' storing the actual string, 'description' storing the description of the text.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>If successful, this method returns the number of strings uploaded.</returns>
        public void UploadNativeStringsAsync(Dictionary<string, string> native_strings, UploadNativeStringsCallback callback, Object state)
        {
            UploadNativeStrings(native_strings, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods
        
        #region Private Methods
        
        private bool RefreshImgSrc(string url, bool isAsync, RefreshImgSrcCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.fbml.refreshImgSrc"},
			                    		{"url", string.Format(CultureInfo.CurrentUICulture, url)}
			                    	};

            if (isAsync)
            {
                SendRequestAsync<fbml_refreshImgSrc_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<fbml_refreshImgSrc_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
     	}

		private bool RefreshRefUrl(string url, bool isAsync, RefreshRefUrlCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.fbml.refreshRefUrl"},
			                    		{"url", string.Format(CultureInfo.CurrentUICulture, url)}
			                    	};

            if (isAsync)
            {
                SendRequestAsync<fbml_refreshRefUrl_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<fbml_refreshRefUrl_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
        }

		private bool SetRefHandle(string handle, string fbml, bool isAsync, SetRefHandleCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{{"method", "facebook.fbml.setRefHandle"}};
            Utilities.AddRequiredParameter(parameterList, "handle", handle);
			Utilities.AddRequiredParameter(parameterList, "fbml", fbml);

            if (isAsync)
            {
                SendRequestAsync<fbml_setRefHandle_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<fbml_setRefHandle_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
      	}

		private bool UploadNativeStrings(Dictionary<string, string> native_strings, bool isAsync, UploadNativeStringsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.fbml.uploadNativeStrings" } };
			Utilities.AddJSONAssociativeArray(parameterList, "native_strings", native_strings);
                
            if (isAsync)
            {
                SendRequestAsync<fbml_uploadNativeStrings_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<fbml_uploadNativeStrings_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

        private bool DeleteCustomTags(List<string> names, bool isAsync, DeleteCustomTagsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> {{"method", "facebook.fbml.deleteCustomTags"}};
            Utilities.AddJSONArray(parameterList, "names", names);
            //{"names", JSONHelper.ConvertToJSONArray(names)}

            if (isAsync)
            {
                SendRequestAsync<fbml_deleteCustomTags_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<fbml_deleteCustomTags_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        private IList<custom_tag> GetCustomTags(string aid, bool isAsync, GetCustomTagsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.fbml.getCustomTags" } };
            Utilities.AddOptionalParameter(parameterList, "app_id", aid);
                
            if (isAsync)
            {
                SendRequestAsync<fbml_getCustomTags_response, IList<custom_tag>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<custom_tag>>(callback), state, "custom_tag");
                return null;
            }

            var response = SendRequest<fbml_getCustomTags_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.custom_tag;
        }

        private int RegisterCustomTags(IEnumerable<CustomTag> tags, bool isAsync, RegisterCustomTagsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.fbml.registerCustomTags" } };
            var list = new List<string>();
            foreach (var item in tags)
            {
                var dict = new Dictionary<string, string>{
                    {"name", item.Name},
                    {"type", item.Type},
                    {"is_public", item.IsPublic.ToString()},
                    {"description", item.Description},
                    {"fbml", item.FBML},
                    {"open_tag_fbml", item.OpenTagFBML},
                    {"close_tag_fbml", item.CloseTagFBML},
                    {"header_fbml", item.HeaderFBML},
                    {"footer_fbml", item.FooterFBML}
                    //{"label", tag.name},
					//{"type", tag.type.ToString(CultureInfo.InvariantCulture)},
					//{"description", tag.description},
					//{"fbml", tag.body},
					//{"open_tag", tag.open_tag},
					//{"close_tag", tag.close_tag},
                };
                var attribList = new List<string>();
                foreach (var attrib in item.Attributes)
                {
                    var dict2 = new Dictionary<string, string>{
                    {"default_value", attrib.DefaultValue},
                    {"description", attrib.Description},
                    {"name", attrib.Name}};
                    attribList.Add(JSONHelper.ConvertToJSONAssociativeArray(dict2));
                }
                dict.Add("attributes", JSONHelper.ConvertToJSONArray(attribList));
                list.Add(JSONHelper.ConvertToJSONAssociativeArray(dict));
            }
            Utilities.AddJSONArray(parameterList, "tags", list);
            //parameterList.Add("tags", JSONHelper.ConvertToJSONArray(itemList));
                
            if (isAsync)
            {
                SendRequestAsync<fbml_registerCustomTags_response, int>(parameterList, new FacebookCallCompleted<int>(callback), state);
                return 0;
            }

			var response = SendRequest<fbml_registerCustomTags_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
        }

        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when DeleteFbmlCustomTags call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DeleteCustomTagsCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetFbmlCustomTags call completed
        /// </summary>
        /// <param name="tags">List of custom_tag objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetCustomTagsCallback(IList<custom_tag> tags, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RegisterFbmlCustomTags call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RegisterCustomTagsCallback(int result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RefreshFbmlImgSrc call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RefreshImgSrcCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RefreshFbmlRefUrl call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RefreshRefUrlCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetFbmlRefHandle call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetRefHandleCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when UploadFbmlNativeStrings call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void UploadNativeStringsCallback(bool result, Object state, FacebookException e);

        #endregion Delegates
    }

	/// <summary>
	/// Contains the different parts of a Facebook feed template.
	/// </summary>
	public class CustomTag
	{
		/// <summary>
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// </summary>
		public bool IsPublic { get; set; }

		/// <summary>
		/// </summary>
		public List<CustomTagAttribute> Attributes { get; set; }

		/// <summary>
		/// </summary>
		public string FBML { get; set; }

		/// <summary>
		/// </summary>
		public string OpenTagFBML { get; set; }
		/// <summary>
		/// </summary>
		public string CloseTagFBML { get; set; }
		/// <summary>
		/// </summary>
		public string HeaderFBML { get; set; }
		/// <summary>
		/// </summary>
		public string FooterFBML { get; set; }

	}
    /// <summary>
    /// CustomTagAttribute object.
    /// </summary>
	public class CustomTagAttribute
	{
		/// <summary>
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// </summary>
		public string DefaultValue { get; set; }

		/// <summary>
		/// </summary>
		public string Description { get; set; }
	}
}