using System;
namespace Facebook.Rest
{
	public interface ILiveMessage : IRestBase
	{
		bool Send(long recipient, string event_name, string message);
		void SendAsync(long recipient, string event_name, string message, LiveMessage.SendCallback callback, object state);
	}
}
