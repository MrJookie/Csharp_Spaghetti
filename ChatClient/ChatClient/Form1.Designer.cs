namespace ChatClient
{
    partial class Form1
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
            this.textBox_messages = new System.Windows.Forms.TextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.textBox_nickname = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.textBox_ipaddr = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_connected_users = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_messages
            // 
            this.textBox_messages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_messages.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_messages.Location = new System.Drawing.Point(12, 90);
            this.textBox_messages.Multiline = true;
            this.textBox_messages.Name = "textBox_messages";
            this.textBox_messages.ReadOnly = true;
            this.textBox_messages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_messages.Size = new System.Drawing.Size(540, 238);
            this.textBox_messages.TabIndex = 5;
            // 
            // textBox_message
            // 
            this.textBox_message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_message.Location = new System.Drawing.Point(12, 334);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(584, 20);
            this.textBox_message.TabIndex = 6;
            // 
            // textBox_nickname
            // 
            this.textBox_nickname.Location = new System.Drawing.Point(12, 51);
            this.textBox_nickname.MaxLength = 10;
            this.textBox_nickname.Name = "textBox_nickname";
            this.textBox_nickname.Size = new System.Drawing.Size(157, 20);
            this.textBox_nickname.TabIndex = 3;
            this.textBox_nickname.Text = "nickname";
            this.textBox_nickname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(174, 50);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(70, 22);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(602, 333);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 22);
            this.button_send.TabIndex = 7;
            this.button_send.Text = "send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // textBox_ipaddr
            // 
            this.textBox_ipaddr.Location = new System.Drawing.Point(12, 24);
            this.textBox_ipaddr.MaxLength = 15;
            this.textBox_ipaddr.Name = "textBox_ipaddr";
            this.textBox_ipaddr.Size = new System.Drawing.Size(156, 20);
            this.textBox_ipaddr.TabIndex = 1;
            this.textBox_ipaddr.Text = "127.0.0.1";
            this.textBox_ipaddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(174, 24);
            this.textBox_port.MaxLength = 5;
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(70, 20);
            this.textBox_port.TabIndex = 2;
            this.textBox_port.Text = "9147";
            this.textBox_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_connected_users
            // 
            this.textBox_connected_users.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_connected_users.Location = new System.Drawing.Point(558, 90);
            this.textBox_connected_users.Multiline = true;
            this.textBox_connected_users.Name = "textBox_connected_users";
            this.textBox_connected_users.ReadOnly = true;
            this.textBox_connected_users.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_connected_users.Size = new System.Drawing.Size(119, 237);
            this.textBox_connected_users.TabIndex = 8;
            this.textBox_connected_users.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 368);
            this.Controls.Add(this.textBox_connected_users);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ipaddr);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_nickname);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.textBox_messages);
            this.Name = "Form1";
            this.Text = "Chattor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_messages;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.TextBox textBox_nickname;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox textBox_ipaddr;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_connected_users;
    }
}

