﻿using System;
using System.Collections.Specialized;
using Facebook.Rest;
using Facebook.Utility;

namespace Facebook.Session
{
    ///<summary>
	/// A provider that will look for session info for FBML pages.  If the session does not exist, but an 
	/// auth token does, the token will be exchanged for a session.
	///</summary>
	public class FbmlSessionProvider : ISessionProvider
	{		
		protected NameValueCollection _inputParams;
    	private readonly IAuth _auth;

    	public FbmlSessionProvider(NameValueCollection inputParams, IAuth auth)
		{
			_inputParams = inputParams;
			_auth = auth;
		}

    	public virtual IFacebookSession GetSession()
		{
			// The logic here, is to look for a sessionkey that already exists, or exchange an 
			// authtoken for a sessionkey if that exists instead.
			if (!string.IsNullOrEmpty(SessionKeyFromRequest))
				return CreateSession(SessionKeyFromRequest, UserId, ExpirationTime);
			return !string.IsNullOrEmpty(AuthToken) ? ExchangeAuthTokenForSession() : null;
		}
		
		private bool InProfileTab
		{
			get { return _inputParams[QueryParameters.InProfileTab] == "1"; }
		}

		protected virtual string SessionKeyFromRequest
		{
			get
			{
				return InProfileTab
					? _inputParams[QueryParameters.ProfileSessionKey]
					: _inputParams[QueryParameters.SessionKey];
			}
		}

		protected string AuthToken
		{
			get { return _inputParams[QueryParameters.AuthToken]; }
		}

		protected long UserId
		{
			get
			{
				return long.Parse(InProfileTab
				                  	? _inputParams[QueryParameters.ProfileUser]
				                  	: _inputParams[QueryParameters.User]);
			}
		}

		protected DateTime ExpirationTime
		{
			get { return DateHelper.ConvertUnixTimeToDateTime(long.Parse(_inputParams[QueryParameters.Expires])); }
		}

		

		protected IFacebookSession ExchangeAuthTokenForSession()
		{
			var sessionInfo = _auth.GetSession(AuthToken);
			return CreateSession(sessionInfo.session_key, sessionInfo.uid,
			                            DateHelper.ConvertUnixTimeToDateTime(sessionInfo.expires));
		}

		protected FacebookSession CreateSession(string sessionKey, long userId, 
			DateTime expiryTime)
		{
			return new FacebookSession {SessionKey = sessionKey, UserId = userId, ExpiryTime = expiryTime};			
		}
	}
}