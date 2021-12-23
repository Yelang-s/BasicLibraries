namespace BasicLibraries.VisaControl.VisaBase
{
    using Ivi.Visa;
    using Ivi.Visa.FormattedIO;
    using Keysight.Visa;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 搜寻类型
    /// </summary>
    public enum ResourceFindPattern
    {
        /// <summary>
        /// GPIB
        /// </summary>
        GPIB,
        /// <summary>
        /// PXI
        /// </summary>
        PXI,
        /// <summary>
        /// VXI
        /// </summary>
        VXI,
        /// <summary>
        /// GPIB_VXI
        /// </summary>
        GPIB_VXI,
        /// <summary>
        /// GPIBAndGPIB_VXI
        /// </summary>
        GPIBAndGPIB_VXI,
        /// <summary>
        /// All_VXI
        /// </summary>
        All_VXI,
        /// <summary>
        /// ASRL
        /// </summary>
        ASRL,
        /// <summary>
        /// All
        /// </summary>
        All
    }

    /// <summary>
    /// 支持Visa协议的仪器操作基础类
    /// 请先使用"FindResources"获取资源集合
    /// </summary>
    public class VisaBaseLibrary
    {
        private static readonly Dictionary<ResourceFindPattern, string> findPatterns = new Dictionary<ResourceFindPattern, string>
        {
            { ResourceFindPattern.GPIB, "GPIB[0-9]*::?*INSTR" },
            { ResourceFindPattern.PXI, "PXI?*INSTR" },
            { ResourceFindPattern.VXI, "VXI?*INSTR" },
            { ResourceFindPattern.GPIB_VXI, "GPIB-VXI?*INSTR" },
            { ResourceFindPattern.GPIBAndGPIB_VXI, "GPIB?*INSTR" },
            { ResourceFindPattern.All_VXI, "?*VXI[0-9]*::?*INSTR" },
            { ResourceFindPattern.ASRL, "ASRL[0-9]*::?*INSTR" },
            { ResourceFindPattern.All, "?*INSTR" }
        };

        /// <summary>
        /// IMessageBasedSession
        /// </summary>
        private IMessageBasedSession _session = null;

        /// <summary>
        /// 资源管理器
        /// </summary>
        private readonly ResourceManager _manager = new ResourceManager();

        /// <summary>
        /// IO操作,当当前IO接口不够时可以直接操作其他IO接口
        /// </summary>
        public MessageBasedFormattedIO IOManager { get; private set; }

        /// <summary>
        /// 按照指定的搜寻条件查找电脑上存在的资源
        /// </summary>
        /// <param name="findPattern">搜寻条件</param>
        /// <returns>资源集合</returns>
        public static List<string> FindResources(ResourceFindPattern findPattern)
        {
            string pattern = findPatterns[findPattern];
            List<string> resources = new List<string>();
            using (ResourceManager manager = new ResourceManager())
            {
                resources = manager.Find(pattern).ToList();
            }
            return resources;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~VisaBaseLibrary()
        {
            _session?.Dispose();
            _manager?.Dispose();
        }

        /// <summary>
        /// 打开指定的资源
        /// </summary>
        /// <param name="resourceName">资源名</param>
        /// <param name="timeout">超时事件:ms,默认30000</param>
        /// <exception cref="Exception"></exception>
        /// <returns>true：打开成功，反之失败</returns>
        public bool OpenResource(string resourceName, int timeout = 30000)
        {
            ResourceOpenStatus openStatus;
            try
            {
                _session = (IMessageBasedSession)_manager.Open(resourceName, AccessModes.None, 30000, out openStatus);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            if (openStatus == ResourceOpenStatus.Success)
            {
                IOManager = new MessageBasedFormattedIO(_session)
                {
                    ReadBufferSize = 1024 * 10
                };
                _session.TimeoutMilliseconds = timeout;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">发送的数据</param>
        /// <param name="endStr">是否附加结束符</param>
        /// <exception cref="NullReferenceException"></exception>
        public void Write(string data, bool endStr = true)
        {
            if (IOManager == null) throw new NullReferenceException("资源未打开,写入数据失败");
            IOManager.DiscardBuffers();
            if (endStr)
            {
                IOManager.WriteLine(data);
            }
            else
            {
                IOManager.Write(data);
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public string ReadString()
        {
            if (IOManager == null) throw new NullReferenceException("资源未打开,写入数据失败");
            return IOManager.ReadString();
        }

        /// <summary>
        /// 读取一行数据
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns>string</returns>
        public string ReadLine()
        {
            if (IOManager == null) throw new NullReferenceException("资源未打开,写入数据失败");
            return IOManager.ReadLine();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public double[] ReadDouble()
        {
            if (IOManager == null) throw new NullReferenceException("资源未打开,写入数据失败");
            string[] data = IOManager.ReadLine().Trim().Split(',');
            double[] datas = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                double.TryParse(data[i], out datas[i]);
            }
            return datas;
        }

        /// <summary>
        /// 读取一组double类型的数据
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns>double[]</returns>
        public double[] ReadLineBinaryBlockOfDouble()
        {
            if (IOManager == null) throw new NullReferenceException("资源未打开,写入数据失败");
            return IOManager.ReadLineBinaryBlockOfDouble();
        }

        /// <summary>
        /// 发送数据并请求结果
        /// </summary>
        /// <param name="data">发送的数据</param>
        /// <exception cref="NullReferenceException"></exception>
        public string Query(string data)
        {
            Write(data);
            return ReadLine();
        }
    }
}
