namespace BasicLibraries.VNAControl.VNA.Keysight
{
    /**********************
     * ① 启动一个测量过程（触发仪器）
     * 将仪器配置为自动执行连续测量
        1.使用 :INIT{1-36}:CONT 命令启动要测量的通道的连续启动模式，并关闭其他通道的连续启动模式。
        2.使用 :TRIG:SOUR 命令将触发源设为“Internal（内部）触发”。

       按要求开始测量
        1.使用 :INIT{1-36}:CONT 命令启动要测量的通道的连续启动模式，并关闭其他通道的连续启动模式。
        2.使用 :TRIG:SOUR 命令将触发源设为“Bus Trigger（总线触发）”。
        3.根据意愿随时触发仪器进行测量。外部控制器可以使用以下三个命令之一触发仪器：
            命令        能否使用*OPC?命令等待扫描结束？        适用的触发源
 
            *TRG        不能                                 只能使用总线触发
            :TRIG       不能                                 外部触发 总线触发 手动触发
            TRIG:SING   能                                   外部触发 总线触发 手动触发
        4.重复步骤3，开始下一个测量过程。

       ② 检索结果：
        1. 读出ASCII格式的数据  :FORM:DATA ASC
        2. 读出二进制格式的数据  :FORM:DATA REAL
            :CALC1:DATA:FDAT?
            :SENS1:FREQ:DATA?

       ③ ECAL校准：（电校准）
         1.使用以下命令执行Ecal：
            校准类型                    命令 
            1端口校准                   :SENS{1-36}:CORR:COLL:ECAL :SOLT1 
            全2端口校准                  :SENS{1-36}:CORR:COLL:ECAL :SOLT2 
            全3端口校准                  :SENS{1-36}:CORR:COLL:ECAL :SOLT3 
            全4端口校准                  :SENS{1-36}:CORR:COLL:ECAL :SOLT4 
            增强的响应校准                 :SENS{1-36}:CORR:COLL:ECAL :ERES
            响应校准（直通）                :SENS{1-36}:CORR:COLL:ECAL :THRU
         注： 在Ecal期间，您可以控制是否执行隔离测量。使用以下命令打开/关闭隔离测量：:SENS{1-36}:CORR:COLL:ECAL:ISOL
        2. 您可以将校准系数与其他仪器设置保存到文件，然后再从文件下载。
            在默认的情况下，系统在保存仪器设置时不保存校准系数。因此，要保存校准系数，您必须发出以下命令明确设定仪器保存校准系数：
            使用以下命令将校准系数保存到文件中：
            :MMEM:STOR 
            使用以下命令从文件下载校准系数：
            :MMEM:LOAD


        



    * ***********************/

