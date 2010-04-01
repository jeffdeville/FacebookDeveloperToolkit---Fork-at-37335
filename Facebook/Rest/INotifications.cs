using System;
namespace Facebook.Rest
{
	public interface INotifications : IAuthorizedRestBase
	{
		Facebook.Schema.notifications Get();
		void GetAsync(Notifications.GetCallback callback, object state);
		Facebook.Schema.notification_data GetList(DateTime? start_time, bool include_read);
		void GetListAsync(DateTime? start_time, bool include_read, Notifications.GetListCallback callback, object state);
		bool MarkRead(System.Collections.Generic.List<long> notification_ids);
		void MarkReadAsync(System.Collections.Generic.List<long> notification_ids, Notifications.MarkReadCallback callback, object state);
		string Send(System.Collections.Generic.List<long> to_ids, string notification);
		string Send(System.Collections.Generic.List<long> to_ids, string notification, string type);
		string Send(string to_ids, string notification);
		string Send(string to_ids, string notification, string type);
		void SendAsync(System.Collections.Generic.List<long> to_ids, string notification, Notifications.SendCallback callback, object state);
		void SendAsync(System.Collections.Generic.List<long> to_ids, string notification, string type, Notifications.SendCallback callback, object state);
		void SendAsync(string to_ids, string notification, Notifications.SendCallback callback, object state);
		void SendAsync(string to_ids, string notification, string type, Notifications.SendCallback callback, object state);
		string SendEmail(System.Collections.Generic.List<long> recipients, string subject, string plainText, string fbml);
		string SendEmail(string recipients, string subject, string plainText, string fbml);
		void SendEmailAsync(System.Collections.Generic.List<long> recipients, string subject, string plainText, string fbml, Notifications.SendCallback callback, object state);
		void SendEmailAsync(string recipients, string subject, string plainText, string fbml, Notifications.SendCallback callback, object state);
	}
}
