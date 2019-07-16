namespace TCP_Pack_Server
{
    partial class TCP_Pack_Server
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCP_Pack_Server));
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIPAdress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxClientList = new System.Windows.Forms.CheckedListBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxSendMsg = new System.Windows.Forms.TextBox();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(705, 5);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 12;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(624, 5);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "启动";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(248, 6);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 10;
            this.textBoxPort.Text = "7000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "服务端端口";
            // 
            // textBoxIPAdress
            // 
            this.textBoxIPAdress.Location = new System.Drawing.Point(71, 6);
            this.textBoxIPAdress.Name = "textBoxIPAdress";
            this.textBoxIPAdress.Size = new System.Drawing.Size(100, 21);
            this.textBoxIPAdress.TabIndex = 8;
            this.textBoxIPAdress.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "服务端IP";
            // 
            // checkedListBoxClientList
            // 
            this.checkedListBoxClientList.CheckOnClick = true;
            this.checkedListBoxClientList.FormattingEnabled = true;
            this.checkedListBoxClientList.Location = new System.Drawing.Point(14, 33);
            this.checkedListBoxClientList.Name = "checkedListBoxClientList";
            this.checkedListBoxClientList.Size = new System.Drawing.Size(200, 500);
            this.checkedListBoxClientList.TabIndex = 14;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(705, 539);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 16;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxSendMsg
            // 
            this.textBoxSendMsg.Location = new System.Drawing.Point(14, 540);
            this.textBoxSendMsg.Name = "textBoxSendMsg";
            this.textBoxSendMsg.Size = new System.Drawing.Size(685, 21);
            this.textBoxSendMsg.TabIndex = 15;
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.ContextMenuStrip = this.contextMenuStrip1;
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.ItemHeight = 12;
            this.listBoxMessage.Location = new System.Drawing.Point(220, 33);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.Size = new System.Drawing.Size(560, 496);
            this.listBoxMessage.TabIndex = 17;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(468, 5);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(150, 23);
            this.buttonDisconnect.TabIndex = 18;
            this.buttonDisconnect.Text = "断开所选择客户端的连接";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // TCP_Pack_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.listBoxMessage);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxSendMsg);
            this.Controls.Add(this.checkedListBoxClientList);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxIPAdress);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TCP_Pack_Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP_Pack_Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCP_Pack_Server_FormClosing);
            this.Load += new System.EventHandler(this.TCP_Pack_Server_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIPAdress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBoxClientList;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxSendMsg;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
    }
}

