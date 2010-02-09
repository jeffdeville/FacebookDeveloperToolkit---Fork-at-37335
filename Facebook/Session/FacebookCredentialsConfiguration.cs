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

            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(Secret))
                throw new Exception("Session must have application key and secret before logging in." + Environment.NewLine);
        }

        public FacebookConfiguration(string apiKey, string secret)
        {
            if (apiKey == null || secret == null) LoadValues();            
            else
            {
                ApiKey = apiKey;
                Secret = secret;
            }
        }

        public string ApiKey { get; private set; }
        public string Secret { get; private set; }
    }
}