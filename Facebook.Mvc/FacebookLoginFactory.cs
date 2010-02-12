﻿using System;
using System.Collections.Specialized;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Mvc
{
	public class FacebookLoginFactory : IFacebookLoginFactory
	{
		private readonly Uri _currentUrl;
		private NameValueCollection _requestParameters;
		private IFacebookApi _facebookApi;

		public FacebookLoginFactory(Uri currentUrl, NameValueCollection requestParameters, IFacebookApi facebookApi)
		{
			_currentUrl = currentUrl;
			_requestParameters = requestParameters;
			_facebookApi = facebookApi;
		}

		#region Implementation of IFacebookLoginFactory

		public ILoginHandler GetLoginHandler(FacebookPageType pageType)
		{
			switch (pageType)
			{
				case FacebookPageType.Connect:
					return new ConnectLogin(_currentUrl);
				case FacebookPageType.IFrame:
					return new IFrameLogin(_requestParameters);
				case FacebookPageType.Fbml:
					return new FbmlLogin(_requestParameters, _facebookApi);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion
	}
}