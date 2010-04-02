using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Facebook.Utility;

namespace Facebook.Rest
{
	public static class ParameterListExtensions
	{
		public static string GenerateSignature(this IDictionary<string, string> parameters, string secret)
		{
			var signatureBuilder = new StringBuilder();

			// Sort the keys of the method call in alphabetical order
			var keyList = Utilities.ParameterDictionaryToList(parameters);
			keyList.Sort();

			// Append all the parameters to the signature input paramaters
			foreach (string key in keyList)
				signatureBuilder.Append(String.Format(CultureInfo.InvariantCulture, "{0}={1}", key, parameters[key]));

			// Append the secret to the signature builder
			signatureBuilder.Append(secret);

			// Compute the MD5 hash of the signature builder
			byte[] hash = MD5Core.GetHash(signatureBuilder.ToString().Trim());

			//var md5 = MD5.Create();
			// Compute the MD5 hash of the signature builder
			//byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(signatureBuilder.ToString().Trim()));

			// Reinitialize the signature builder to store the actual signature
			signatureBuilder = new StringBuilder();

			// Append the hash to the signature
			foreach (var hashByte in hash)
				signatureBuilder.Append(hashByte.ToString("x2", CultureInfo.InvariantCulture));

			return signatureBuilder.ToString();
		}		

		public static string ToEncodedString(this IDictionary<string, string> parameters)
		{
			var queryBuilder = new StringBuilder();
			// Build the query
			foreach (var kvp in parameters)
			{
				queryBuilder.Append(kvp.Key);
				queryBuilder.Append("=");
#if !SILVERLIGHT
				queryBuilder.Append(System.Web.HttpUtility.UrlEncode(kvp.Value));
#else
                queryBuilder.Append(System.Windows.Browser.HttpUtility.UrlEncode(kvp.Value));
#endif
				queryBuilder.Append("&");
			}
			queryBuilder.Remove(queryBuilder.Length - 1, 1);
			return queryBuilder.ToString();
		}

		public static byte[] ToByteArray(this IDictionary<string, string> parameters, byte[] data, string contentType, string boundary)
		{
			// Build the query
			var sb = new StringBuilder();
			foreach (var kvp in parameters)
			{
				sb.Append(Constants.PREFIX).Append(boundary).Append(Constants.NEWLINE);
				sb.Append("Content-Disposition: form-data; name=\"").Append(kvp.Key).Append("\"");
				sb.Append(Constants.NEWLINE);
				sb.Append(Constants.NEWLINE);
				sb.Append(kvp.Value);
				sb.Append(Constants.NEWLINE);
			}

			sb.Append(Constants.PREFIX).Append(boundary).Append(Constants.NEWLINE);
			sb.Append("Content-Disposition: form-data; filename=\"dummyFileName." + BaseAuthenticatedService.MimeTypes[contentType].ToString() + "\"").Append(Constants.NEWLINE);
			sb.Append("Content-Type: ").Append(contentType).Append(Constants.NEWLINE).Append(Constants.NEWLINE);

			byte[] boundaryBytes = Encoding.UTF8.GetBytes(String.Concat(Constants.NEWLINE, Constants.PREFIX, boundary, Constants.PREFIX, Constants.NEWLINE));

			byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());

			using (MemoryStream stream = new MemoryStream(postHeaderBytes.Length + data.Length + boundaryBytes.Length))
			{
				stream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
				stream.Write(data, 0, data.Length);
				stream.Write(boundaryBytes, 0, boundaryBytes.Length);
				return stream.GetBuffer();
			}
		}
	}
}