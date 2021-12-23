namespace BasicLibraries.CommonControl.TCP.Client
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    public class TCPAsyncClient
    {
        public delegate void ReceiveDataEventHanlder(string s);
        public event ReceiveDataEventHanlder ReceiveDataEvent;
        public delegate void ClientConnectedEventHandler(bool b);
        public event ClientConnectedEventHandler ClientConnectedEvent;
        private readonly Socket _client;
        private byte[] receiveData = new byte[1024];
        public bool Connected { get; private set; } = false;


        public TCPAsyncClient()
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            IPAddress _ip = IPAddress.None;
            if (!IPAddress.TryParse(ip, out _ip))
            {
                throw new Exception("IP地址解析失败");
            }
            if (port > IPEndPoint.MaxPort || port < IPEndPoint.MinPort)
            {
                throw new Exception("端口设置错误");
            }

            try
            {
                EndPoint point = new IPEndPoint(_ip, port);
                _client.BeginConnect(point, ConnectCallBack, _client);
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

        public void SendData(string msg)
        {
            byte[] sendData = Encoding.UTF8.GetBytes(msg);
            _client.BeginSend(sendData, 0, sendData.Length, SocketFlags.None, null, null);
        }

        public void DisConnect()
        {
            _client.Close();
            // _client.Dispose();
            Connected = false;
            ClientConnectedEvent?.BeginInvoke(false, null, null);
        }

        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="result"></param>
        private void ConnectCallBack(IAsyncResult result)
        {
            Socket client = result.AsyncState as Socket;
            client.EndConnect(result);
            ClientConnectedEvent?.BeginInvoke(true, null, null);
            Console.WriteLine($"连接成功->连接到:{client.RemoteEndPoint.ToString()}");
            Connected = client.Connected;
            client.BeginReceive(receiveData, 0, receiveData.Length, SocketFlags.None, ReceiveDataCallBack, client);
        }

        /// <summary>
        /// 数据接收回调
        /// </summary>
        /// <param name="result"></param>
        private void ReceiveDataCallBack(IAsyncResult result)
        {
            try
            {
                Socket client = result.AsyncState as Socket;
                int count = client.EndReceive(result);
                string recvData = Encoding.UTF8.GetString(receiveData, 0, count);
                ReceiveDataEvent?.BeginInvoke(recvData, null, null);
                Console.WriteLine($"{client.RemoteEndPoint.ToString()}:{recvData}");
                client.BeginReceive(receiveData, 0, receiveData.Length, SocketFlags.None, ReceiveDataCallBack, client);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
