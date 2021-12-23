namespace BasicLibraries.VNAControl.Base
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    using VisaControl.VisaBase;
    using VNAControl.Format;

    /// <summary>
    /// VNA类型
    /// </summary>
    public enum VNATypes
    {
        ZNB8,
        E5071C
    }

    /// <summary>
    /// 校准类型
    /// </summary>
    public enum CalType
    {
        /// <summary>
        /// 电子校准器
        /// </summary>
        ECAL,
        /// <summary>
        /// 机械校准件
        /// </summary>
        MechanicalCalUnit
    }

    public struct Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }

    /// <summary>
    /// VNA操作基础类
    /// 请先使用"FindResources"获取资源数组
    /// </summary>
    public class VNABase : VisaBaseLibrary
    {
        /// <summary>
        /// 动作集合
        /// </summary>
        public static Dictionary<string, Action> Actions { get; set; } = new Dictionary<string, Action>();

        /// <summary>
        /// VNA类型
        /// </summary>
        public VNATypes VNAType { get; protected set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public Error ErrorInfo { get; protected set; }

        /// <summary>
        /// 资源名
        /// </summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// 校准文件加载状态
        /// </summary>
        internal bool LoadCalStatus { get; private set; }

        /// <summary>
        /// 触发VNA,获取数据
        /// </summary>
        /// <param name="channelData"></param>
        /// <param name="totalChannelNum"></param>
        /// <param name="data"></param>
        public virtual bool GetData(VNAChannelData channelData, int totalChannelNum, ref VNADataFormat data) { throw new NotImplementedException(); }

        /// <summary>
        /// 触发VNA，获取数据
        /// </summary>
        /// <param name="channelData">通道参数</param>
        /// <param name="totalChannelNum">总通道数</param>
        /// <param name="action">在触发需要做的额外的事</param>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public virtual bool GetData(VNAChannelData channelData, int totalChannelNum, Action action, ref VNADataFormat data) { throw new NotImplementedException(); }

        /// <summary>
        /// 1端口校准
        /// </summary>
        /// <param name="channelData">通道参数</param>
        /// <param name="totalChannelNum">总通道数</param>
        /// <param name="calType">校准类型</param>
        /// <returns></returns>
        internal virtual bool DoOnePortCalibration(VNAChannelData channelData, int totalChannelNum, CalType calType = CalType.ECAL) { throw new NotImplementedException(); }

        /// <summary>
        /// 1端口校准
        /// </summary>
        /// <param name="channelData">通道参数</param>
        /// <param name="totalChannelNum">总通道数</param>
        /// <param name="action">校准前要做的额外的事</param>
        /// <param name="calType">校准类型</param>
        /// <returns></returns>
        internal virtual bool DoOnePortCalibration(VNAChannelData channelData, int totalChannelNum, Action action, CalType calType = CalType.ECAL) { throw new NotImplementedException(); }

        /// <summary>
        /// 端口扩展
        /// </summary>
        /// <param name="channelData"></param>
        internal virtual bool DoPortExtensions(VNAChannelData channelData) { throw new NotImplementedException(); }

        /// <summary>
        /// 端口扩展
        /// </summary>
        /// <param name="channelData"></param>
        internal virtual bool DoPortExtensions(VNAChannelData channelData, Action action) { throw new NotImplementedException(); }

        /// <summary>
        /// 执行匹配电路嵌入
        /// </summary>
        /// <param name="channelData"></param>
        /// <returns></returns>
        internal virtual bool DoPortMacthing(VNAChannelData channelData) { throw new NotImplementedException(); }

        /// <summary>
        /// 保存校准状态到校准文件
        /// </summary>
        internal virtual bool SaveCalFile() { throw new NotImplementedException(); }

        /// <summary>
        /// 删除校准文件
        /// </summary>
        internal virtual void DeleteCalFile() { throw new NotImplementedException(); }

        /// <summary>
        /// 设置VNA屏幕显示
        /// </summary>
        /// <param name="allParameter"></param>
        internal virtual void SetDisplay(VNAAllParameter allParameter) { throw new NotImplementedException(); }

        /// <summary>
        /// 加载校准文件
        /// </summary>
        internal virtual bool LoadCal() { throw new NotImplementedException(); }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="key">资源字符串中的关键字</param>
        /// <param name="resourceInfo">连接成功后返回所连接的仪器的信息</param>
        /// <returns>连接结果</returns>
        public virtual bool Connect(string key, out string resourceInfo)
        {
            resourceInfo = string.Empty;
            bool result;
            try
            {
                string resourceName = VNABase.FindResources(ResourceFindPattern.All).Find((x) => { return x.Contains(key); });
                if (string.IsNullOrEmpty(resourceName))
                {
                    MessageBox.Show($"连接失败\r\nKey为'{key}'的资源不存在", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                result = OpenResource(resourceName);
                ResourceName = resourceName;
                if (result)
                {
                    Write("*RST");
                    Write("*CLS");
                    resourceInfo = Query("*IDN?").Trim();
                    LoadCalStatus = LoadCal();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 检查是否存在错误
        /// </summary>
        /// <returns>返回false时为无错误，true为有错误可在errorInfo里面看到信息</returns>
        internal bool CheckError()
        {
            string[] strtmp = Query(":SYSTem:ERRor?").Split(',');
            Write("*CLS");
            bool result = int.TryParse(strtmp[0], out int code);
            ErrorInfo = new Error
            {
                ErrorMsg = code != 0 ? strtmp[1] : "",
                ErrorCode = code
            };
            return code != 0;
        }

        /// <summary>
        /// 清除错误
        /// </summary>
        /// <returns></returns>
        internal bool ClearError()
        {
            Write("*CLS");
            return !CheckError();
        }

        /// <summary>
        /// 检查指令是否完成
        /// </summary>
        /// <returns>true completed</returns>
        internal bool CheckCompleted()
        {
            string res = string.Empty;
            bool result = true;
            Stopwatch timer = Stopwatch.StartNew();
            do
            {
                res = Query("*OPC?");
                if (res.Contains("Error") || timer.ElapsedMilliseconds > 5000)
                {
                    result = false;
                    break;
                }
                Application.DoEvents();
            } while (!res.Contains("1"));
            return result;
        }
    }
}
