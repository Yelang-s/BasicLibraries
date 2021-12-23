namespace BasicLibraries.VNAControl.VNA.Rohde
{
    using BasicLibraries.VNAControl.Base;
    using BasicLibraries.VNAControl.Format;
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// 此类为罗德网分仪控制类
    /// </summary>
    public class RS_ZNB8 : VNABase
    {
        private readonly string calName = "ampCal";

        private readonly object _getDataLock = new object();

        public override bool GetData(VNAChannelData channelData, int totalChannelNum, ref VNADataFormat data)
        {
            lock (_getDataLock)
            {
                // Select the trace as the active trace.
                Write($"CALC{channelData.ChannelNo}:PAR:SEL '{channelData.VNATraceData.TraceName}'");
                Write("SYST:DISP:UPD OFF");
                // 切换通道为单次触发模式
                Write($"INITiate{channelData.ChannelNo}:CONTinuous OFF");
                // 触发单次扫描
                Write($"INITiate{channelData.ChannelNo}:IMMediate");
                // Wait until the single sweep sequence is complete
                // 等待测量过程的结束
                if (!CheckCompleted()) return false;

                data.MajorValueOfTheData = new double[channelData.Point];
                data.Frequency = new double[channelData.Point];
                data.MinorValueOfTheData = new double[channelData.Point];

                // get pahse data
                //Write($"CALC{channelData.ChannelNo}:FORM PHASe");// PHASe
                //Write($"CALC{channelData.ChannelNo}:DATA? FDAT");
                //string[] phase = ReadLine().Trim().Split(',');

                // get frequency data
                Write($"CALCulate{channelData.ChannelNo}:DATA:STIMulus?");
                string[] frequency = ReadLine().Trim().Split(',');

                // get response data
                Write($"CALC{channelData.ChannelNo}:FORM {channelData.ReadDataTraceType}");
                Write($"CALC{channelData.ChannelNo}:DATA? FDAT");
                string[] response = ReadLine().Trim().Split(',');
                //20210811
                Write($"SYST:DISP:UPD ON");
                Write($"DISP:WIND{channelData.ChannelNo}:TRAC{channelData.VNATraceData.TraceNo}:Y:AUTO ONCE");
                try
                {
                    for (int i = 0; i < frequency.Length; i++)
                    {
                        data.Frequency[i] = Convert.ToDouble(frequency[i]);
                        data.MinorValueOfTheData[i] = 0;
                        data.MajorValueOfTheData[i] = Convert.ToDouble(response[i]);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                Write($"CALC{channelData.ChannelNo}:FORM {channelData.ShowDataTraceType}");
                return true;
            }
        }

        public override bool GetData(VNAChannelData channelData, int totalChannelNum, Action action, ref VNADataFormat data)
        {
            action?.Invoke();
            return GetData(channelData, totalChannelNum, ref data);
        }

        internal override bool DoOnePortCalibration(VNAChannelData channelData, int totalChannelNum, CalType calType = CalType.ECAL)
        {
            // 检查校准器是否连接
            string calUnitInfo = Query("SYSTem:COMMunicate:RDEVice:AKAL:ADDRess:ALL?");
            if (string.IsNullOrEmpty(calUnitInfo))
            {
                throw new Exception("校准器未连接");
            }

            //检查网分仪与校准器之间的连接状态 返回格式 <analyzer_port_no>, <cal_unit_port_no>
            string[] Info = Query($"SENSe{channelData.ChannelNo}:CORRection:COLLect:AUTO:PORTs:CONNection?").Split(',');
            // 开启连续扫描模式
            Write($"INIT{channelData.ChannelNo}:CONT ON");
            //设置开始频率
            Write($"SENS{channelData.ChannelNo}:FREQ:STAR {channelData.StartFrequency} MHZ");
            //设置结束频率
            Write($"SENS{channelData.ChannelNo}:FREQ:STOP {channelData.StopFrequency} MHZ");
            //设置测试点数
            Write($"SENS{channelData.ChannelNo}:SWE:POINts {channelData.Point}");
            // 切换为smith图形
            Write($"CALC{channelData.ChannelNo}:FORM SMITH");
            MessageBox.Show("校准配置完成,请连接校准器件,并检查波形", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //执行校准
            Write($"SENSe{channelData.ChannelNo}:CORRection:COLLect:AUTO '', {channelData.PortNo}");
            // 等待测量过程的结束
            if (!CheckCompleted()) return false;
            Write($"CALC{channelData.ChannelNo}:FORM {channelData.ShowDataTraceType}");
            return !CheckError();
        }

        internal override bool DoOnePortCalibration(VNAChannelData channelData, int totalChannelNum, Action action, CalType calType = CalType.ECAL)
        {
            action?.Invoke();
            return DoOnePortCalibration(channelData, totalChannelNum, calType);
        }

        internal override bool DoPortExtensions(VNAChannelData channelData)
        {
            //Defines the offset parameters for the active test port such that the residual delay of the active trace 
            //(defined as the negative derivative of the phase response) is minimized and the measured loss is reproduced as far as possible across the entire sweep range.
            Write($"SENSe{channelData.ChannelNo}:CORRection:LOSS{channelData.PortNo}:AUTO ONCE");
            Write($"CALC{channelData.ChannelNo}:FORM {channelData.ShowDataTraceType}");
            return !CheckError();
        }

        internal override bool DoPortExtensions(VNAChannelData channelData, Action action)
        {
            action?.Invoke();
            return DoPortExtensions(channelData);
        }

        internal override bool SaveCalFile()
        {
            //Set the active directory.
            //Write("MMEM:CDIR 'c:\\Rohde&Schwarz\\nwa'");
            //Store the current setup configuration in the file ampcal.zvx in the default directory for setup files C:\Rohde&Schwarz\NWA\RecallSets\ampcal.zvx.
            Write($"MMEM:STOR:STAT 1,'{calName}.znx'");
            if (CheckError())
            {
                return false;
            }
            return true;
        }

        internal override void DeleteCalFile()
        {

        }

        internal override void SetDisplay(VNAAllParameter allParameter)
        {
            // "1,name1,2,name2...."
            string[] cat = Query("CONF:CHAN:CAT?").Replace("'", "").Split(',');
            for (int i = 0; i < cat.Length; i += 2)
            {
                Write($":CONF:CHAN{cat[i]}:STAT OFF");
            }
            foreach (VNAChannelData channelData in allParameter.VNAChannelDatas)
            {
                Write($":CONF:CHAN{channelData.ChannelNo}:STAT ON");
                Write($":CALC{channelData.ChannelNo}:PAR:SDEF '{channelData.VNATraceData.TraceName}', '{channelData.VNATraceData.SParameter}'");
                //20210811
                Write($":DISPLAY:WINDOW{channelData.ChannelNo}:STATE ON");
                Write($":DISPLAY:WINDOW{channelData.ChannelNo}:TRACE{channelData.VNATraceData.TraceNo}:FEED '{channelData.VNATraceData.TraceName}'");
                System.Threading.Thread.Sleep(100);
            }
        }

        internal override bool LoadCal()
        {
            //Set the active directory.
            //Write("MMEM:CDIR 'c:\\Rohde&Schwarz\\nwa'");
            //Re-load the settings stored in ampcal.zvx.
            Write($"MMEM:LOAD:STAT 1,'{calName}.znx'");
            if (CheckError())
            {
                ClearError();
                return false;
            }
            return true;
        }

        public override bool Connect(string key, out string info)
        {
            VNAType = VNATypes.ZNB8;
            return base.Connect(key, out info);
        }
    }
}
