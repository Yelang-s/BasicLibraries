namespace BasicLibraries.CommonControl.TCP.Server
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    public class DataEventArgs : EventArgs
    {
        public string ServerIP { get; set; }
        public string ClientIP { get; set; }
        public string Data { get; set; }
    }

    /// <summary>
    /// 异步服务器
    /// </summary>
    public class TCPAsyncServer
    {
        public delegate void ServerCreatedEventHandler(object sender, DataEventArgs data);
        /// <summary>
        /// 服务器创建成功后触发的事件
        /// </summary>
        public event ServerCreatedEventHandler ServerCreatedEvent;

        public delegate void ClientConnectedEventHandler(object sender, DataEventArgs data);
        /// <summary>
        /// 当客户端连接接入触发的事件
        /// </summary>
        public event ClientConnectedEventHandler ClientConnectedEvent;

        public delegate void ReceiveDataEventHandler(object sender, DataEventArgs data);
        /// <summary>
        /// 接收到数据后触发的事件
        /// </summary>
        public event ReceiveDataEventHandler ReceiveDataEvent;

        public delegate void ClientCloseEventHandler(object sender, DataEventArgs data);
        /// <summary>
        /// 客户端断开连接后触发的事件
        /// </summary>
        public event ClientCloseEventHandler ClientCloseEvent;

        /// <summary>
        /// 当前所建立的服务器集合
        /// </summary>
        public static List<Socket> serverLists = new List<Socket>();

        /// <summary>
        /// 连接到服务器的客户端集合
        /// </summary>
        public List<Socket> clientLists = new List<Socket>();

        /// <summary>
        /// 接收数据数组
        /// </summary>
        private byte[] recvData = new byte[1024 * 10];

        /// <summary>
        /// 当前服务器
        /// </summary>
        public Socket Server { get; }

        public TCPAsyncServer()
        {
            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 开始监听端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="listenCount"></param>
        public void Start(string ip, int port, int listenCount)
        {
            IPAddress _ip = IPAddress.None;
            if (!IPAddress.TryParse(ip, out _ip))
            {
                throw new Exception("IP地址解析失败");
            }
            if (port > IPEndPoint.MaxPort || port < IPEndPoint.MinPort)
            {
                throw new Exception("端口设置不正确");
            }
            try
            {
                EndPoint point = new IPEndPoint(_ip, port);
                Server.Bind(point);
                Server.Listen(listenCount);
                Console.WriteLine($"服务器：{Server.LocalEndPoint.ToString()} 开始监听......");
                serverLists.Add(Server);
                ServerCreatedEvent?.BeginInvoke(this, new DataEventArgs { ServerIP = Server.LocalEndPoint.ToString() }, null, null);
                Server.BeginAccept(Accept, Server);
            }
            catch (SocketException se)
            {
                throw new Exception(se.Message, se);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        public void Stop()
        {
            serverLists.Remove(Server);
            Server.Close();
            Server.Dispose();
            foreach (Socket socket in clientLists)
            {
                socket.Close();
                socket.Dispose();
            }
            clientLists.Clear();
        }

        /// <summary>
        ///监听回调
        /// </summary>
        /// <param name="result"></param>
        private void Accept(IAsyncResult result)
        {
            Socket _server = result.AsyncState as Socket;
            try
            {
                Socket client = _server.EndAccept(result);
                Console.WriteLine($"{client.RemoteEndPoint.ToString()}->接入成功......");
                ClientConnectedEvent?.BeginInvoke(this, new DataEventArgs { ServerIP = _server.LocalEndPoint.ToString(), ClientIP = client.RemoteEndPoint.ToString() }, null, null);
                client.BeginReceive(recvData, 0, recvData.Length, SocketFlags.None, Receive, client);
                clientLists.Add(client);
                System.Threading.Thread.Sleep(1);
                _server.BeginAccept(Accept, _server);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("server关闭,退出监听");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 数据接收回调
        /// </summary>
        /// <param name="result"></param>
        private void Receive(IAsyncResult result)
        {
            Socket client = result.AsyncState as Socket;
            string clientip = client.RemoteEndPoint.ToString();
            try
            {
                int recv = client.EndReceive(result);
                if (recv > 0)
                {
                    string recvStr = Encoding.UTF8.GetString(recvData, 0, recv);
                    ReceiveDataEvent?.BeginInvoke(this, new DataEventArgs { ClientIP = clientip, Data = recvStr }, null, null);
                    Console.WriteLine($"{client.LocalEndPoint.ToString()}->接收到数据:{recvStr}");
                    //byte[] data = Encoding.UTF8.GetBytes($"hello-->{recvStr}");
                    //client.BeginSend(data, 0, data.Length, SocketFlags.None, Send, client);
                    System.Threading.Thread.Sleep(1);
                    client.BeginReceive(recvData, 0, recvData.Length, SocketFlags.None, Receive, client);
                }
                else
                {

                    clientLists.Remove(client);
                    ClientCloseEvent?.BeginInvoke(this, new DataEventArgs { ClientIP = clientip, Data = "断开连接" }, null, null);
                    Console.WriteLine($"{clientip}->断开连接");
                }
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("退出接收数据");
            }
            catch (SocketException se)
            {
                if (se.NativeErrorCode == 10054)// 客户端自己强制主动断开了连接
                {
                    clientLists.Remove(client);
                    ClientCloseEvent?.BeginInvoke(this, new DataEventArgs { ClientIP = clientip, Data = se.Message }, null, null);
                    Console.WriteLine($"{clientip}->{se.Message}");
                    return;
                }
                throw new Exception(se.Message, se);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 发送回调
        /// </summary>
        /// <param name="result"></param>
        private void Send(IAsyncResult result)
        {
            Socket client = result.AsyncState as Socket;
            int sendCount = client.EndSend(result);
            Console.WriteLine($"{client.RemoteEndPoint.ToString()}->send {sendCount} ok");
        }
    }
}
