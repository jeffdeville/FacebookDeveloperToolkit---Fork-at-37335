using System;

namespace Facebook.Session
{
    public class CachedSessionInfo
    {
        public CachedSessionInfo(string sessionKey, long userId, DateTime expiryTime)
        {
            SessionKey = sessionKey;
            UserId = userId;
            ExpiryTime = expiryTime;
        }

        public string SessionKey { get; set; }
        public long UserId { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}