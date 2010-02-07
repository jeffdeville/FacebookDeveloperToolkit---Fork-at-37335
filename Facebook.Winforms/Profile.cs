using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook.Winforms.Properties;
using Facebook.Schema;

namespace Facebook.Winforms
{
	/// <summary>
	/// A control to display a user's profile.
	/// </summary>
	[ToolboxItem(true), ToolboxBitmap(typeof (BaseControl))]
	public partial class Profile : BaseControl
	{
		private user _user;

        /// <summary> 
        /// Constructor
        /// </summary>
        public Profile()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The user to display in the control.
		/// </summary>
		[Category("Facebook")]
		[Description("")]
		public user User
		{
			get { return _user; }
			set
			{
				_user = value;
				OnLoad();
			}
		}

		private void OnLoad()
		{
			if (!IsDesignTime() && _user != null)
			{
				LoadControls();
			}
		}

		private void LoadControls()
		{
			LoadBasicInfoPanel(_user);
			LoadPersonalInfoPanel(_user);
			LoadEducationInfoPanel(_user);
			LoadWorkInfoPanel(_user);
			LoadPicture(_user);
		}

		private void LoadPicture(user user)
		{
			if (user.pic != null)
			{
				pbProfilePicture.ImageLocation = user.pic;
			}
			else
			{
				pbProfilePicture.Image = user.picture;
			}
		}

		private void LoadWorkInfoPanel(user user)
		{
			tlpWork.Controls.Clear();

			if (user.work_history == null || user.work_history.work_info.Count <= 0) return;

			var row = 0;
			foreach (var job in user.work_history.work_info)
			{
				if (!String.IsNullOrEmpty(job.company_name))
				{
					tlpWork.Controls.Add(CreatePromptLabel(Resources.lblEmployer), 0, row);
					tlpWork.Controls.Add(CreateValueLabel(job.company_name), 1, row);
					row++;
				}
				if (!String.IsNullOrEmpty(job.position))
				{
					tlpWork.Controls.Add(CreatePromptLabel(Resources.lblPosition), 0, row);
					tlpWork.Controls.Add(CreateValueLabel(job.position), 1, row);
					row++;
				}
				if (!String.IsNullOrEmpty(job.start_date))
				{
					var timePeriod = new StringBuilder();
					timePeriod.Append(job.start_date);
					timePeriod.Append(" - ");
					timePeriod.Append(job.end_date);

					tlpWork.Controls.Add(CreatePromptLabel(Resources.lblTimePeriod), 0, row);
					tlpWork.Controls.Add(CreateValueLabel(timePeriod.ToString()), 1, row);
					row++;
				}
				if (job.location != null)
				{
					tlpWork.Controls.Add(CreatePromptLabel(Resources.lblLocation), 0, row);
					tlpWork.Controls.Add(CreateValueLabel(job.location.city + ", " + job.location.state), 1, row);
					row++;
				}
				if (!String.IsNullOrEmpty(job.description))
				{
					tlpWork.Controls.Add(CreatePromptLabel(Resources.lblDescription), 0, row);
					tlpWork.Controls.Add(CreateValueLabel(job.description), 1, row);
					row++;
				}

				tlpWork.Controls.Add(new Label(), 0, row);
				tlpWork.Controls.Add(new Label(), 1, row);
				row++;
			}

			foreach (RowStyle rowStyle in tlpWork.RowStyles)
			{
				rowStyle.SizeType = SizeType.AutoSize;
			}
		}

		private void LoadEducationInfoPanel(user user)
		{
			tpEducation.Controls.Clear();

            if ((user.education_history == null) || (user.education_history.education_info == null)) return;


			var collegeText = new StringBuilder();

			foreach (var education in user.education_history.education_info)
			{
				collegeText.Append(education.name);
				collegeText.Append(" '");
				if (education.year > 0)
					collegeText.AppendLine(education.year.ToString(CultureInfo.InvariantCulture).Substring(2));

				foreach (var concentration in education.concentrations.concentration)
				{
					collegeText.AppendLine(concentration);
				}
			}
			SetRowValue(tlpEducation, lblCollege, collegeText.ToString().Trim());
		}

