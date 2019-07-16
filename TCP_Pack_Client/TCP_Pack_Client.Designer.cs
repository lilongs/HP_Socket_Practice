namespace TCP_Pack_Client
{
    partial class TCP_Pack_Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCP_Pack_Client));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIPAdress = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAsyncConn = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBoxSendMsg = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务端IP";
            // 
            // textBoxIPAdress
            // 
            this.textBoxIPAdress.Location = new System.Drawing.Point(71, 12);
            this.textBoxIPAdress.Name = "textBoxIPAdress";
            this.textBoxIPAdress.Size = new System.Drawing.Size(100, 21);
            this.textBoxIPAdress.TabIndex = 1;
            this.textBoxIPAdress.Text = "127.0.0.1";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(248, 12);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "7000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务端端口";
            // 
            // checkBoxAsyncConn
            // 
            this.checkBoxAsyncConn.AutoSize = true;
            this.checkBoxAsyncConn.Location = new System.Drawing.Point(354, 14);
            this.checkBoxAsyncConn.Name = "checkBoxAsyncConn";
            this.checkBoxAsyncConn.Size = new System.Drawing.Size(120, 16);
            this.checkBoxAsyncConn.TabIndex = 4;
            this.checkBoxAsyncConn.Text = "是否异步发起连接";
            this.checkBoxAsyncConn.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(624, 11);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "启动";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(705, 11);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxSendMsg
            // 
            this.textBoxSendMsg.Location = new System.Drawing.Point(14, 540);
            this.textBoxSendMsg.Name = "textBoxSendMsg";
            this.textBoxSendMsg.Size = new System.Drawing.Size(685, 21);
            this.textBoxSendMsg.TabIndex = 8;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(705, 539);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 9;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.ContextMenuStrip = this.contextMenuStrip1;
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.ItemHeight = 12;
            this.listBoxMessage.Location = new System.Drawing.Point(12, 39);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.Size = new System.Drawing.Size(768, 496);
            this.listBoxMessage.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "清空";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // TCP_Pack_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.listBoxMessage);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxSendMsg);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.checkBoxAsyncConn);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxIPAdress);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TCP_Pack_Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP_Pack_Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCP_Pack_Client_FormClosing);
            this.Load += new System.EventHandler(this.TCP_Pack_Client_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIPAdress;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAsyncConn;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxSendMsg;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

