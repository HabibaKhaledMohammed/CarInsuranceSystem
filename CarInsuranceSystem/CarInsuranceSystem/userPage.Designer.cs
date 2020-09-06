namespace WindowsFormsApplication1
{
    partial class userPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userPage));
            this.chatbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatbutton
            // 
            this.chatbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chatbutton.BackColor = System.Drawing.Color.IndianRed;
            this.chatbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chatbutton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chatbutton.Location = new System.Drawing.Point(301, 85);
            this.chatbutton.Name = "chatbutton";
            this.chatbutton.Size = new System.Drawing.Size(156, 53);
            this.chatbutton.TabIndex = 1;
            this.chatbutton.Text = "Chat";
            this.chatbutton.UseVisualStyleBackColor = false;
            this.chatbutton.Click += new System.EventHandler(this.chatbutton_Click);
            // 
            // userPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(762, 567);
            this.ControlBox = false;
            this.Controls.Add(this.chatbutton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "userPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button chatbutton;
    }
}