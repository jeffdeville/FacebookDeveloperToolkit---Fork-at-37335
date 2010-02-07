using System.ComponentModel;
using System.ComponentModel.Design;

namespace Facebook.Winforms.Components
{
	internal class FacebookServiceDesigner : ComponentDesigner
	{
		private DesignerActionListCollection _dalc;

		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (_dalc == null)
				{
					_dalc = new DesignerActionListCollection {new FacebookServiceDesignerActionList(Component)};
				}

				return _dalc;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class FacebookServiceDesignerActionList : DesignerActionList
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="component"></param>
		public FacebookServiceDesignerActionList(IComponent component)
			: base(component)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		[Category("Setup")]
		[Description("The API key received from facebook for this application that is using the FacebookService component")]
		[DisplayName("API Key")]
		public string ApplicationKey
		{
			get { return FacebookService.ApplicationKey; }
			set { SetProperty("ApplicationKey", value); }
		}


		/// <summary>
		/// 
		/// </summary>
		private FacebookService FacebookService
		{
			get { return (FacebookService) Component; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="value"></param>
		private void SetProperty(string propertyName, object value)
		{
			var property = TypeDescriptor.GetProperties(FacebookService)[propertyName];
			property.SetValue(FacebookService, value);
		}
	}
}