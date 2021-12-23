namespace BasicLibraries.CommonControl.Serial
{
    using System;
    using System.IO.Ports;
    using System.Windows.Forms;
    public static class SerialCommonTool
    {
        /// <summary>
        /// 获取本机所有的端口
        /// </summary>
        /// <returns></returns>
        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        ///  添加端口到combox中
        /// </summary>
        /// <param name="box"></param>
        public static void ShowPort(ref ComboBox box)
        {
            box.Items.AddRange(GetPorts());
        }

        /// <summary>
        /// 添加波特率到combox中
        /// </summary>
        /// <param name="box"></param>
        public static void ShowBandRate(ref ComboBox box)
        {
            object[] bandRate = new object[] { 50, 75, 100, 150, 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 43000, 56000, 57600, 115200 };
            box.Items.AddRange(bandRate);
            box.SelectedIndex = 9;
        }

        /// <summary>
        /// 添加Parity到combox中
        /// </summary>
        /// <param name="box"></param>
        public static void ShowParity(ref ComboBox box)
        {
            box.Items.AddRange(Enum.GetNames(typeof(Parity)));
            box.SelectedIndex = 0;
        }

        /// <summary>
        /// 添加StopBits到combox中
        /// </summary>
        /// <param name="box"></param>
        public static void ShowStopBits(ref ComboBox box)
        {
            box.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            box.SelectedIndex = 1;
        }

        /// <summary>
        /// 添加数据位到combox中
        /// </summary>
        /// <param name="box"></param>
        public static void ShowDataBits(ref ComboBox box)
        {
            object[] databits = new object[] { 5, 6, 7, 8 };
            box.Items.AddRange(databits);
            box.SelectedIndex = 3;
        }
    }
}
