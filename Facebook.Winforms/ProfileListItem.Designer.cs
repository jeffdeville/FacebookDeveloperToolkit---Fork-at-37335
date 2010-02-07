namespace Facebook.Winforms
{
    partial class ProfileListItem
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
            this.pbProfilePicture = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblNeworkPrompt = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.LinkLabel();
            this.lblNetworks = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // pbProfilePicture
            // 
            this.pbProfilePicture.Location = new System.Drawing.Point(7, 7);
            this.pbProfilePicture.Name = "pbProfilePicture";
            this.pbProfilePicture.Size = new System.Drawing.Size(100, 95);
            this.pbProfilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbProfilePicture.TabIndex = 23;
            this.pbProfilePicture.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Gray;
            this.label17.Location = new System.Drawing.Point(124, 7);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "Name:";
            // 
            // lblNeworkPrompt
            // 
            this.lblNeworkPrompt.AutoSize = true;
            this.lblNeworkPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNeworkPrompt.ForeColor = System.Drawing.Color.Gray;
            this.lblNeworkPrompt.Location = new System.Drawing.Point(124, 23);
            this.lblNeworkPrompt.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblNeworkPrompt.Name = "lblNeworkPrompt";
            this.lblNeworkPrompt.Size = new System.Drawing.Size(51, 13);
            this.lblNeworkPrompt.TabIndex = 25;
            this.lblNeworkPrompt.Text = "Network:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lblName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblName.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lblName.Location = new System.Drawing.Point(186, 7);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 13);
            this.lblName.TabIndex = 26;
            this.lblName.TabStop = true;
            this.lblName.Text = "lblName";
            this.lblName.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(120)))), ((int)(((byte)(205)))));
            this.lblName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblName_LinkClicked);
            // 
            // lblNetworks
            // 
            this.lblNetworks.AutoSize = true;
            this.lblNetworks.ForeColor = System.Drawing.Color.Gray;
            this.lblNetworks.Location = new System.Drawing.Point(186, 23);
            this.lblNetworks.Name = "lblNetworks";
            this.lblNetworks.Size = new System.Drawing.Size(62, 13);
            this.lblNetworks.TabIndex = 28;
            this.lblNetworks.Text = "lblNetworks";
            // 
            // ProfileListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblNetworks);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblNeworkPrompt);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.pbProfilePicture);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.Name = "ProfileListItem";
            this.Size = new System.Drawing.Size(352, 108);
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbProfilePicture;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblNeworkPrompt;
        private System.Windows.Forms.LinkLabel lblName;
        private System.Windows.Forms.Label lblNetworks;
    }
}
