namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays a list of friends
    /// </summary>
    partial class FriendList
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
			this.lblFriendCount = new System.Windows.Forms.Label();
			this.flpFriends = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// lblFriendCount
			// 
			this.lblFriendCount.AutoSize = true;
			this.lblFriendCount.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFriendCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFriendCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(89)))), ((int)(((byte)(152)))));
			this.lblFriendCount.Location = new System.Drawing.Point(0, 0);
			this.lblFriendCount.Name = "lblFriendCount";
			this.lblFriendCount.Size = new System.Drawing.Size(76, 13);
			this.lblFriendCount.TabIndex = 0;
			this.lblFriendCount.Text = "lblFriendCount";
			// 
			// flpFriends
			// 
			this.flpFriends.AutoScroll = true;
			this.flpFriends.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flpFriends.BackColor = System.Drawing.Color.White;
			this.flpFriends.Dock = System.Windows.Forms.DockStyle.Left;
			this.flpFriends.Location = new System.Drawing.Point(0, 13);
			this.flpFriends.Name = "flpFriends";
			this.flpFriends.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.flpFriends.Size = new System.Drawing.Size(373, 229);
			this.flpFriends.TabIndex = 1;
			// 
			// FriendList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.flpFriends);
			this.Controls.Add(this.lblFriendCount);
			this.Name = "FriendList";
			this.Size = new System.Drawing.Size(374, 242);
			this.Load += new System.EventHandler(this.FriendList_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFriendCount;
        private System.Windows.Forms.FlowLayoutPanel flpFriends;
    }
}
