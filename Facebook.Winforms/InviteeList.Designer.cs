namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays a list of users invited to an event
    /// </summary>
    partial class InviteeList
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
            this.lblInviteeCount = new System.Windows.Forms.Label();
            this.flpInvitees = new System.Windows.Forms.FlowLayoutPanel();
            this.rbAccepted = new System.Windows.Forms.RadioButton();
            this.rbUnsure = new System.Windows.Forms.RadioButton();
            this.rbDeclined = new System.Windows.Forms.RadioButton();
            this.rbNoReply = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInviteeCount
            // 
            this.lblInviteeCount.AutoSize = true;
            this.lblInviteeCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInviteeCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInviteeCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(89)))), ((int)(((byte)(152)))));
            this.lblInviteeCount.Location = new System.Drawing.Point(0, 0);
            this.lblInviteeCount.Name = "lblInviteeCount";
            this.lblInviteeCount.Size = new System.Drawing.Size(76, 13);
            this.lblInviteeCount.TabIndex = 0;
            this.lblInviteeCount.Text = "lblnviteeCount";
            // 
            // flpInvitees
            // 
            this.flpInvitees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flpInvitees.AutoScroll = true;
            this.flpInvitees.BackColor = System.Drawing.Color.White;
            this.flpInvitees.Location = new System.Drawing.Point(0, 67);
            this.flpInvitees.Name = "flpInvitees";
            this.flpInvitees.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.flpInvitees.Size = new System.Drawing.Size(422, 175);
            this.flpInvitees.TabIndex = 1;
            // 
            // rbAccepted
            // 
            this.rbAccepted.AutoSize = true;
            this.rbAccepted.Checked = true;
            this.rbAccepted.Location = new System.Drawing.Point(6, 19);
            this.rbAccepted.Name = "rbAccepted";
            this.rbAccepted.Size = new System.Drawing.Size(71, 17);
            this.rbAccepted.TabIndex = 2;
            this.rbAccepted.TabStop = true;
            this.rbAccepted.Text = "Accepted";
            this.rbAccepted.UseVisualStyleBackColor = true;
            this.rbAccepted.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbUnsure
            // 
            this.rbUnsure.AutoSize = true;
            this.rbUnsure.Location = new System.Drawing.Point(83, 19);
            this.rbUnsure.Name = "rbUnsure";
            this.rbUnsure.Size = new System.Drawing.Size(59, 17);
            this.rbUnsure.TabIndex = 4;
            this.rbUnsure.Text = "Unsure";
            this.rbUnsure.UseVisualStyleBackColor = true;
            this.rbUnsure.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbDeclined
            // 
            this.rbDeclined.AutoSize = true;
            this.rbDeclined.Location = new System.Drawing.Point(148, 19);
            this.rbDeclined.Name = "rbDeclined";
            this.rbDeclined.Size = new System.Drawing.Size(67, 17);
            this.rbDeclined.TabIndex = 5;
            this.rbDeclined.Text = "Declined";
            this.rbDeclined.UseVisualStyleBackColor = true;
            this.rbDeclined.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbNoReply
            // 
            this.rbNoReply.AutoSize = true;
            this.rbNoReply.Location = new System.Drawing.Point(221, 19);
            this.rbNoReply.Name = "rbNoReply";
            this.rbNoReply.Size = new System.Drawing.Size(69, 17);
            this.rbNoReply.TabIndex = 6;
            this.rbNoReply.Text = "No Reply";
            this.rbNoReply.UseVisualStyleBackColor = true;
            this.rbNoReply.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNoReply);
            this.groupBox1.Controls.Add(this.rbAccepted);
            this.groupBox1.Controls.Add(this.rbDeclined);
            this.groupBox1.Controls.Add(this.rbUnsure);
            this.groupBox1.Location = new System.Drawing.Point(3, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 45);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display Mode";
            // 
            // InviteeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flpInvitees);
            this.Controls.Add(this.lblInviteeCount);
            this.Name = "InviteeList";
            this.Size = new System.Drawing.Size(425, 242);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInviteeCount;
        private System.Windows.Forms.FlowLayoutPanel flpInvitees;
        private System.Windows.Forms.RadioButton rbAccepted;
        private System.Windows.Forms.RadioButton rbUnsure;
        private System.Windows.Forms.RadioButton rbDeclined;
        private System.Windows.Forms.RadioButton rbNoReply;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
