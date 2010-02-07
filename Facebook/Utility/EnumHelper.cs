using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Facebook.Utility
{
	///<summary>
    /// Contains helper functions used to help when using enums inside of collections and serialization
	///</summary>
	public class EnumHelper
	{
		private EnumHelper()
		{
		}

		///<summary>
		///</summary>
		///<param name="enumeratedType"></param>
		///<typeparam name="T"></typeparam>
		///<returns></returns>
		///<exception cref="ArgumentException"></exception>
		public static string GetEnumDescription<T>(T enumeratedType)
		{
			var description = enumeratedType.ToString();

			var enumType = typeof (T);
			// Can't use type constraints on value types, so have to do check like this
			if (enumType.BaseType != typeof (Enum))
				throw new ArgumentException("T must be of type System.Enum");

			var fieldInfo = enumeratedType.GetType().GetField(enumeratedType.ToString());

			if (fieldInfo != null)
			{
				var attributes = fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);

				if (attributes != null && attributes.Length > 0)
				{
					description = ((DescriptionAttribute) attributes[0]).Description;
				}
			}

			return description;
		}

		///<summary>
		///</summary>
		///<param name="enums"></param>
		///<typeparam name="T"></typeparam>
		///<returns></returns>
		///<exception cref="ArgumentException"></exception>
		public static string GetEnumCollectionDescription<T>(Collection<T> enums)
		{
			var sb = new StringBuilder();

			var enumType = typeof (T);

			// Can't use type constraints on value types, so have to do check like this
			if (enumType.BaseType != typeof (Enum))
				throw new ArgumentException("T must be of type System.Enum");

			foreach (var enumeratedType in enums)
			{
				sb.AppendLine(GetEnumDescription(enumeratedType));
			}

			return sb.ToString();
		}
	}
}