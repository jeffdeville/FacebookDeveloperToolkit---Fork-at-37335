using System;
namespace Facebook.Rest
{
	public interface IBatch
	{
		void BeginBatch();
		System.Collections.Generic.List<string> CallList { get; }
		System.Collections.Generic.List<BatchRecord> CallListAsync { get; }
		System.Collections.Generic.IList<object> ExecuteBatch();
		System.Collections.Generic.IList<object> ExecuteBatch(bool isSerial);
		void ExecuteBatchAsync();
		void ExecuteBatchAsync(bool isSerial);
		bool IsActive { get; }
	}
}
