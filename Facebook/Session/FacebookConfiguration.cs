using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Facebook.Session
{
    public class FacebookConfiguration
    {
        public FacebookConfiguration()
        {
            LoadValues();
        }

        private void LoadValues()
        {
            var values = (NameValueCollection) ConfigurationSettings.GetConfig("facebook");

            if(values == null)
                throw new Exception("Facebook must have application key and secret before logging in." + Environment.NewLine);

            ApiKey = values["ApiKey"];
            Secret = values["Secret"];
        	ConnectLogonUrl = values["ConnectLogonUrl"];

        	var compress = values["CompressRequest"] ?? "true";
			CompressRequest = Convert.ToBoolean(compress);

            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(Secret))
                throw new Exception("Session must have application key and secret before logging in." + Environment.NewLine);
        }

        public FacebookConfiguration(string apiKey, string secret, string connectLogonUrl)
        {
            if (apiKey == null || secret == null) LoadValues();            
            else
            {
                ApiKey = apiKey;
                Secret = secret;
            	ConnectLogonUrl = connectLogonUrl;
            }
        }

        public string ApiKey { get; private set; }
        public string Secret { get; private set; }
    	public string ConnectLogonUrl { get; set; }
    	public bool CompressRequest { get; set; }
    	
    }
}