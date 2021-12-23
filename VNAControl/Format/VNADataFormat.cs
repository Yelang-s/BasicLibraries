namespace BasicLibraries.VNAControl.Format
{
    public struct VNADataFormat
    {
        /// <summary>
        /// 频率
        /// </summary>
        public double[] Frequency { get; set; }

        /// <summary>
        /// 数据元主值
        /// </summary>
        public double[] MajorValueOfTheData { get; set; }

        /// <summary>
        /// 数据元副值
        /// </summary>
        public double[] MinorValueOfTheData { get; set; }
    }
}