    using System;
    using System.Windows.Forms;
    using VNAControl.Base;
    using VNAControl.Format;
using static System.Windows.Forms.AxHost;

/// <summary>
/// 此类为安捷伦或者是德科技的网络分析仪E5071C型号的操作类
/// </summary>
    public class KS_E5071C : VNABase
    {
        private readonly object _getDataLock = new object();
        public override bool GetData(VNAChannelData channelData, int totalChannelNum, ref VNADataFormat data)
        {
            lock (_getDataLock)
            {
                // 关闭实时更新
                Write(":DISP:ENAB OFF");
                // 选择读取的数据格式为二进制
                //Write($":FORM:DATA REAL");
                // 选择要进行测试量轨迹
                Write($":CALC{channelData.ChannelNo}:PAR{channelData.VNATraceData.TraceNo}:SEL");
                // 检查 上一条指令是否出错
                if (CheckError()) return false;

                for (int i = 1; i <= totalChannelNum; i++)
                {
                    // 关闭其他通道的连续启动模式
                    Write($":INIT{i}:CONT OFF");
                }
                // 启动要测量的通道的连续启动模式
                Write($":INIT{channelData.ChannelNo}:CONT ON");
                //Write(":TRIG:SOUR BUS");
                Write(":TRIG:SING");
                // 设置Smoothing的aperture
                Write($"CALC{channelData.ChannelNo}:SMO:APER {channelData.Smoothing.Aperture}");
                // 开启smoothing
                Write($"CALC{channelData.ChannelNo}:SMO:STAT {(channelData.Smoothing.Enable ? "ON" : "OFF")}");

                // 等待测量过程的结束
                if (!CheckCompleted()) return false;

                // 读取频率
                Write($":SENS{channelData.ChannelNo}:FREQ:DATA?");
                data.Frequency = ReadLineBinaryBlockOfDouble();
                // 指定轨迹的数据格式
                Write($":CALC{channelData.ChannelNo}:FORM {channelData.ReadDataTraceType}");
                // 读取数据
                Write($":CALC{channelData.ChannelNo}:DATA:FDAT?");
                double[] temMdata = ReadLineBinaryBlockOfDouble();
                // 解析
                try
                {
                    data.MajorValueOfTheData = new double[channelData.Point];
                    data.MinorValueOfTheData = new double[channelData.Point];
                    for (int i = 0; i < temMdata.Length / 2; i++)
                    {
                        data.MajorValueOfTheData[i] = temMdata[2 * i];
                        data.MinorValueOfTheData[i] = temMdata[2 * i + 1];
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                //将VNA屏幕上显示的曲线切换
                Write($":CALC{channelData.ChannelNo}:FORM {channelData.ShowDataTraceType}");
                // 更新屏幕
                Write(":DISP:ENAB ON");
                Write($":DISP:WIND{channelData.ChannelNo}:TRAC{channelData.VNATraceData.TraceNo}:Y:AUTO");
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
            //Write($":CALC{channelData.ChannelNo}:PAR{channelData.VNATraceData.TraceNo}:SEL");
            //设置内部触发模式
            Write(":TRIG:SOUR INT");
            base.Write($":FORM:DATA ASC");
            //Clears the all status register.
            Write("*CLS");
            for (int i = 1; i <= totalChannelNum; i++)
            {
                //关闭触发系统中选择通道(Ch)的连续初始化模式（由连续初始化的触发系统进行设置）。
                Write($":INIT{i}:CONT OFF");
            }
            //激活选择通道的选择曲线  
            Write($":CALC{channelData.ChannelNo}:PAR{channelData.VNATraceData.TraceNo}:SEL");
            // 检查 上一条指令是否出错
            if (CheckError()) return false;
            Write($":SENS{channelData.ChannelNo}:CORR:COLL:CKIT {(int)channelData.CalKit}");
            if (CheckError()) return false;
            //设置/获取选择通道(Ch)的扫描类型。
            Write($":SENS{channelData.ChannelNo}:SWE:TYPE LINear");
            //设置/获取选择通道(Ch)的扫描范围的开始值。
            Write($":SENS{channelData.ChannelNo}:FREQ:STAR {channelData.StartFrequency * 1000000}");
            //设置/获取选择通道(Ch)的扫描范围的停止值。
            Write($":SENS{channelData.ChannelNo}:FREQ:STOP {channelData.StopFrequency * 1000000}");
            //设置/获取选择通道(Ch)的测量点数
            Write($":SENS{channelData.ChannelNo}:SWE:POIN {channelData.Point}");
            //开启/关闭触发系统中选择通道(Ch)的连续初始化模式（由连续初始化的触发系统进行设置）。
            Write($":INIT{channelData.ChannelNo}:CONT ON");
            //针对选择通道(Ch)的选择迹线(Tr)开启/关闭数据迹线显示。
            Write($":DISP:WIND{channelData.ChannelNo}:TRAC{channelData.VNATraceData.TraceNo}:STAT ON");
            //设置/获取选择通道(Ch)的激活迹线的数据格式。
            Write($":CALC{channelData.ChannelNo}:FORM MLOG");
            //针对选择的通道(Ch)开启/关闭或返回端口扩展的状态。端口扩展修正
            Write($":SENS{channelData.ChannelNo}:CORR:EXT OFF");
            //针对选择的通道(Ch)启动/关闭或获取自动端口扩展的状态。--port extensions                                                    
            Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:PORT{channelData.PortNo} OFF");
            switch (calType)
            {
                case CalType.ECAL:
                    MessageBox.Show("参数配置完成,请连接电子校准器", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //当频率偏置功能打开时，这个命令用ECal（电校准）针对选择的通道(Ch)执行指定端口的1端口校准。 
                    Write($":SENS{channelData.ChannelNo}:CORR:COLL:ECAL:SOLT1 {channelData.PortNo}");
                    if (!CheckCompleted()) return false;
                    break;
                case CalType.MechanicalCalUnit:

                    Write($":SENS{channelData.ChannelNo}:CORR:COLL:METH:SOLT1 {channelData.PortNo}");
                    MessageBox.Show("参数配置完成,请连接到机械校准组件的OPEN端口", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    // 1. 执行open cal
                    MessageBox.Show("执行OPEN校准", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Write($"SENS{channelData.ChannelNo}:CORR:COLL:OPEN {channelData.PortNo}");
                    if (!CheckCompleted()) return false;
                    MessageBox.Show("OPEN校准完成,请连接到机械校准组件的SHORT端口", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    // 2. 执行short
                    MessageBox.Show("执行SHORT校准", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Write($"SENS{channelData.ChannelNo}:CORR:COLL:SHOR {channelData.PortNo}");
                    if (!CheckCompleted()) return false;
                    MessageBox.Show("SHORT校准完成,请连接到机械校准组件的LOAD端口", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    // 3. 执行load cal
                    MessageBox.Show("执行LOAD校准", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Write($"SENS{channelData.ChannelNo}:CORR:COLL:LOAD {channelData.PortNo}");
                    if (!CheckCompleted()) return false;
                    MessageBox.Show("LOAD校准完成,请勿断开机械校准器,确认后请观察波形是否在-50db以下", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Write($"SENS{channelData.ChannelNo}:CORR:COLL:SAVE");
                    break;

                    #region 未验证
                    //switch (channelData.CalKit)
                    //{
                    //    case CalKits.CK_85033D:
                    //    case CalKits.CK_85052D:
                    //    case CalKits.CK_85032F:
                    //    case CalKits.CK_85032B_E:
                    //    case CalKits.CK_85036B_E:
                    //    case CalKits.CK_85031B:
                    //    case CalKits.CK_85050C_D:
                    //    case CalKits.CK_85052C:
                    //    case CalKits.CK_85038A_F_M:
                    //    case CalKits.CK_85054D:
                    //    case CalKits.CK_85056D:
                    //    case CalKits.CK_85056K:
                    //    case CalKits.CK_85039B:
                    //    case CalKits.CK_85033E:
                    //        Write($":SENS{channelData.ChannelNo}:CORR:COLL:METH:SOLT1 {channelData.PortNo}");
                    //        MessageBox.Show("参数配置完成,请连接到机械校准组件的OPEN端口", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        // 1. 执行open cal
                    //        MessageBox.Show("执行OPEN校准", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        Write($"SENS{channelData.ChannelNo}:CORR:COLL:OPEN { channelData.PortNo}");
                    //        if (!CheckCompleted()) return false;
                    //        MessageBox.Show("OPEN校准完成,请连接到机械校准组件的SHORT端口", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        // 2. 执行short
                    //        MessageBox.Show("执行SHORT校准", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        Write($"SENS{channelData.ChannelNo}:CORR:COLL:SHOR { channelData.PortNo}");
                    //        if (!CheckCompleted()) return false;
                    //        MessageBox.Show("SHORT校准完成,请连接到机械校准组件的LOAD端口", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        // 3. 执行load cal
                    //        MessageBox.Show("执行LOAD校准", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        Write($"SENS{channelData.ChannelNo}:CORR:COLL:LOAD { channelData.PortNo}");
                    //        if (!CheckCompleted()) return false;
                    //        MessageBox.Show("LOAD校准完成,请断开机械校准器", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    //        Write($"SENS{channelData.ChannelNo}:CORR:COLL:SAVE");
                    //        break;

                    //    case CalKits.CK_X11644A:
                    //        break;
                    //    case CalKits.CK_P11644A:
                    //        break;
                    //    case CalKits.CK_K11644A:
                    //        break;
                    //    case CalKits.CK_85050B:
                    //        break;
                    //    case CalKits.CK_85052B:
                    //        break;
                    //    case CalKits.CK_85054B:
                    //        break;
                    //    case CalKits.CK_85056A:
                    //        break;
                    //    default:
                    //        break;
                    //}
                    #endregion

            }
            Write($":DISP:WIND{channelData.ChannelNo}:TRAC{channelData.VNATraceData.TraceNo}:Y:AUTO");

            Write($":FORM:DATA REAL");
            //return !CheckError();
            return SaveCalFile();
        }

        internal override bool DoOnePortCalibration(VNAChannelData channelData, int totalChannelNum, Action action, CalType calType = CalType.ECAL)
        {
            action?.Invoke();
            return DoOnePortCalibration(channelData, totalChannelNum, calType);
        }

        internal override bool DoPortExtensions(VNAChannelData channelData)
        {
            Write(":TRIG:SOUR INT");
            Write($":CALC{channelData.ChannelNo}:PAR{channelData.VNATraceData.TraceNo}:SEL");
            // 检查 上一条指令是否出错
            if (CheckError()) return false;
            Write($":CALC{channelData.ChannelNo}:FORM PHASe");
            //针对选择的通道(Ch)启动/关闭或获取自动端口扩展的状态。
            Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:PORT{channelData.PortNo} ON");
            //针对选择的通道(Ch)设置/获取计算自动端口扩展损耗值的频率点。
            Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:CONF CSPN");
            //针对选择的通道(Ch)开启/关闭或获取自动端口扩展结果的损耗补偿状态。
            Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:LOSS ON");
            /*
            针对选择的通道(Ch)开启/关闭或获取使用自动端口扩展结果直流损耗值。
            仅在选中了包含损耗时可用。失配会使 S11 和 S22 迹线上增加纹波。如果纹波较大，S11 和 S22 会显示为大于 0 dB，
            这会导致在使用 S 参数时数值出现不稳定的情况。
            调节失配可增大夹具的损耗，使纹波的峰值在 0 dB 以下。 虽然这会使误差增大（所有误差都是负的），但使得 S 参数在仿真器中使用时不会出现数值不稳定的情况。
            ON - 使迹线偏移，使所有数据点位于零或零以下的位置。
            OFF - 曲线拟合计算的最准确的应用，但允许正响应。
            */
            if (MessageBox.Show("是否使用调整失配?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
                Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:DCOF ON");
            }
            else
            {
                Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:DCOF OFF");
            }

            if (MessageBox.Show("开路测量?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
                Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:MEAS OPEN");
                if (!CheckCompleted()) return false;
            }
            if (MessageBox.Show("短路测量?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
                Write($":SENS{channelData.ChannelNo}:CORR:EXT:AUTO:MEAS SHORT");
                if (!CheckCompleted()) return false;
            }
            Write($":DISP:WIND{channelData.ChannelNo}:TRAC{channelData.VNATraceData.TraceNo}:Y:AUTO");
            //return !CheckError();
            return SaveCalFile();
        }

        internal override bool DoPortExtensions(VNAChannelData channelData, Action action)
        {
            action?.Invoke();
            return DoPortExtensions(channelData);
        }

        internal override bool DoPortMacthing(VNAChannelData channelData)
        {
            // 选择端口
            //Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:PORT{channelData.PortNo}");
            // 选择电路
            Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:PORT{channelData.PortNo}:TYPE {channelData.PortMatching.Circuit.Split(':')[0]}");
            // 设置对应的值
            Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:PORT{channelData.PortNo}:PAR:C {channelData.PortMatching.C}");
            Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:PORT{channelData.PortNo}:PAR:G {channelData.PortMatching.G}");
            Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:PORT{channelData.PortNo}:PAR:L {channelData.PortMatching.L}");
            Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:PORT{channelData.PortNo}:PAR:R {channelData.PortMatching.R}");
            // 开启电路匹配
            Write($":CALC{channelData.ChannelNo}:FSIM:SEND:PMC:STAT {(channelData.PortMatching.PortMatchingEnable ? "ON" : "OFF")}");
            // 开启夹具仿真
            Write($":CALC{channelData.ChannelNo}:FSIM:STAT {(channelData.PortMatching.FixtureSimulator ? "ON" : "OFF")}");
            return SaveCalFile();
        }

        internal override bool SaveCalFile()
        {
            Write(":TRIG:SOUR BUS");
            Write(":FORM:DATA REAL");
            Write(":MMEM:STOR:STYP CST");                                         //用SCPI.MMEMory.STORe.STATe对象将仪器状态保存到文件中时，选择要保存的内容。“CSTate”：指定保存测量条件和校准状态。
            Write(":MMEM:STOR 'D:AMP_CalFile.STA'");                              //将仪器状态（要保存的内容由SCPI.MMEMory.STORe.STYPe对象指定）保存到文件中（文件扩展名为.sta）。
            return !CheckError();
        }

        internal override void DeleteCalFile()
        {
            Write(":MMEM:DEL 'D:AMP_CalFile.STA'");
            Write("*RST");
            Write("*CLS");
        }

        internal override void SetDisplay(VNAAllParameter allParameter)
        {
            Write("SYSTem:PRESet");

            switch (allParameter.VNAChannelDatas.Count)
            {
                case 1:
                    Write(":DISP:SPL D1");
                    break;
                case 2:
                    Write(":DISP:SPL D1_2");
                    break;
                case 3:
                    Write(":DISP:SPL D1_2_3");
                    break;
                case 4:
                    Write(":DISP:SPL D12_34");
                    break;
                case 5:
                case 6:
                    Write(":DISP:SPL D12_34_56");
                    break;
                case 7:
                case 8:
                    Write(":DISP:SPL D12_34_56_78");
                    break;
                case 9:
                    Write(":DISP:SPL D123_456_789");
                    break;
                case 10:
                case 11:
                case 12:
                    Write(":DISP:SPL D123__ABC");
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                    Write(":DISP:SPL D1234__DEFG");
                    break;
            }
            for (int i = 0; i < allParameter.VNAChannelDatas.Count; i++)
            {
                // 设置/获取选择通道(Ch)的迹线数。默认设置1条轨迹
                Write($":CALC{allParameter.VNAChannelDatas[i].ChannelNo}:PAR:COUN 1");
                // 设置/获取选择通道(Ch)的图形布局。
                Write($":DISP:WIND{allParameter.VNAChannelDatas[i].ChannelNo}:SPL D1");
                // 针对选择的通道(Ch)设置/获取选择迹线(Tr)的测量参数
                Write($":CALC{allParameter.VNAChannelDatas[i].ChannelNo}:PAR{allParameter.VNAChannelDatas[i].VNATraceData.TraceNo}:DEF {allParameter.VNAChannelDatas[i].VNATraceData.SParameter}");
                Write($":DISP:WIND{allParameter.VNAChannelDatas[i].ChannelNo}:TITL ON");
                Write($":DISP:WIND{allParameter.VNAChannelDatas[i].ChannelNo}:TITL:DATA \"{allParameter.VNAChannelDatas[i].ChannelName}\"");
                System.Threading.Thread.Sleep(100);
            }
        }

        internal override bool LoadCal()
        {
            Write("MMEM:LOAD 'D:AMP_CalFile.STA'");
            Write($":FORM:DATA REAL");
            // 检查 上一条指令是否出错
            return !CheckError();
        }

        public override bool Connect(string key, out string info)
        {
            VNAType = VNATypes.E5071C;
            return base.Connect(key, out info);
        }
    }
}
