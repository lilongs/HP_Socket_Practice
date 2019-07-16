using System;
using System.Text;
using System.Windows.Forms;
using HPSocketCS;

namespace TCP_Pack_Server
{
    //表示应用程序状态的枚举
    public enum AppState
    {
        Starting, Started, Stopping, Stoped, Error
    }

    public partial class TCP_Pack_Server : Form
    {
        private AppState appState = AppState.Stoped;

        //PACK模型，应用程序不必处理分包与数据抓取，HP-Socket组件保证每个OnReceive事件都向应用程序提供一个完整的数据包
        //PACK模型组件会对应用程序发送的每个数据包自动加上4字节（32位）的包头，组件接收到数据时根据包头信息自动分包，每个完整数据包通过OnReceive事件发送给应用程序
        private HPSocketCS.TcpPackServer server = new HPSocketCS.TcpPackServer();

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

        //操作checkedListBoxClientList的方法
        private void CheckedListBoxOperation(string connId, string operationType)
        {
            checkedListBoxClientList.BeginInvoke((MethodInvoker)delegate
            {
                switch (operationType)
                {
                    case "add":
                        {
                            checkedListBoxClientList.Items.Add(connId);
                        }
                        break;

                    case "remove":
                        {
                            checkedListBoxClientList.Items.Remove(connId);
                        }
                        break;
                }
            });
        }

