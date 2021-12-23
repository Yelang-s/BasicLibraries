namespace BasicLibraries.VNAControl.Format
{
    using System.Collections.Generic;
    /// <summary>
    /// 网分仪类型
    /// </summary>
    public enum VNAType
    {
        /// <summary>
        /// 罗德施瓦茨
        /// </summary>
        ZNB8,
        /// <summary>
        /// Keysight-Agilent
        /// </summary>
        E5071C
    }

    /// <summary>
    /// 机械校准件类型
    /// </summary>
    public enum CalKits
    {
        CK_85033E = 1,
        CK_85033D,
        CK_85052D,
        CK_85032F,
        CK_85032B_E,
        CK_85036B_E,
        CK_85031B,
        CK_85050C_D,
        CK_85052C,
        CK_85038A_F_M,
        CK_85054D,
        CK_85056D,
        CK_85056K,
        CK_85039B,
        CK_X11644A,
        CK_P11644A,
        CK_K11644A,
        CK_85050B,
        CK_85052B,
        CK_85054B,
        CK_85056A
    }

    /// <summary>
    /// 轨迹样式
    /// </summary>
    public enum TraceStyle
    {
        /// <summary>
        /// 指定对数幅度格式
        /// 单位 dB
        /// 应用实例：
        /// 1. 回波损耗测量
        /// 2. 插入损耗测量（或增益测量）
        /// 数据格式：
        /// 数据元-主值 对数幅度
        /// 数据元-副值 始终为0
        /// </summary>
        MLOGarithmic,
        /// <summary>
        /// 指定相位格式。
        /// 单位 度°
        /// 应用实例：
        /// 测量与线性相位的偏移
        /// 数据格式：
        /// 数据元-主值 相位
        /// 数据元-副值 始终为0
        /// </summary>
        PHASe,
        /// <summary>
        /// 指定群延迟格式
        /// 单位 秒s
        /// 应用实例：
        /// 测量群时延
        /// 数据格式：
        /// 数据元-主值 群时延
        /// 数据元-副值 始终为0
        /// </summary>
        GDELay,
        /// <summary>
        /// 指定Smith图表格式（线性/相位）。
        /// 数据格式：
        /// 数据元-主值 线性幅度
        /// 数据元-副值 相位
        /// </summary>
        SLINear,
        /// <summary>
        /// 指定Smith图表格式（对数/相位）。
        /// 数据格式：
        /// 数据元-主值 对数幅度
        /// 数据元-副值 相位
        /// </summary>
        SLOGarithmic,
        /// <summary>
        /// 指定Smith图表格式（实部/虚部）。
        /// 数据格式：
        /// 数据元-主值 复数的实部
        /// 数据元-副值 复数的虚部
        /// </summary>
        SCOMplex,
        /// <summary>
        /// 指定Smith图表格式（R+jX）。
        /// 数据格式：
        /// 数据元-主值 电阻
        /// 数据元-副值 电抗
        /// </summary>
        SMITh,
        /// <summary>
        /// 指定Smith图表格式（G+jB）。
        /// 数据格式：
        /// 数据元-主值 电导
        /// 数据元-副值 电纳
        /// </summary>
        SADMittance,
        /// <summary>
        /// 指定极性格式（Lin/相位）。
        /// 数据格式：
        /// 数据元-主值 线性幅度
        /// 数据元-副值 相位
        /// </summary>
        PLINear,
        /// <summary>
        /// 指定极性格式（对数/相位）。
        /// 数据格式：
        /// 数据元-主值 对数幅度
        /// 数据元-副值 相位
        /// </summary>
        PLOGarithmic,
        /// <summary>
        /// 指定极性格式（实部/虚部）。
        /// 数据格式：
        /// 数据元-主值 复数实部
        /// 数据元-副值 复数实部
        /// </summary>
        POLar,
        /// <summary>
        /// 指定线性幅度格式。
        /// 单位 抽象数
        /// 应用实例：
        /// 测量反射系数
        /// 数据格式：
        /// 数据元-主值 线性幅度
        /// 数据元-副值 始终为0
        /// </summary>
        MLINear,
        /// <summary>
        /// 指定SWR格式。
        /// 单位 抽象数
        /// 应用实例：
        /// 测量驻波比
        /// 数据格式：
        /// 数据元-主值 SWR(驻波比)
        /// 数据元-副值 始终为0
        /// </summary>
        SWR,
        /// <summary>
        /// 指定实部格式
        /// 数据格式：
        /// 数据元-主值 复数的实部
        /// 数据元-副值 始终为0
        /// </summary>
        REAL,
        /// <summary>
        /// 指定虚部格式。
        /// 数据格式：
        /// 数据元-主值 复数的虚部
        /// 数据元-副值 始终为0
        /// </summary>
        IMAGinary,
        /// <summary>
        /// 指定扩展相位格式。
        /// 单位 度°
        /// 应用实例：
        /// 测量与线性相位的偏移
        /// 数据格式：
        /// 数据元-主值 扩大的相位
        /// 数据元-副值 始终为0
        /// </summary>
        UPHase,
        /// <summary>
        /// 指定正相位格式。
        /// 单位 度°
        /// 应用实例：
        /// 测量与线性相位的偏移
        /// </summary>
        PPHase
    }

    /// <summary>
    /// 轨迹的S参数
    /// </summary>
    public enum MeasurementParameters
    {
        S11,
        S12,
        S13,
        S14,
        S21,
        S22,
        S23,
        S24,
        S31,
        S32,
        S33,
        S34,
        S41,
        S42,
        S43,
        S44
    }

    /// <summary>
    /// VNA参数格式
    /// </summary>
    public struct VNAAllParameter
    {
        /// <summary>
        /// VNA编号
        /// </summary>
        public int VNANo { get; set; }
        /// <summary>
        /// VNA类型
        /// </summary>
        public VNAType Type { get; set; }
        /// <summary>
        /// VNA IP
        /// </summary>
        public string VNAIP { get; set; }
        /// <summary>
        /// 通道参数集合
        /// </summary>
        public List<VNAChannelData> VNAChannelDatas { get; set; }
    }

    /// <summary>
    /// 通道参数
    /// </summary>
    public struct VNAChannelData
    {
        /// <summary>
        /// VNA端口
        /// </summary>
        public int PortNo { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNo { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 开始频率
        /// </summary>
        public double StartFrequency { get; set; }
        /// <summary>
        /// 结束频率
        /// </summary>
        public double StopFrequency { get; set; }
        /// <summary>
        /// 点数
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 是否开启自动补偿
        /// </summary>
        public bool AutoLoss { get; set; }
        /// <summary>
        /// 通道滤波参数
        /// </summary>
        public SmoothingPara Smoothing { get; set; }
        /// <summary>
        /// 读取数据格式
        /// </summary>
        public TraceStyle ReadDataTraceType { get; set; }
        /// <summary>
        /// 显示的轨迹样式
        /// </summary>
        public TraceStyle ShowDataTraceType { get; set; }
        /// <summary>
        /// 是否使用Switch
        /// </summary>
        public bool SwitchEnable { get; set; }
        /// <summary>
        /// Switch的通道
        /// </summary>
        public int SwitchChennel { get; set; }
        /// <summary>
        /// 使用机械校准件时,机械校准件的类型，电子校准不需要
        /// </summary>
        public CalKits CalKit { get; set; }
        /// <summary>
        /// 通道中的轨迹集合
        /// </summary>
        public VNATraceData VNATraceData { get; set; }
        /// <summary>
        /// 通道的电路匹配
        /// </summary>
        public PortMatching  PortMatching { get; set; }
    }

    /// <summary>
    /// 轨迹参数
    /// </summary>
    public struct VNATraceData
    {
        /// <summary>
        /// 轨迹编号
        /// </summary>
        public int TraceNo { get; set; }
        /// <summary>
        /// 轨迹名
        /// </summary>
        public string TraceName { get; set; }
        /// <summary>
        /// 轨迹的S参数
        /// </summary>
        public MeasurementParameters SParameter { get; set; }

    }

    /// <summary>
    /// 滤波参数,针对于通道,适用于通道内的所有的Trace
    /// </summary>
    public struct SmoothingPara
    {
        /// <summary>
        /// 开启曲线圆滑
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 圆滑系数
        /// </summary>
        public double Aperture { get; set; }
    }

    /// <summary>
    /// 匹配电路嵌入参数
    /// </summary>
    public struct PortMatching
    {
        /// <summary>
        /// 开启/关闭选择通道(Ch)的夹具模拟器功能。
        /// </summary>
        public bool FixtureSimulator { get; set; }
        /// <summary>
        /// 对选择通道(Ch)的所有端口开启/关闭匹配电路嵌入功能。
        /// </summary>
        public bool PortMatchingEnable { get; set; }
        /// <summary>
        /// 针对选择通道(Ch)的端口1和4(Pt)选择匹配电路类型。
        /// </summary>
        public string Circuit { get; set; }
        /// <summary>
        /// 匹配电路C值 
        /// 单位：F（法拉）
        /// 范围：-1E18到1E18
        /// </summary>
        public double C { get; set; }
        /// <summary>
        /// 匹配电路G值 
        /// 单位：S（西门子）
        /// 范围：-1E18到1E18
        /// </summary>
        public double G { get; set; }
        /// <summary>
        /// 匹配电路L值
        /// 单位：H（亨利）
        /// 范围：-1E18到1E18
        /// </summary>
        public double L { get; set; }
        /// <summary>
        /// 匹配电路R值
        /// 单位：ohm（欧姆）
        /// 范围：-1E18到1E18
        /// </summary>
        public double R { get; set; }
    }
}
