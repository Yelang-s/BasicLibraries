namespace BasicLibraries.CommonControl.TCP.Client
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    public class TCPClientHelper
    {
        private readonly TcpClient client;
        private readonly object _isLock = new object();
        private readonly string _ip;
        private readonly int _port;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPClientHelper(string ip, int port)
        {
            _ip = ip;
            _port = port;
            client = new TcpClient();
        }

        public TcpClient Client { get { return client; } }
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Connected
        {
            get
            {
                if (client == null)
                {
                    return false;
                }
                return client.Connected;
            }
        }

        /// <summary>
        /// 连接到指定的TCP主机
        /// </summary>
        /// <param name="ip">TCP主机ip地址</param>
        /// <param name="port">TCP主机端口号</param>
        public void Connect()
        {
            try
            {
                IPEndPoint remote = new IPEndPoint(IPAddress.Parse(_ip), _port);
                client.Connect(remote);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 断开连接指定的TCP主机
        /// </summary>
        public void DisConnect()
        {
            client.Close();
        }

        /// <summary>
        /// 发送数据并同步接收结果
        /// </summary>
        /// <param name="msg">数据</param>
        /// <param name="endStr">结束符</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public string SendRead(string msg, string endStr = "\r\n", int timeout = 3000)
        {
            lock (_isLock)
            {
                try
                {
                    if (!Connected)
                    {
                        return "指定TCP主机未连接.";
                    }
                    // 如果该NetworkStream拥有对Socket的所有权，则在使用NetworkStream的Close方法时会同时关闭Socket, 否则关闭NetworkStream时不会关闭Socket
                    //创建发送接收数据的网络流
                    using (NetworkStream stream = new NetworkStream(client.Client, false))
                    {
                        StringBuilder @string = new StringBuilder();
                        //将发送的数据转换为byte[]
                        byte[] sendBuffer = Encoding.Default.GetBytes(msg);
                        //发送数据
                        stream.Write(sendBuffer, 0, sendBuffer.Length);
                        //stream.ReadTimeout = timeout;
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        do
                        {

                            //创建接受数据的buffer
                            byte[] recvBuffer = new byte[1024 * 1024];
                            //接收数据
                            int recv = stream.Read(recvBuffer, 0, recvBuffer.Length);

                            @string.Append(Encoding.Default.GetString(recvBuffer, 0, recv));

                            if (timer.ElapsedMilliseconds > timeout)
                            {
                                @string.Clear();
                                @string.Append("timeout");
                                break;
                            }

                        } while (!@string.ToString().Contains(endStr));

                        return @string.ToString();
                    }
                }
                catch (Exception ex)
                {
                    DisConnect();
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 特写单独收发数据
        /// </summary>
        /// <param name="msg">byte[]类型的数据</param>
        /// <returns></returns>
        public byte[] SendRead(byte[] msg)
        {
            lock (_isLock)
            {
                try
                {
                    if (!Connected)
                    {
                        return new byte[] { 0 };
                    }

                    byte[] data = new byte[1024];
                    //创建发送接收数据的网络流
                    using (NetworkStream stream = new NetworkStream(client.Client, false))
                    {
                        StringBuilder @string = new StringBuilder();
                        //将发送的数据转换为byte[]
                        byte[] sendBuffer = msg;
                        //发送数据
                        stream.Write(sendBuffer, 0, sendBuffer.Length);

                        do
                        {
                            //创建接受数据的buffer
                            byte[] recvBuffer = new byte[1024];
                            //接收数据
                            int recv = stream.Read(recvBuffer, 0, recvBuffer.Length);

                            @string.Append(Encoding.Default.GetString(recvBuffer, 0, recv));

                        } while (!@string.ToString().Contains("\r\n"));

                        //获取服务端发送过来的总数据长度
                        int dataLenght = int.Parse(@string.ToString().Trim());

                        data = new byte[dataLenght];
                        //接收数据
                        stream.Read(data, 0, data.Length);

                        return data;
                    }
                }
                catch (Exception ex)
                {
                    DisConnect();
                    throw new Exception(ex.Message, ex);
                }
            }
        }

    }
}
