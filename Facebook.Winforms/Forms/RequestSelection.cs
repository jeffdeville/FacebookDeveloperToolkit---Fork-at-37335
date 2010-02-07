using System;
using System.Windows.Forms;
using Facebook.Winforms.Components;

namespace Facebook.Winforms.Forms
{
    internal sealed partial class RequestSelection : Form
    {
        #region Private Data

        private readonly Uri _callbackUrl;

        private readonly FacebookService _facebookService;

        #endregion

        #region Constructors

        private RequestSelection()
        {
            InitializeComponent();
        }

        public RequestSelection(string requestUrl, Uri callbackUrl, FacebookService service)
            : this()
        {
            _callbackUrl = callbackUrl;
            _facebookService = service;
            wbRequest.Navigate(new UnicodeUri(requestUrl));
        }

        #endregion

        #region Private Methods

        private void wbRequest_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //if (e.Url.ToString().StartsWith(_callbackUrl.ToString()))
            //{
            //    _facebookService.ReceiveRequestData(e.Url);
            //    Close();
            //}
        }

        #endregion
    }
}