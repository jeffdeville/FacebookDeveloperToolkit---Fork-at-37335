namespace Facebook.Winforms.Forms
{
    partial class RequestSelection
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
            this.wbRequest = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbRequest
            // 
            this.wbRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbRequest.Location = new System.Drawing.Point(0, 0);
            this.wbRequest.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbRequest.Name = "wbRequest";
            this.wbRequest.Size = new System.Drawing.Size(798, 633);
            this.wbRequest.TabIndex = 0;
            this.wbRequest.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbRequest_Navigating);
            // 
            // RequestSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 633);
            this.Controls.Add(this.wbRequest);
            this.Name = "RequestSelection";
            this.Text = "Send Requests";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbRequest;
    }
}