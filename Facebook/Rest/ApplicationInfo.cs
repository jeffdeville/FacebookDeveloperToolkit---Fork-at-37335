using Facebook.Session;

namespace Facebook.Rest
{
	public class ApplicationInfo
	{
		private FacebookConfiguration _facebookConfig;

		public ApplicationInfo() 
		{
			_facebookConfig = new FacebookConfiguration();
		}
		//public FacebookSession() : this(null, null) { }
		public ApplicationInfo(FacebookConfiguration facebookConfig)
		{
			_facebookConfig = facebookConfig;
		}

		/// <summary>
		/// Application key
		/// </summary>
		public string ApplicationKey
		{
			get { return _facebookConfig.ApiKey; }
		}

		/// <summary>
		/// Application secret
		/// </summary>
		public string ApplicationSecret
		{
			get { return _facebookConfig.Secret; }
		}

		///<summary>
		/// Whether the Http Post and Response should be compressed
		///</summary>
		public bool CompressHttp
		{
			get;
			set;
		}

		
	}
}