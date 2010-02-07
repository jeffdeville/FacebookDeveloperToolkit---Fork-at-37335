namespace Facebook.Winforms.Forms
{
    partial class FacebookExtendedPermission
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbFacebookLogin = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbFacebookLogin
            // 
            this.wbFacebookLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbFacebookLogin.Location = new System.Drawing.Point(0, 0);
            this.wbFacebookLogin.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbFacebookLogin.Name = "wbFacebookLogin";
            this.wbFacebookLogin.ScrollBarsEnabled = false;
            this.wbFacebookLogin.Size = new System.Drawing.Size(802, 617);
            this.wbFacebookLogin.TabIndex = 0;
            this.wbFacebookLogin.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbFacebookLogin_Navigated);
            // 
            // FacebookExtendedPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 617);
            this.Controls.Add(this.wbFacebookLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FacebookExtendedPermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Facebook Extended Permission";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FacebookAuthentication_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbFacebookLogin;
    }
}