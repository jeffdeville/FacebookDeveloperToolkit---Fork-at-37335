using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Facebook;
using Facebook.Utility;
using System.Globalization;

namespace Facebook.Controls
{
    [ToolboxItem(true)]
    public partial class Profile : BaseControl
    {
        private User _user = null;

        [Category("Facebook")]
        [Description("")]
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnLoad();
            }
        }
        public Profile()
        {
            InitializeComponent();
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

        private void LoadPicture(User user)
        {
            pbProfilePicture.Image = user.Picture;
        }

        private void LoadWorkInfoPanel(User user)
        {
            tlpWork.Controls.Clear();

            if (user.WorkHistory != null && user.WorkHistory.Count > 0)
            {
                int row = 0;

                foreach(Work job in user.WorkHistory)
                {
                    if (!String.IsNullOrEmpty(job.CompanyName))
                    {
                        tlpWork.Controls.Add(CreatePromptLabel(Facebook.Properties.Resources.lblEmployer), 0, row);
                        tlpWork.Controls.Add(CreateValueLabel(job.CompanyName), 1, row);
                        row++;
                    }
                    if (!String.IsNullOrEmpty(job.Position))
                    {
                        tlpWork.Controls.Add(CreatePromptLabel(Facebook.Properties.Resources.lblPosition), 0, row);
                        tlpWork.Controls.Add(CreateValueLabel(job.Position), 1, row);
                        row++;
                    }
                    if (job.StartDate != null)
                    {
                        StringBuilder timePeriod = new StringBuilder();
                        timePeriod.Append(job.StartDate.ToString("MM/yyyy", CultureInfo.InvariantCulture));
                        timePeriod.Append(" - ");

                        if (job.EndDate == DateTime.MinValue)
                            timePeriod.Append("Present");
                        else
                            timePeriod.Append(job.EndDate.ToString("MM/yyyy", CultureInfo.InvariantCulture));

                        tlpWork.Controls.Add(CreatePromptLabel(Facebook.Properties.Resources.lblTimePeriod), 0, row);
                        tlpWork.Controls.Add(CreateValueLabel(timePeriod.ToString()), 1, row);
                        row++;
                    }
                    if (job.Location != null)
                    {
                        tlpWork.Controls.Add(CreatePromptLabel(Facebook.Properties.Resources.lblLocation), 0, row);
                        tlpWork.Controls.Add(CreateValueLabel(job.Location.ToString()), 1, row);
                        row++;
                    }
                    if (!String.IsNullOrEmpty(job.Description))
                    {
                        tlpWork.Controls.Add(CreatePromptLabel(Facebook.Properties.Resources.lblDescription), 0, row);
                        tlpWork.Controls.Add(CreateValueLabel(job.Description), 1, row);
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
        }

        private void LoadEducationInfoPanel(User user)
        {
            tpEducation.Controls.Clear();

            if (user.SchoolHistory != null)
            {
                if (user.SchoolHistory.HigherEducation != null)
                {
                    StringBuilder collegeText = new StringBuilder();

                    foreach (HigherEducation higherEducation in user.SchoolHistory.HigherEducation)
                    {
                        collegeText.Append(higherEducation.School);
                        collegeText.Append(" '");
                        if (higherEducation.ClassYear > 0)
                            collegeText.AppendLine(higherEducation.ClassYear.ToString(CultureInfo.InvariantCulture).Substring(2));

                        foreach (string concentration in higherEducation.Concentration)
                        {
                            collegeText.AppendLine(concentration);
                        }
                    }
                    SetRowValue(tlpEducation, lblCollege, collegeText.ToString().Trim());
                }

                if (user.SchoolHistory.HighSchool != null)
                {
                    StringBuilder highSchoolText = new StringBuilder();

                    if (!String.IsNullOrEmpty(user.SchoolHistory.HighSchool.HighSchoolOneName))
                    {
                        highSchoolText.Append(user.SchoolHistory.HighSchool.HighSchoolOneName);

                        if (user.SchoolHistory.HighSchool.GraduationYear > 0)
                        {
                            highSchoolText.Append(" '");
                            if (user.SchoolHistory.HighSchool.GraduationYear > 0)
                            {
                                highSchoolText.AppendLine(user.SchoolHistory.HighSchool.GraduationYear.ToString(CultureInfo.InvariantCulture).Substring(2));
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(user.SchoolHistory.HighSchool.HighSchoolTwoName))
                    {
                        highSchoolText.AppendLine(user.SchoolHistory.HighSchool.HighSchoolTwoName);
                    }

                    SetRowValue(tlpEducation, lblHighSchool, highSchoolText.ToString().Trim());
                }
            }
        }

        private void LoadPersonalInfoPanel(User user)
        {
            SetRowValue(tlpPersonal, lblActivities, user.Activities);
            SetRowValue(tlpPersonal, lblInterests, user.Interests);
            SetRowValue(tlpPersonal, lblFavoriteMusic, user.Music);
            SetRowValue(tlpPersonal, lblFavoriteTVShows, user.TVShows);
            SetRowValue(tlpPersonal, lblFavoriteMovies, user.Movies);
            SetRowValue(tlpPersonal, lblFavoriteBooks, user.Books);
            SetRowValue(tlpPersonal, lblFavoriteQuotes, user.Quotes);
            SetRowValue(tlpPersonal, lblAboutMe, user.AboutMe);
        }

        private void LoadBasicInfoPanel(User user)
        {
            lblName.Text = user.Name;
            lblLocation.Text = user.CurrentLocation.ToString();

            SetRowValue(tlpBasic, lblSex, EnumHelper.GetEnumDescription(user.Sex));
            SetRowValue(tlpBasic, lblInterestedIn, EnumHelper.GetEnumCollectionDescription(user.InterestedInGenders));
            SetRowValue(tlpBasic, lblRelationshipStatus, EnumHelper.GetEnumDescription(user.RelationshipStatus));
            SetRowValue(tlpBasic, lblLookingFor, EnumHelper.GetEnumCollectionDescription(user.InterstedInRelationshipTypes));
            if (user.Birthday != null)
            {
                SetRowValue(tlpBasic, lblBirthday, user.Birthday.Value.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture));
            }
            else
            {
                SetRowValue(tlpBasic, lblBirthday, string.Empty);
            }
            SetRowValue(tlpBasic, lblHometown, user.HometownLocation.ToString());
            SetRowValue(tlpBasic, lblPoliticalViews, EnumHelper.GetEnumDescription(user.PoliticalView));
            SetRowValue(tlpBasic, lblReligiousViews, user.Religion);
        }

        private static void SetRowValue(TableLayoutPanel layoutPanel, Label label, string value)
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
            Label label = new Label();
            label.Text = text + ":";
            label.Font = new Font("Tahoma", 8.25f);
            label.ForeColor = Color.Gray;
            label.Dock = DockStyle.Top;
            label.Margin = new Padding(3, 3, 3, 0);
            label.AutoSize = true;

            return label;
        }

        private static Label CreateValueLabel(string text)
        {
            Label label = new Label();
            label.Text = text;
            label.Font = new Font("Tahoma", 8.25f);
            label.ForeColor = Color.FromArgb(96, 120, 205);
            label.Dock = DockStyle.Top;
            label.Margin = new Padding(3, 3, 3, 0);
            label.AutoSize = true;

            return label;
        }

        private void OnDrawItem(DrawItemEventArgs e)
        {
            //try
            //{
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

                

                //To set the alignment of the caption.
                string tabName = tcProfile.TabPages[e.Index].Text;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;

                Rectangle r = tcProfile.GetTabRect(e.Index);
                e.Graphics.FillRectangle(backBrush, r);

                //r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
                e.Graphics.DrawString(tabName, tcProfile.Font, foreBrush, r, sf);

                sf.Dispose();

                backBrush.Dispose();
                foreBrush.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    string message = ex.Message;
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
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
