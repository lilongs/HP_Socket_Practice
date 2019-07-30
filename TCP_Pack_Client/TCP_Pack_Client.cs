using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HPSocketCS;

namespace TCP_Pack_Client
{
    //表示应用程序状态的枚举
    public enum AppState
    {
        Starting, Started, Stopping, Stoped, Error
    }

    public partial class TCP_Pack_Client : Form
    {
        public Dictionary<string, string> EquipmentInfo = new Dictionary<string, string>()
        {
            { "A8001","WS1"},
            { "A8002","WS2"},
            { "A8003","WS3"},
            { "A8004","WS4"},
            { "A8005","Run-In"},
            { "A8006","AC01"},
            { "A8007","AC02"},
            { "A8008","FC01"},
            { "A8009","FC02"},
            { "A8010","CC"}
        };
        private AppState appState = AppState.Stoped;

        //PACK模型，应用程序不必处理分包与数据抓取，HP-Socket组件保证每个OnReceive事件都向应用程序提供一个完整的数据包
        //PACK模型组件会对应用程序发送的每个数据包自动加上4字节（32位）的包头，组件接收到数据时根据包头信息自动分包，每个完整数据包通过OnReceive事件发送给应用程序
        //private HPSocketCS.TcpPackClient client = new HPSocketCS.TcpPackClient();
        private HPSocketCS.TcpClient client = new HPSocketCS.TcpClient();

        //将信息显示到UI线程的方法
        private void ShowMSG(string msg)
        {
            listBoxMessage.BeginInvoke((MethodInvoker)delegate
            {
                if (listBoxMessage.Items.Count > 200)
                {
                    listBoxMessage.Items.RemoveAt(0);
                }
                listBoxMessage.Items.Add(msg);
            });
        }

        public TCP_Pack_Client()
        {
            InitializeComponent();
        }

