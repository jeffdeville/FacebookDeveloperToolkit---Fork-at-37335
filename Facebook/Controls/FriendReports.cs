using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Facebook.Controls
{
    [ToolboxItem(true)]
    public partial class FriendReports : BaseControl
    {
        private Collection<User> _friends = null;

        [Category("Facebook")]
        [Description("")]
        public Collection<User> Friends
        {
            get { return _friends; }
            set 
            {
                _friends = value;
                OnLoad();
            }
        }

        public FriendReports()
        {
            InitializeComponent();
        }

        private void OnLoad()
        {
            if (!IsDesignTime() && _friends != null)
            {
               Microsoft.Reporting.WinForms.ReportDataSource  rptSource1 = 
                   new Microsoft.Reporting.WinForms.ReportDataSource();
               rptSource1.Name = "myProj_EsnItems";
               rptSource1.Value = UserBindingSource;

               reportViewer1.LocalReport.DataSources.Add(rptSource1);
                reportViewer1.LocalReport.ReportEmbeddedResource = "Facebook.Controls.Report1.rdlc";
                reportViewer1.RefreshReport();
            }
        }

    
    }
}