		private void LoadPersonalInfoPanel(user user)
		{
			SetRowValue(tlpPersonal, lblActivities, user.activities);
			SetRowValue(tlpPersonal, lblInterests, user.interests);
			SetRowValue(tlpPersonal, lblFavoriteMusic, user.music);
			SetRowValue(tlpPersonal, lblFavoriteTVShows, user.tv);
			SetRowValue(tlpPersonal, lblFavoriteMovies, user.movies);
			SetRowValue(tlpPersonal, lblFavoriteBooks, user.books);
			SetRowValue(tlpPersonal, lblFavoriteQuotes, user.quotes);
			SetRowValue(tlpPersonal, lblAboutMe, user.about_me);
		}

		private void LoadBasicInfoPanel(user user)
		{


			lblName.Text = user.name;
			lblLocation.Text = user.current_location.city  + ", " + user.current_location.state;

			SetRowValue(tlpBasic, lblSex, user.sex);
            if (user.meeting_sex != null)
            {
                SetRowValue(tlpBasic, lblInterestedIn, string.Join(",", user.meeting_sex.sex.ToArray()));
            }
            else
            {
                SetRowValue(tlpBasic, lblInterestedIn, string.Empty);
            }
            SetRowValue(tlpBasic, lblRelationshipStatus, user.relationship_status);
            if (user.meeting_for != null)
            {
                SetRowValue(tlpBasic, lblLookingFor, string.Join(",", user.meeting_for.seeking.ToArray()));
            }
            else
            {
                SetRowValue(tlpBasic, lblLookingFor, string.Empty);
            }
            SetRowValue(tlpBasic, lblBirthday, user.birthday);
            if (user.hometown_location != null)
            {
                SetRowValue(tlpBasic, lblHometown, user.hometown_location.city + ", " + user.hometown_location.state);
            }
            else
            {
                SetRowValue(tlpBasic, lblHometown, string.Empty);
            }
			SetRowValue(tlpBasic, lblPoliticalViews, user.political);
			SetRowValue(tlpBasic, lblReligiousViews, user.religion);
		}

		private static void SetRowValue(TableLayoutPanel layoutPanel, Control label, string value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				label.Text = value;
				layoutPanel.RowStyles[layoutPanel.GetRow(label)].SizeType = SizeType.AutoSize;
			}
			else
			{
				layoutPanel.RowStyles[layoutPanel.GetRow(label)].SizeType = SizeType.Absolute;
				layoutPanel.RowStyles[layoutPanel.GetRow(label)].Height = 0;
			}

			layoutPanel.AutoScroll = true;
		}

		private static Label CreatePromptLabel(string text)
		{
			return new Label
			       	{
			       		Text = (text + ":"),
			       		Font = new Font("Tahoma", 8.25f),
			       		ForeColor = Color.Gray,
			       		Dock = DockStyle.Top,
			       		Margin = new Padding(3, 3, 3, 0),
			       		AutoSize = true
			       	};
		}

		private static Label CreateValueLabel(string text)
		{
			return new Label
			       	{
			       		Text = text,
			       		Font = new Font("Tahoma", 8.25f),
			       		ForeColor = Color.FromArgb(96, 120, 205),
			       		Dock = DockStyle.Top,
			       		Margin = new Padding(3, 3, 3, 0),
			       		AutoSize = true
			       	};
		}

		private void OnDrawItem(DrawItemEventArgs e)
		{
			Brush backBrush;
			Brush foreBrush;

			if (e.Index == tcProfile.SelectedIndex)
			{
				backBrush = new SolidBrush(Color.FromArgb(59, 89, 152));
				foreBrush = Brushes.White;
			}
			else
			{
				backBrush = Brushes.White;
				foreBrush = new SolidBrush(Color.FromArgb(59, 89, 152));
			}

			var tabName = tcProfile.TabPages[e.Index].Text;
			var sf = new StringFormat {Alignment = StringAlignment.Center};

			var r = tcProfile.GetTabRect(e.Index);
			e.Graphics.FillRectangle(backBrush, r);

			e.Graphics.DrawString(tabName, tcProfile.Font, foreBrush, r, sf);

			sf.Dispose();

			backBrush.Dispose();
			foreBrush.Dispose();
		}

		#region Event Handlers

		private void Profile_Load(object sender, EventArgs e)
		{
			OnLoad();
		}

		private void tcProfile_DrawItem(object sender, DrawItemEventArgs e)
		{
			OnDrawItem(e);
		}

		#endregion Event Handlers
	}
}