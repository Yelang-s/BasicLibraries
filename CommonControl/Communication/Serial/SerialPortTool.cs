namespace BasicLibraries.CommonControl.Serial
{
    using System;
    using System.IO;
    using System.IO.Ports;
    public class SerialPortTool
    {
        private SerialPort _serial;

        public bool IsOpen { get { return _serial != null && _serial.IsOpen; } }

        public SerialPortTool(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serial = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            try
            {
                if (_serial == null) throw new Exception("Instance is null");
                if (_serial.IsOpen) _serial.Close();
                _serial.Open();
            }
            catch (IOException ioex)
            {
                throw new Exception(ioex.Message, ioex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                _serial.Close();
            }
            catch (IOException ioe)
            {
                throw new Exception(ioe.Message, ioe);
            }
            _serial = null;
        }

        /// <summary>
        /// 发送string类型数据
        /// </summary>
        /// <param name="writeData"></param>
        public void Write(string writeData)
        {
            if (_serial == null) throw new Exception("Instance is null");
            try
            {
                _serial.Write(writeData);
            }
            catch (InvalidOperationException ioe)
            {
                throw new Exception(ioe.Message, ioe);
            }
            catch (TimeoutException te)
            {
                throw new Exception(te.Message, te);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 发送byte数组类型数据
        /// </summary>
        /// <param name="datas"></param>
        public void Write(byte[] datas)
        {
            if (_serial == null) throw new Exception("Instance is null");
            try
            {
                _serial.Write(datas, 0, datas.Length);
            }
            catch (InvalidOperationException ioe)
            {
                throw new Exception(ioe.Message, ioe);
            }
            catch (TimeoutException te)
            {
                throw new Exception(te.Message, te);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 读取当前输入缓存中的所有可读数据
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            if (_serial == null) throw new Exception("Instance is null");
            try
            {
                return _serial.ReadExisting();
            }
            catch (InvalidOperationException ioe)
            {
                throw new Exception(ioe.Message, ioe);
            }
        }
    }
}