        public TCP_Pack_Server()
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
                buttonStart.Enabled = (appState == AppState.Stoped);
                buttonStop.Enabled = (appState == AppState.Started);
                textBoxSendMsg.Enabled = (appState == AppState.Started);
                buttonSend.Enabled = (appState == AppState.Started);
            });
        }

        private void TCP_Pack_Server_Load(object sender, EventArgs e)
        {
            appState = AppState.Stoped;
            SetControlState();

            //绑定事件
            //绑定监听地址前触发
            server.OnPrepareListen += new TcpServerEvent.OnPrepareListenEventHandler(server_OnPrepareListen);
            //客户端连接请求被接受后触发
            server.OnAccept += new TcpServerEvent.OnAcceptEventHandler(server_OnAccept);
            //发送消息后触发
            server.OnSend += new TcpServerEvent.OnSendEventHandler(server_OnSend);
            //收到消息后触发
            server.OnReceive += new TcpServerEvent.OnReceiveEventHandler(server_OnReceive);
            //连接关闭后触发（服务端的连接通常是多个，只要某一个连接关闭了都会触发）
            server.OnClose += new TcpServerEvent.OnCloseEventHandler(server_OnClose);
            //组件停止后触发
            server.OnShutdown += new TcpServerEvent.OnShutdownEventHandler(server_OnShutdown);

            //PACK模型包头格式
            //XXXXXXXXXXXXX YYYYYYYYYYYYYYYYYYY
            //前13位为包头标识，用于数据包校验，取值范围为0-8191（ox1FFF）,当包头标识为0时不校验包头
            //后19位为长度，记录包体长度。有效数据包最大长度不能超过524287（ox7FFFF）字节，默认长度限制为262144（ox40000）字节
            //设置包头标识，客户端与服务端的包头标识一致才能通信
            server.PackHeaderFlag = 0xff;
            //设置包体长度
            server.MaxPackSize = 0x1000;
        }

        //socket参数是当前连接的socket句柄
        //事件处理方法的返回值如果为HandleResult.Error，HP-Socket组件会立即中断连接
        //不能在事件处理方法中调用Start()和Stop()方法
        //所有的事件处理方法都是在非UI线程，不能直接在事件处理方法中更新UI，需要用到委托

        #region 事件处理方法

        private HandleResult server_OnPrepareListen(IntPtr soListen)
        {
            ShowMSG("开始监听");

            return HandleResult.Ok;
        }

        private HandleResult server_OnAccept(IntPtr connId, IntPtr pClient)
        {
            ShowMSG(string.Format("接受客户端连接请求，连接ID：{0}", connId));
            string strConnID = connId.ToString();
            if (checkedListBoxClientList.Items.Contains(strConnID) == false)
            {
                CheckedListBoxOperation(connId.ToString(), "add");
            }

            return HandleResult.Ok;
        }

        private HandleResult server_OnSend(IntPtr connId, byte[] bytes)
        {
            ShowMSG(string.Format("向连接ID：{0} 的客户端发送信息成功，长度：{1}", connId, bytes.Length));

            return HandleResult.Ok;
        }

        private HandleResult server_OnReceive(IntPtr connId, byte[] bytes)
        {
            //string recievedStr = Encoding.Default.GetString(bytes);
            string recievedStr = Encoding.GetEncoding("UTF-8").GetString(bytes);
            ShowMSG(string.Format("收到连接ID：{0} 的信息，内容：{1}，长度：{2}", connId, recievedStr, bytes.Length));

            return HandleResult.Ok;
        }

        //当触发了OnClose事件时，表示连接已经被关闭，并且OnClose事件只会被触发一次
        //通过errorCode参数判断是正常关闭还是异常关闭，0表示正常关闭
        private HandleResult server_OnClose(IntPtr connId, SocketOperation enOperation, int errorCode)
        {
            CheckedListBoxOperation(connId.ToString(), "remove");

            if (errorCode == 0)
            {
                ShowMSG(string.Format("连接已断开，连接ID：{0}", connId));
            }
            else
            {
                ShowMSG(string.Format("客户端连接发生异常，已经断开连接，连接ID：{0}，错误代码：{1}", connId, errorCode));
            }

            return HandleResult.Ok;
        }

        private HandleResult server_OnShutdown()
        {
            appState = AppState.Stoped;
            SetControlState();

            ShowMSG("服务端已经停止服务");

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

                server.IpAddress = ip;
                server.Port = port;

                if (server.Start())
                {
                    appState = AppState.Started;
                    SetControlState();
                }
                else
                {
                    appState = AppState.Stoped;
                    SetControlState();
                    throw new Exception(string.Format("服务端启动失败：{0}，{1}", server.ErrorMessage, server.ErrorCode));
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

            server.Stop();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (checkedListBoxClientList.Items.Count > 0)
            {
                try
                {
                    for (int i = 0; i < checkedListBoxClientList.Items.Count; i++)
                    {
                        IntPtr connId = (IntPtr)Convert.ToInt32(checkedListBoxClientList.Items[i]);

                        if (checkedListBoxClientList.GetItemChecked(i))
                        {
                            server.Disconnect(connId, true);
                        }
                    }
                }
                catch (Exception exc)
                {
                    ShowMSG(exc.Message);
                }
            }
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxMessage.Items.Clear();
        }

        private void TCP_Pack_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (appState == AppState.Started)
            {
                ShowMSG("请先停止服务");
                e.Cancel = true;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string sendContent = textBoxSendMsg.Text;
            if (sendContent.Length < 1)
            {
                return;
            }
            if (checkedListBoxClientList.Items.Count < 1)
            {
                return;
            }

            try
            {
               // byte[] sendBytes = Encoding.Default.GetBytes(sendContent);
                byte[] sendBytes = Encoding.GetEncoding("UTF-8").GetBytes(sendContent);
                for (int i = 0; i < checkedListBoxClientList.Items.Count; i++)
                {
                    IntPtr connId = (IntPtr)Convert.ToInt32(checkedListBoxClientList.Items[i]);
                    if (checkedListBoxClientList.GetItemChecked(i))
                    {
                        server.Send(connId, sendBytes, sendBytes.Length);
                    }
                }

                textBoxSendMsg.Text = string.Empty;
            }
            catch (Exception exc)
            {
                ShowMSG(string.Format("发送失败：{0}", exc.Message));
            }
        }
    }
}