namespace Facebook.Winforms
{
    partial class EventList
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
            this.lblEventCount = new System.Windows.Forms.Label();
            this.flpEvents = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblEventCount
            // 
            this.lblEventCount.AutoSize = true;
            this.lblEventCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEventCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(89)))), ((int)(((byte)(152)))));
            this.lblEventCount.Location = new System.Drawing.Point(0, 0);
            this.lblEventCount.Name = "lblEventCount";
            this.lblEventCount.Size = new System.Drawing.Size(74, 13);
            this.lblEventCount.TabIndex = 0;
            this.lblEventCount.Text = "lblEventCount";
            // 
            // flpEvents
            // 
            this.flpEvents.AutoScroll = true;
            this.flpEvents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.flpEvents.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpEvents.Location = new System.Drawing.Point(0, 13);
            this.flpEvents.Name = "flpEvents";
            this.flpEvents.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.flpEvents.Size = new System.Drawing.Size(353, 229);
            this.flpEvents.TabIndex = 1;
            // 
            // EventList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flpEvents);
            this.Controls.Add(this.lblEventCount);
            this.Name = "EventList";
            this.Size = new System.Drawing.Size(353, 242);
            this.Load += new System.EventHandler(this.EventList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEventCount;
        private System.Windows.Forms.FlowLayoutPanel flpEvents;
    }
}
