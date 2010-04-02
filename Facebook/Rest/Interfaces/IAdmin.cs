using System;
namespace Facebook.Rest
{
	public interface IAdmin : IAuthenticatedService
	{
		bool BanUsers(System.Collections.Generic.List<long> uids);
		void BanUsersAsync(System.Collections.Generic.List<long> uids, Admin.BanUsersCallback callback, object state);
		int GetAllocation(Admin.IntegrationPointName name);
		void GetAllocationAsync(Admin.IntegrationPointName name, Admin.GetAllocationCallback callback, object state);
		System.Collections.Generic.List<string> GetApplicationPropertyNames();
		System.Collections.Generic.Dictionary<string, string> GetAppProperties();
		System.Collections.Generic.Dictionary<string, string> GetAppProperties(System.Collections.Generic.List<string> properties);
		void GetAppPropertiesAsync(Admin.GetAppPropertiesCallback callback, object state);
		void GetAppPropertiesAsync(System.Collections.Generic.List<string> properties, Admin.GetAppPropertiesCallback callback, object state);
		System.Collections.Generic.IList<long> GetBannedUsers();
		System.Collections.Generic.IList<long> GetBannedUsers(System.Collections.Generic.List<long> uids);
		void GetBannedUsersAsync(Admin.GetBannedUsersCallback callback, object state);
		void GetBannedUsersAsync(System.Collections.Generic.List<long> uids, Admin.GetBannedUsersCallback callback, object state);
		System.Collections.Generic.List<string> GetDailyMetricNames();
		System.Collections.Generic.List<string> GetMetricNames(Admin.Period period);
		System.Collections.Generic.IList<Facebook.Schema.metrics> GetMetrics(System.Collections.Generic.List<string> metrics, DateTime startDate, DateTime endDate, Admin.Period period);
		System.Collections.Generic.IList<Facebook.Schema.metrics> GetMetrics(DateTime startDate, DateTime endDate, Admin.Period period);
		void GetMetricsAsync(System.Collections.Generic.List<string> metrics, DateTime startDate, DateTime endDate, Admin.Period period, Admin.GetMetricsCallback callback, object state);
		void GetMetricsAsync(DateTime startDate, DateTime endDate, Admin.Period period, Admin.GetMetricsCallback callback, object state);
		string GetRestrictionInfo();
		void GetRestrictionInfoAsync(Admin.GetRestrictionInfoCallback callback, object state);
		bool SetAppProperties(System.Collections.Generic.Dictionary<string, string> properties);
		void SetAppPropertiesAsync(System.Collections.Generic.Dictionary<string, string> properties, Admin.SetAppPropertiesCallback callback, object state);
		bool SetRestrictionInfo();
		bool SetRestrictionInfo(System.Collections.Generic.Dictionary<string, string> restriction);
		void SetRestrictionInfoAsync(Admin.SetRestrictionInfoCallback callback, object state);
		void SetRestrictionInfoAsync(System.Collections.Generic.Dictionary<string, string> restriction, Admin.SetRestrictionInfoCallback callback, object state);
		bool UnbanUsers(System.Collections.Generic.List<long> uids);
		void UnbanUsersAsync(System.Collections.Generic.List<long> uids, Admin.UnBanUsersCallback callback, object state);
	}
}
