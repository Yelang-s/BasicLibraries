namespace VNAControl.Forms
{
    using BasicLibraries.CommonControl.DataGridViewExtension;
    using BasicLibraries.CommonControl.Json;
    using BasicLibraries.VNAControl.Format;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using VNAControl.VNA;

    internal partial class FrmCreateCalFile : Form
    {
        private FrmCreateCalFile()
        {
            InitializeComponent();
            dgvCalDetail.DoubleBuffered(true);
            ClearItem();
            RefreshVNA();
            vnaPort.Items.AddRange(new string[] { "1", "2", "3", "4" });
            vnaChannel.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" });
            traceNolist.Items.AddRange(new string[] { "1" });
            sParameter.Items.AddRange(Enum.GetNames(typeof(MeasurementParameters)));
            readType.Items.AddRange(Enum.GetNames(typeof(TraceStyle)));
            showType.Items.AddRange(Enum.GetNames(typeof(TraceStyle)));
            ShowCal(VNACommonData.VNACalFilePath);
        }

        private static FrmCreateCalFile _instance;
        internal static FrmCreateCalFile Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmCreateCalFile();
                }
                return _instance;
            }
        }

        private void ClearItem()
        {
            vnaConfig.Items.Clear();
            vnaChannel.Items.Clear();
            vnaPort.Items.Clear();
            sParameter.Items.Clear();
            readType.Items.Clear();
            showType.Items.Clear();
            traceNolist.Items.Clear();
        }

        private void RefreshVNA()
        {
            vnaConfig.Items.Clear();
            for (int i = 0; i < FrmVNAConfigMain.Instance.allConfig.Count; i++)
            {
                vnaConfig.Items.Add($"VNA{FrmVNAConfigMain.Instance.allConfig[i].VNAID} Config");
            }
        }

        private void dgvCalDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(e.Location);
            }
        }

        private void tsmOpenFile_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(FrmVNAConfigMain.ConfigPath))
            {
                Directory.CreateDirectory(FrmVNAConfigMain.ConfigPath);
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "*.sjon|*json",
                InitialDirectory = FrmVNAConfigMain.ConfigPath,
                Title = "请选择校准文件"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShowCal(openFileDialog.FileName);
            }
        }

        private void tsmAddRow_Click(object sender, EventArgs e)
        {
            dgvCalDetail.Rows.Add();
        }

        private void tsmDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgvCalDetail.CurrentRow == null) return;
            dgvCalDetail.Rows.RemoveAt(dgvCalDetail.CurrentRow.Index);
        }

        private void tsmSaveFile_Click(object sender, EventArgs e)
        {
            bool result = CreataData(out VNAAllParameter[] allParameters);
            if (result)
            {
                string data = allParameters.ObjToJson();
                if (File.Exists(VNACommonData.VNACalFilePath))
                {
                    File.Delete(VNACommonData.VNACalFilePath);
                }
                using (FileStream fs = new FileStream(VNACommonData.VNACalFilePath, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] datas = Encoding.UTF8.GetBytes(data);
                    fs.Write(datas, 0, datas.Length);
                }
                MessageBox.Show("OK", "保存结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fail", "保存结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCalDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private bool CreataData(out VNAAllParameter[] allParameters)
        {
            button1.Focus();
            HashSet<string> vnas = new HashSet<string>();
            if (dgvCalDetail.Rows.Count == 0)
            {
                allParameters = null;
                return false;
            }
            try
            {
                for (int i = 0; i < dgvCalDetail.Rows.Count; i++)
                {
                    vnas.Add(dgvCalDetail.Rows[i].Cells[0].Value.ToString());
                }
                allParameters = new VNAAllParameter[vnas.Count];
                for (int i = 0; i < vnas.Count; i++)
                {
                    allParameters[i].VNANo = FrmVNAConfigMain.Instance.allConfig[i].VNAID;
                    allParameters[i].VNAIP = FrmVNAConfigMain.Instance.allConfig[i].IP;
                    allParameters[i].Type = FrmVNAConfigMain.Instance.allConfig[i].Type;
                    allParameters[i].VNAChannelDatas = new List<VNAChannelData>();
                    for (int j = 0; j < dgvCalDetail.Rows.Count; j++)
                    {
                        if (!dgvCalDetail.Rows[j].Cells[0].Value.ToString().Split()[0].EndsWith($"{i + 1}"))
                        {
                            continue;
                        }
                        VNAChannelData channelData = new VNAChannelData
                        {
                            AutoLoss = true,
                            PortNo = Convert.ToInt32(dgvCalDetail.Rows[j].Cells[1].Value.ToString()),
                            ChannelNo = Convert.ToInt32(dgvCalDetail.Rows[j].Cells[2].Value.ToString()),
                            ChannelName = dgvCalDetail.Rows[j].Cells[3].Value.ToString(),
                            StartFrequency = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[4].Value.ToString()),
                            StopFrequency = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[5].Value.ToString()),
                            Point = Convert.ToInt32(dgvCalDetail.Rows[j].Cells[6].Value.ToString()),
                            ReadDataTraceType = (TraceStyle)Enum.Parse(typeof(TraceStyle), dgvCalDetail.Rows[j].Cells[12].Value.ToString()),
                            ShowDataTraceType = (TraceStyle)Enum.Parse(typeof(TraceStyle), dgvCalDetail.Rows[j].Cells[13].Value.ToString()),
                            SwitchEnable = Convert.ToBoolean(dgvCalDetail.Rows[j].Cells[14].Value),
                            SwitchChennel = Convert.ToInt32(dgvCalDetail.Rows[j].Cells[15].Value.ToString()),
                            Smoothing = new SmoothingPara
                            {
                                Enable = Convert.ToBoolean(dgvCalDetail.Rows[j].Cells[7].Value),
                                Aperture = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[8].Value.ToString()),
                            },
                            VNATraceData = new VNATraceData
                            {
                                TraceNo = Convert.ToInt32(dgvCalDetail.Rows[j].Cells[9].Value.ToString()),
                                TraceName = dgvCalDetail.Rows[j].Cells[10].Value.ToString(),
                                SParameter = (MeasurementParameters)Enum.Parse(typeof(MeasurementParameters), dgvCalDetail.Rows[j].Cells[11].Value.ToString()),
                            },
                            PortMatching = new PortMatching
                            {
                                FixtureSimulator = Convert.ToBoolean(dgvCalDetail.Rows[j].Cells[16].Value),
                                PortMatchingEnable = Convert.ToBoolean(dgvCalDetail.Rows[j].Cells[17].Value),
                                Circuit = dgvCalDetail.Rows[j].Cells[18].Value.ToString(),
                                C = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[19].Value.ToString()),
                                G = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[20].Value.ToString()),
                                L = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[21].Value.ToString()),
                                R = Convert.ToDouble(dgvCalDetail.Rows[j].Cells[22].Value.ToString()),
                            }
                        };
                        allParameters[i].VNAChannelDatas.Add(channelData);
                    }
                }
            }
            catch(Exception ex)
            {
                allParameters = null;
                MessageBox.Show($"存储失败,参数不正确.\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ShowCal(string file)
        {
            dgvCalDetail.Rows.Clear();
            try
            {
                VNACommonData.GetVNAAllParameters(out VNAAllParameter[] allParameters, file);
                for (int i = 0; i < allParameters.Length; i++)
                {
                    foreach (VNAChannelData data in allParameters[i].VNAChannelDatas)
                    {
                        int id = dgvCalDetail.Rows.Add();
                        dgvCalDetail.Rows[id].Cells[0].Value = $"VNA{allParameters[i].VNANo} Config";
                        dgvCalDetail.Rows[id].Cells[1].Value = data.PortNo.ToString();
                        dgvCalDetail.Rows[id].Cells[2].Value = data.ChannelNo.ToString();
                        dgvCalDetail.Rows[id].Cells[3].Value = data.ChannelName.ToString();
                        dgvCalDetail.Rows[id].Cells[4].Value = data.StartFrequency.ToString();
                        dgvCalDetail.Rows[id].Cells[5].Value = data.StopFrequency.ToString();
                        dgvCalDetail.Rows[id].Cells[6].Value = data.Point.ToString();
                        dgvCalDetail.Rows[id].Cells[7].Value = data.Smoothing.Enable;
                        dgvCalDetail.Rows[id].Cells[8].Value = data.Smoothing.Aperture.ToString();
                        dgvCalDetail.Rows[id].Cells[9].Value = data.VNATraceData.TraceNo.ToString();
                        dgvCalDetail.Rows[id].Cells[10].Value = data.VNATraceData.TraceName.ToString();
                        dgvCalDetail.Rows[id].Cells[11].Value = data.VNATraceData.SParameter.ToString();
                        dgvCalDetail.Rows[id].Cells[12].Value = data.ReadDataTraceType.ToString();
                        dgvCalDetail.Rows[id].Cells[13].Value = data.ShowDataTraceType.ToString();
                        dgvCalDetail.Rows[id].Cells[14].Value = data.SwitchEnable;
                        dgvCalDetail.Rows[id].Cells[15].Value = data.SwitchChennel.ToString();

                        dgvCalDetail.Rows[id].Cells[16].Value = data.PortMatching.FixtureSimulator;
                        dgvCalDetail.Rows[id].Cells[17].Value = data.PortMatching.PortMatchingEnable;
                        dgvCalDetail.Rows[id].Cells[18].Value = data.PortMatching.Circuit?.ToString();
                        dgvCalDetail.Rows[id].Cells[19].Value = data.PortMatching.C.ToString();
                        dgvCalDetail.Rows[id].Cells[20].Value = data.PortMatching.G.ToString();
                        dgvCalDetail.Rows[id].Cells[21].Value = data.PortMatching.L.ToString();
                        dgvCalDetail.Rows[id].Cells[22].Value = data.PortMatching.R.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取校准信息失败\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsmHelp_Click(object sender, EventArgs e)
        {
            FrmShowHelp.Instance.Show();
            FrmShowHelp.Instance.ShowHelp(HelpC.GetCalFileHelp());
        }
    }
}
