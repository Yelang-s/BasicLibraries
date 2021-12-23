namespace BasicLibraries.CommonControl.TCP
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Global
    {

        /// <summary>
        /// 获取本机的当前的指定类型的IP地址
        /// </summary>
        /// <param name="addressFamily">ipv4/ipv6</param>
        /// <returns></returns>
        public static string[] GetAddresses(AddressFamily addressFamily = AddressFamily.InterNetwork)
        {
            List<string> ips = new List<string>();
            foreach (var item in Dns.GetHostAddresses(Dns.GetHostName()).ToArray().Where(x => x.AddressFamily == addressFamily))
            {
                ips.Add(item.ToString());
            }
            return ips.ToArray();
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// cmd 命令ping 的操作。查看当前Ip是否畅通。
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool Ping(string ip)
        {
            Ping p = new Ping();
            PingOptions options = new PingOptions
            {
                DontFragment = true
            };
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }
    }
}
