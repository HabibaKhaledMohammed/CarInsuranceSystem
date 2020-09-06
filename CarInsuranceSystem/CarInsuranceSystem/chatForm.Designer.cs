namespace CarInsuranceSystem
{
    partial class chatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chatForm));
            this.imageShow = new System.Windows.Forms.Panel();
            this.Send_button = new System.Windows.Forms.Button();
            this.Message_text = new System.Windows.Forms.TextBox();
            this.friendsList = new System.Windows.Forms.ListBox();
            this.messageList = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // imageShow
            // 
            this.imageShow.AutoScroll = true;
            this.imageShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.imageShow.Location = new System.Drawing.Point(170, 0);
            this.imageShow.Name = "imageShow";
            this.imageShow.Size = new System.Drawing.Size(53, 502);
            this.imageShow.TabIndex = 21;
            // 
            // Send_button
            // 
            this.Send_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Send_button.BackgroundImage")));
            this.Send_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Send_button.Location = new System.Drawing.Point(786, 407);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(100, 58);
            this.Send_button.TabIndex = 19;
            this.Send_button.UseVisualStyleBackColor = true;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // Message_text
            // 
            this.Message_text.Location = new System.Drawing.Point(227, 407);
            this.Message_text.Multiline = true;
            this.Message_text.Name = "Message_text";
            this.Message_text.Size = new System.Drawing.Size(553, 58);
            this.Message_text.TabIndex = 18;
            // 
            // friendsList
            // 
            this.friendsList.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.friendsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.friendsList.Font = new System.Drawing.Font("Ravie", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.friendsList.ForeColor = System.Drawing.Color.Gray;
            this.friendsList.FormattingEnabled = true;
            this.friendsList.ItemHeight = 19;
            this.friendsList.Items.AddRange(new object[] {
            "friends"});
            this.friendsList.Location = new System.Drawing.Point(0, 0);
            this.friendsList.Name = "friendsList";
            this.friendsList.Size = new System.Drawing.Size(172, 502);
            this.friendsList.TabIndex = 17;
            this.friendsList.SelectedIndexChanged += new System.EventHandler(this.friendsList_SelectedIndexChanged);
            // 
            // messageList
            // 
            this.messageList.Location = new System.Drawing.Point(229, 27);
            this.messageList.MinimumSize = new System.Drawing.Size(20, 20);
            this.messageList.Name = "messageList";
            this.messageList.Size = new System.Drawing.Size(657, 349);
            this.messageList.TabIndex = 22;
            this.messageList.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.messageList_DocumentCompleted);
            // 
            // chatForm
            // 
            this.AcceptButton = this.Send_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 502);
            this.Controls.Add(this.messageList);
            this.Controls.Add(this.imageShow);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.Message_text);
            this.Controls.Add(this.friendsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "chatForm";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.chatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel imageShow;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.TextBox Message_text;
        private System.Windows.Forms.ListBox friendsList;
        private System.Windows.Forms.WebBrowser messageList;
    }
}