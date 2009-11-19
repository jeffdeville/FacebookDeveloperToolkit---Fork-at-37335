using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Facebook.Session;

namespace Facebook.Tests.Asynchronous
{
	/// <summary>
	/// Just a simple class to use as a web session, since the CanvasSession objects are not available in this Silverlight test framework.
	/// </summary>
	public class TestSession : FacebookSession
	{
		public TestSession(string apiKey, string secret)
		{
			ApplicationKey = apiKey;
			ApplicationSecret = secret;
		}

		public override void Login()
		{
			throw new NotImplementedException();
		}

		public override void Logout()
		{
			throw new NotImplementedException();
		}
	}
}
