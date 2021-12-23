namespace BasicLibraries.CommonControl.TCP.Server
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    public class TCPServer
    {
        public delegate void ServerCreatedEventHandler(Socket s);
        /// <summary>
        /// 服务器创建成功触发
        /// </summary>
        public event ServerCreatedEventHandler ServerCreatedEvent;

        public delegate void ClientConnectedEventHandler(Socket s);
        /// <summary>
        /// 客户端请求连接成功后触发
        /// </summary>
        public event ClientConnectedEventHandler ClientConnectedEvent;

        public delegate void ReceiveEventHandler(Data data);
        /// <summary>
        /// 接收到数据后触发的事件，里面包含的是数据
        /// </summary>
        public event ReceiveEventHandler ReceiveEvent;

        public delegate void ClientDisConnectedEventHandler(Data data);
        /// <summary>
        /// 客户端断开连接后触发的事件
        /// </summary>
        public event ClientDisConnectedEventHandler ClientDisConnectedEvent;

        /// <summary>
        /// 连接到的客户端及接收数据的线程
        /// </summary>
        public static Dictionary<Socket, Task> clients = new Dictionary<Socket, Task>();

        public static Dictionary<Socket, CancellationTokenSource> servers = new Dictionary<Socket, CancellationTokenSource>();

        private readonly Socket _server;
        public Socket Server { get { return _server; } }

        public TCPServer()
        {
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="ip">地址</param>
        /// <param name="port">端口号</param>
        /// <param name="listenCount">监听个数</param>
        public void Start(string ip, int port, int listenCount)
        {
            try
            {
                IPAddress address = IPAddress.None;
                if (!IPAddress.TryParse(ip, out address))
                    throw new Exception("IP地址错误");
                if (port > IPEndPoint.MaxPort || port < IPEndPoint.MinPort)
                    throw new Exception("端口超过范围");
                EndPoint endPoint = new IPEndPoint(address, port);
                _server.Bind(endPoint);
                _server.Listen(listenCount);
                ServerCreatedEvent?.BeginInvoke(_server, null, null);
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => { Monitor(cancellationTokenSource); }, cancellationTokenSource.Token);
                servers.Add(_server, cancellationTokenSource);
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
        /// 关闭所有的服务器
        /// </summary>
        public static void StopAllServer(Dictionary<Socket, CancellationTokenSource> servers)
        {
            foreach (var server in servers)
            {
                try
                {
                    server.Value.Cancel();
                    server.Key.Shutdown(SocketShutdown.Both);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 关闭指定的服务器
        /// </summary>
        /// <param name="server">服务器标志</param>
        /// <param name="source">关闭相关线程的标志</param>
        public static void StopServer(Socket server, CancellationTokenSource source)
        {
            try
            {
                server.Shutdown(SocketShutdown.Both);
                source.Cancel();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 监听
        /// </summary>
        private void Monitor(CancellationTokenSource source)
        {
            while (true)
            {
                if (source.IsCancellationRequested)
                {
                    return;
                }
                try
                {
                    Socket client = _server.Accept();
                    ClientConnectedEvent?.BeginInvoke(client, null, null);
                    byte[] sendData = Encoding.UTF8.GetBytes("Hello\r\n");
                    client.Send(sendData, 0, sendData.Length, SocketFlags.None);
                    Task t = Task.Run(() => { ServerTask(client, source); });
                    clients.Add(client, t);
                }
                catch (SocketException se)
                {
                    throw new Exception(se.Message, se);
                }
                catch (InvalidOperationException ie)
                {
                    throw new Exception(ie.Message, ie);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                System.Threading.Thread.Sleep(2);
            }
        }

        /// <summary>
        /// 接收数据及业务处理
        /// </summary>
        /// <param name="obj"></param>
        private void ServerTask(object obj, object obj1)
        {
            Socket client = obj as Socket;
            CancellationTokenSource tokenSource = obj1 as CancellationTokenSource;
            byte[] recvData = new byte[1024];
            string msg = string.Empty;
            string clientEndpoint = client.RemoteEndPoint.ToString();
            while (true)
            {
                if (tokenSource.IsCancellationRequested)
                {
                    return;
                }
                try
                {
                    int recvCount = client.Receive(recvData, 0, recvData.Length, SocketFlags.None);

                    if (recvCount > 0)
                    {
                        msg = Encoding.UTF8.GetString(recvData, 0, recvCount);
                        ReceiveEvent?.BeginInvoke(new Data { socket = clientEndpoint, data = msg }, null, null);
                    }
                    else
                    {
                        msg = "断开连接";
                        ClientDisConnectedEvent?.BeginInvoke(new Data { socket = clientEndpoint, data = msg }, null, null);
                        clients.Remove(client);
                        break;
                    }

                }
                catch (Exception ex)
                {
                    msg = "发生异常:" + ex.Message;
                    ClientDisConnectedEvent?.BeginInvoke(new Data { socket = clientEndpoint, data = msg }, null, null);
                    clients.Remove(client);
                    break;
                }

                System.Threading.Thread.Sleep(2);
            }
            client.Close();
            client.Dispose();
        }

    }

    public struct Data
    {
        public string socket;
        public string data;
    }
}
