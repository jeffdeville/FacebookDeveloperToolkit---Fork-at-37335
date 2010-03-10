using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Facebook.Tests.Mvc.MvcFakes
{
	public class FakeHttpFileCollection : HttpFileCollectionBase
	{
		// Fields
		private Dictionary<string, HttpPostedFile> _collection;

		// Methods
		public FakeHttpFileCollection()
		{
			_collection = new Dictionary<string, HttpPostedFile>();			
		}

		public override void CopyTo(Array dest, int index)
		{
			throw new NotImplementedException("This one isn't mocked");
		}

		public override HttpPostedFileBase Get(int index)
		{
			HttpPostedFile httpPostedFile = this._collection.Values.ToList()[index];
			if (httpPostedFile == null)
			{
				return null;
			}
			return new HttpPostedFileWrapper(httpPostedFile);
		}

		public override HttpPostedFileBase Get(string name)
		{
			HttpPostedFile httpPostedFile = this._collection[name];
			if (httpPostedFile == null)
			{
				return null;
			}
			return new HttpPostedFileWrapper(httpPostedFile);
		}

		public override IEnumerator GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		public override string GetKey(int index)
		{
			return this._collection.Keys.ToList()[index];
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this._collection.GetObjectData(info, context);
		}

		public override void OnDeserialization(object sender)
		{
			this._collection.OnDeserialization(sender);
		}

		// Properties
		public override string[] AllKeys
		{
			get
			{
				return this._collection.Keys.ToArray();
			}
		}

		public override int Count
		{
			get
			{
				return this._collection.Count;
			}
		}

		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public override HttpPostedFileBase this[string name]
		{
			get
			{
				HttpPostedFile httpPostedFile = this._collection[name];
				if (httpPostedFile == null)
				{
					return null;
				}
				return new HttpPostedFileWrapper(httpPostedFile);
			}
		}

		public override HttpPostedFileBase this[int index]
		{
			get
			{
				HttpPostedFile httpPostedFile = this._collection.Values.ToList()[index];
				if (httpPostedFile == null)
				{
					return null;
				}
				return new HttpPostedFileWrapper(httpPostedFile);
			}
		}

		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}