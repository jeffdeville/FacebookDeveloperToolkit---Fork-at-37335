using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Facebook.Controls
{
    [ToolboxItem(false)]
    public partial class BaseControl : UserControl
    {

        protected bool IsDesignTime()
        {
            return (this.Site != null && this.Site.DesignMode);
        }

        public BaseControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }
    }
}
