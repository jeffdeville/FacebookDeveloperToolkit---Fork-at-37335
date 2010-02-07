namespace Facebook.Winforms
{
    partial class FriendMap
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
            this.virtualEarthBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // virtualEarthBrowser
            // 
            this.virtualEarthBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.virtualEarthBrowser.Location = new System.Drawing.Point(0, 0);
            this.virtualEarthBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.virtualEarthBrowser.Name = "virtualEarthBrowser";
            this.virtualEarthBrowser.ScrollBarsEnabled = false;
            this.virtualEarthBrowser.Size = new System.Drawing.Size(700, 470);
            this.virtualEarthBrowser.TabIndex = 0;
            // 
            // FriendMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.virtualEarthBrowser);
            this.Name = "FriendMap";
            this.Size = new System.Drawing.Size(700, 470);
            this.Load += new System.EventHandler(this.FriendMap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser virtualEarthBrowser;
    }
}
