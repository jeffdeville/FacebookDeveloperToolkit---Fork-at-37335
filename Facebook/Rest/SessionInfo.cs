using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;

namespace Facebook.Rest
{
	public class SessionInfo : IFacebookSession
	{
		public string SessionKey { get; set; }
		public string ApplicationKey { get; set; }
		public string ApplicationSecret { get; private set; }
		public bool CompressHttp { get; set; }
		public DateTime ExpiryTime { get; set; }
		public bool SessionExpires { get; set; }
		public string SessionSecret { get; set; }
		public string Secret { get; private set; }
		public long UserId { get; set; }
		public List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }
		public string CheckPermissions()
		{
			throw new NotImplementedException();
		}
	}
}