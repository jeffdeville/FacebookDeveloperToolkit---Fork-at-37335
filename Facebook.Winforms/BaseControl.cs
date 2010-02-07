using System.ComponentModel;
using System.Windows.Forms;

namespace Facebook.Winforms
{
    /// <summary> 
    /// Base Control used to help other controls work in UI Designer correctly.
    /// </summary>
    [ToolboxItem(false)]
	public partial class BaseControl : UserControl
	{
        /// <summary> 
        /// Constructor
        /// </summary>
        public BaseControl()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
		}

        /// <summary> 
        /// used to helper experience with this control in the IDE designer
        /// </summary>
        protected bool IsDesignTime()
		{
			return (Site != null && Site.DesignMode);
		}
	}
}