        //根据AppState设置界面控件相关状态的方法
        private void SetControlState()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                textBoxIPAdress.Enabled = (appState == AppState.Stoped);
                textBoxPort.Enabled = (appState == AppState.Stoped);
                checkBoxAsyncConn.Enabled = (appState == AppState.Stoped);
                buttonStart.Enabled = (appState == AppState.Stoped);
                buttonStop.Enabled = (appState == AppState.Started);
                textBoxSendMsg.Enabled = (appState == AppState.Started);
                buttonSend.Enabled = (appState == AppState.Started);
            });
        }

        private void TCP_Pack_Client_Load(object sender, EventArgs e)
        {
            appState = AppState.Stoped;
            SetControlState();

            //绑定事件
            //开始连接前触发
            client.OnPrepareConnect += new TcpClientEvent.OnPrepareConnectEventHandler(client_OnPrepareConnect);
            //连接成功后触发
            client.OnConnect += new TcpClientEvent.OnConnectEventHandler(client_OnConnect);
            //发送消息后触发
            client.OnSend += new TcpClientEvent.OnSendEventHandler(client_OnSend);
            //收到消息后触发
            client.OnReceive += new TcpClientEvent.OnReceiveEventHandler(client_OnReceive);
            //连接关闭后触发
            client.OnClose += new TcpClientEvent.OnCloseEventHandler(client_OnClose);

            //PACK模型包头格式
            //XXXXXXXXXXXXX YYYYYYYYYYYYYYYYYYY
            //前13位为包头标识，用于数据包校验，取值范围为0-8191（ox1FFF）,当包头标识为0时不校验包头
            //后19位为长度，记录包体长度。有效数据包最大长度不能超过524287（ox7FFFF）字节，默认长度限制为262144（ox40000）字节
            //设置包头标识，客户端与服务端的包头标识一致才能通信
            //client.PackHeaderFlag = 0xff;
            //设置包体长度
            //client.MaxPackSize = 0x1000;
        }

        //socket参数是当前连接的socket句柄
        //事件处理方法的返回值如果为HandleResult.Error，HP-Socket组件会立即中断连接
        //不能在事件处理方法中调用Start()和Stop()方法
        //所有的事件处理方法都是在非UI线程，不能直接在事件处理方法中更新UI，需要用到委托

        #region 事件处理方法

        private HandleResult client_OnPrepareConnect(TcpClient sender, IntPtr socket)
        {
            ShowMSG(string.Format("正在连接服务端，socket句柄为：{0}", socket.ToString()));

            return HandleResult.Ok;
        }

        private HandleResult client_OnConnect(TcpClient sender)
        {
            //如果是异步连接，更新控件状态
            if (checkBoxAsyncConn.Checked == true)
            {
                appState = AppState.Started;
                SetControlState();
            }

            ShowMSG("连接服务端成功");

            return HandleResult.Ok;
        }

        private HandleResult client_OnSend(TcpClient sender, byte[] bytes)
        {
            ShowMSG(string.Format("信息发送成功，长度：{0}", bytes.Length));

            return HandleResult.Ok;
        }

        private HandleResult client_OnReceive(TcpClient sender, byte[] bytes)
        {
            //string recievedStr = Encoding.Default.GetString(bytes);
            string recievedStr = Encoding.GetEncoding("UTF-8").GetString(bytes);

            ShowMSG(string.Format("收到信息，内容：{0}，长度：{1}", recievedStr, bytes.Length));

            return HandleResult.Ok;
        }

        //当触发了OnClose事件时，表示连接已经被关闭，并且OnClose事件只会被触发一次
        //通过errorCode参数判断是正常关闭还是异常关闭，0表示正常关闭
        private HandleResult client_OnClose(TcpClient sender, SocketOperation enOperation, int errorCode)
        {
            appState = AppState.Stoped;
            SetControlState();

            if (errorCode == 0)
            {
                ShowMSG("连接已关闭");
            }
            else
            {
                ShowMSG(string.Format("连接异常关闭：{0}，{1}", client.ErrorMessage, client.ErrorCode));
            }

            return HandleResult.Ok;
        }

        #endregion 事件处理方法

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = textBoxIPAdress.Text.Trim();
                ushort port = ushort.Parse(textBoxPort.Text.Trim());

                appState = AppState.Starting;

                //Client组件发起连接的过程可以是同步或异步的
                //同步是指组件的连接方法等到连接成功或失败了再返回（返回true或false）
                //异步是指组件的连接方法会立即返回，如果返回值为false则表示连接失败，如果连接成功则稍后会触发OnConnect事件
                if (client.Connect(ip, port, checkBoxAsyncConn.Checked))
                {
                    if (checkBoxAsyncConn.Checked == false)
                    {
                        appState = AppState.Started;
                        SetControlState();
                    }
                }
                else
                {
                    appState = AppState.Stoped;
                    SetControlState();
                    throw new Exception(string.Format("无法建立连接：{0}，{1}", client.ErrorMessage, client.ErrorCode));
                }
            }
            catch (Exception exc)
            {
                ShowMSG(exc.Message);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            appState = AppState.Stopping;

            client.Stop();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string sendContent = textBoxSendMsg.Text;
            if (sendContent.Length < 1)
            {
                return;
            }

            try
            {
                //byte[] sendBytes = Encoding.Default.GetBytes(sendContent);
                byte[] sendBytes = Encoding.GetEncoding("UTF-8").GetBytes(sendContent);
                client.Send(sendBytes, sendBytes.Length);
                textBoxSendMsg.Text = string.Empty;
            }
            catch (Exception exc)
            {
                ShowMSG(string.Format("发送失败：{0}", exc.Message));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBoxMessage.Items.Clear();
        }

        private void TCP_Pack_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (appState == AppState.Started)
            {
                ShowMSG("请先关闭连接");
                e.Cancel = true;
            }
        }
        Thread thread;
        ManualResetEvent ma;
        bool on_off = false;
        bool stop = false;

        private void btn_Test_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            btn_Test.Enabled = false;
            //批量测试数据写入          
            thread = new Thread(TestStep);//通过ThreadStart委托告诉子线程执行什么方法　
            thread.IsBackground=true;
            thread.Start();
        }

        private void TestStep()
        {
            string productno = string.Empty;
            string equipment = string.Empty;
            int times = 0;

            this.BeginInvoke((MethodInvoker)delegate
            {
                if (String.IsNullOrEmpty(this.comboBox1.Text))
                {
                    MessageBox.Show("请选择站点名称");
                    return;
                }
                if (String.IsNullOrEmpty(this.txtProductno.Text))
                {
                    MessageBox.Show("请输入写入测试产品编码");
                    return;
                }
                if (String.IsNullOrEmpty(this.txtTimes.Text))
                {
                    MessageBox.Show("请输入写入测试次数");
                    return;
                }
                productno = this.txtProductno.Text;
                equipment= this.comboBox1.Text;
                times = Convert.ToInt32(this.txtTimes.Text);

                for (int i = 0; i < times; i++)
                {
                    if (stop)
                        return;
                    if (on_off)
                    {
                        ma = new ManualResetEvent(false);
                        ma.WaitOne();
                    }

                    string sendContent = string.Empty;
                    byte[] sendBytes = new byte[] { };
                    string product = productno + i.ToString("0000");
                    //建立握手
                    sendContent = "<STX>Process_ID," + equipment + ",null<ETX>";
                    sendBytes = Encoding.GetEncoding("UTF-8").GetBytes(sendContent);
                    client.Send(sendBytes, sendBytes.Length);
                    Thread.Sleep(1000);

                    //产品进站
                    sendContent = "<STX>Process_IN," + equipment + ",null," + product + "<ETX>";
                    sendBytes = Encoding.GetEncoding("UTF-8").GetBytes(sendContent);
                    client.Send(sendBytes, sendBytes.Length);
                    Thread.Sleep(1000);

                    //产品出站
                    sendContent = "<STX>Process_OUT," + equipment + ",null," + product + ",pass,TestItem1=2.3,TestItem4=6.2,2<ETX>";
                    sendBytes = Encoding.GetEncoding("UTF-8").GetBytes(sendContent);
                    client.Send(sendBytes, sendBytes.Length);
                    Thread.Sleep(1000);

                    //产品测试出站
                    sendContent = @"<STX>START_OUT," + equipment + ",null," + product + ",pass,<?xml version=\"1.0\" encoding=\"utf - \"?>" +
                            "< Cluster SERIALNR = \"A2C53112763\" TYPEFAMILY = \"F3XBASE\" TYPECODE = \"A2C53112763\" TESTMODE = \"AUTO\" TESTRESULT = \"2\" >" +
                            "< Teststep StepNo = \"10000010\" StepName = \"Open AIDA\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.03\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"00000000\" StepName = \"Set power\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.16\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"00000001\" StepName = \"Power On\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.17\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"12000023\" StepName = \"Power Switch ON\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.14\" TestResult = \"0\" />" +
                            "< Teststep StepNo = \"50000010\" StepName = \"Delay 2s\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"2.02\" TestResult = \"0\" />" +
                            "< Teststep StepNo = \"10000030\" StepName = \"Enter to diagnostic\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"2.38\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"60000001\" StepName = \"5.7.1\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0\" TestResult = \"0\" />" +
                            "< Teststep StepNo = \"57120001\" StepName = \"read the Visteon product partnumber from the product NVM\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.27\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"57120002\" StepName = \"read the Serial Number\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.26\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"60000002\" StepName = \"5.7.2\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0\" TestResult = \"0\" />" +
                            "< Teststep StepNo = \"57220000\" StepName = \"Read Traceability\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.27\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"60000003\" StepName = \"5.7.3\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0\" TestResult = \"0\" />" +
                            "< Teststep StepNo = \"57300000\" StepName = \"Reset Fazit Traceability\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.41\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"60000012\" StepName = \"5.5.20\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.05\" TestResult = \"0\" />" +
                            "< Teststep StepNo = \"21000002\" StepName = \"Generate Sine 1KHZ\" Value = \"0\" UPPERLIMIT = \"0\" LOWERLIMIT = \"0\" Unit = \"\" Duration = \"0.03\" TestResult = \"1\" />" +
                            "< Teststep StepNo = \"55200001\" StepName = \"Speaker audio Freq test\" Value = \"0.95292561040690982\" UPPERLIMIT = \"1050\" LOWERLIMIT = \"950\" Unit = \"\" Duration = \"1.49\" TestResult = \"2\" />" +
                            "</ Cluster >< ETX>";
                    sendBytes = Encoding.GetEncoding("UTF-8").GetBytes(sendContent);
                    client.Send(sendBytes, sendBytes.Length);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        btnCounts.Text = i.ToString("0000");
                    });
                    Thread.Sleep(1000);
                }

            });            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {            
            if (this.btnPause.Text == "暂停")
            {
                stop = true;
                this.btnPause.Text = "继续";
            }
            else
            {
                stop = false;
                this.btnPause.Text = "暂停";
            }               
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stop = true;
            btn_Test.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}