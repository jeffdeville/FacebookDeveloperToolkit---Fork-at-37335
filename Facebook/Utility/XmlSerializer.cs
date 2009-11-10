using System;
using System.Xml;
using System.IO;

namespace Facebook.Utility
{
	public class XmlSerializer
	{
		public static T Deserialize<T>(string xml)
		{
			XmlReader xmlReader = XmlReader.Create(new StringReader(xml));
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

			try
			{
				return (T)serializer.Deserialize(xmlReader);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Unable to deserialize XML into object of type '{0}'. The XML was the following: {1}", typeof(T).ToString(), xml), ex);
			}
		}
	}
}
