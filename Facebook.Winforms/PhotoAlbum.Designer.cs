namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays a photo album
    /// </summary>
    partial class PhotoAlbum
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.lblPhotoCount = new System.Windows.Forms.Label();
            this.lbPrevious = new System.Windows.Forms.LinkLabel();
            this.lbnext = new System.Windows.Forms.LinkLabel();
            this.lblTag = new System.Windows.Forms.Label();
            this.cboAlbums = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fbdExport = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPhoto
            // 
            this.pbPhoto.BackColor = System.Drawing.Color.White;
            this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPhoto.Location = new System.Drawing.Point(15, 37);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(277, 150);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPhoto.TabIndex = 23;
            this.pbPhoto.TabStop = false;
            // 
            // lblPhotoCount
            // 
            this.lblPhotoCount.AutoSize = true;
            this.lblPhotoCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhotoCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(89)))), ((int)(((byte)(152)))));
            this.lblPhotoCount.Location = new System.Drawing.Point(12, 208);
            this.lblPhotoCount.Name = "lblPhotoCount";
            this.lblPhotoCount.Size = new System.Drawing.Size(0, 13);
            this.lblPhotoCount.TabIndex = 24;
            // 
            // lbPrevious
            // 
            this.lbPrevious.AutoSize = true;
            this.lbPrevious.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lbPrevious.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lbPrevious.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lbPrevious.Location = new System.Drawing.Point(97, 233);
            this.lbPrevious.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbPrevious.Name = "lbPrevious";
            this.lbPrevious.Size = new System.Drawing.Size(48, 13);
            this.lbPrevious.TabIndex = 28;
            this.lbPrevious.TabStop = true;
            this.lbPrevious.Text = "Previous";
            this.lbPrevious.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lbPrevious.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbPrevious_LinkClicked);
            // 
            // lbnext
            // 
            this.lbnext.AutoSize = true;
            this.lbnext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lbnext.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lbnext.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lbnext.Location = new System.Drawing.Point(172, 233);
            this.lbnext.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbnext.Name = "lbnext";
            this.lbnext.Size = new System.Drawing.Size(30, 13);
            this.lbnext.TabIndex = 29;
            this.lbnext.TabStop = true;
            this.lbnext.Text = "Next";
            this.lbnext.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lbnext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbnext_LinkClicked);
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTag.ForeColor = System.Drawing.Color.Black;
            this.lblTag.Location = new System.Drawing.Point(17, 190);
            this.lblTag.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(275, 13);
            this.lblTag.TabIndex = 30;
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboAlbums
            // 
            this.cboAlbums.FormattingEnabled = true;
            this.cboAlbums.Location = new System.Drawing.Point(55, 10);
            this.cboAlbums.Name = "cboAlbums";
            this.cboAlbums.Size = new System.Drawing.Size(147, 21);
            this.cboAlbums.TabIndex = 31;
            this.cboAlbums.SelectedIndexChanged += new System.EventHandler(this.cboAlbums_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Album:";
            // 
            // fbdExport
            // 
            this.fbdExport.RootFolder = System.Environment.SpecialFolder.MyPictures;
            // 
            // PhotoAlbum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboAlbums);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lbnext);
            this.Controls.Add(this.lbPrevious);
            this.Controls.Add(this.lblPhotoCount);
            this.Controls.Add(this.pbPhoto);
            this.Name = "PhotoAlbum";
            this.Size = new System.Drawing.Size(310, 264);
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Label lblPhotoCount;
        private System.Windows.Forms.LinkLabel lbPrevious;
        private System.Windows.Forms.LinkLabel lbnext;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.ComboBox cboAlbums;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbdExport;
    }
}
