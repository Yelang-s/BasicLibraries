namespace VNAControl.Forms
{
    using BasicLibraries.VNAControl.Base;
    using BasicLibraries.VNAControl.Format;
    using BasicLibraries.VNAControl.VNA.Keysight;
    using BasicLibraries.VNAControl.VNA.Rohde;
    using SwitchControl;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
using VNAControl.VNA;

    internal partial class FrmCalibration : Form
    {
        private CalType calType = CalType.ECAL;

        private CalKits calKit =  CalKits.CK_85033E;
        internal static VNAAllParameter CalP { get; set; }

        public readonly VNABase @base = null;
        private FrmCalibration()
        {
            InitializeComponent();
            AddBtn();
            // connect vna
            try
            {
                switch (CalP.Type)
                {
                    case VNAType.ZNB8:
                        @base = new RS_ZNB8();
                        break;
                    case VNAType.E5071C:
                        @base = new KS_E5071C();
                        break;
                    default:
                        break;
                }
                if (!@base.Connect(CalP.VNAIP, out string info))
                {
                    MessageBox.Show("VAN Connect Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!@base.LoadCalStatus ||
                        MessageBox.Show("校准文件已加载,是否重新布局", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        @base.SetDisplay(CalP);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"软件需求环境异常\r\n请确认电脑是否安装了'IOLibSuite'\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private static FrmCalibration _instance;

        public static FrmCalibration Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmCalibration();
                }
                return _instance;
            }
        }

        private void AddBtn()
        {
            int x = 10;
            int y = 50;
            int lastBtnLocationX = 0;
            int lastBtnLocationY = 0;
            foreach (VNAChannelData data in CalP.VNAChannelDatas)
            {
                Button btnCal = new Button()
                {
                    Size = new Size(150, 50),
                    Location = new Point(x, y),
                    Name = $"btn{data.ChannelName}Cal",
                    Text = data.ChannelName + $" Cal",
                    Tag = data
                };
                btnCal.MouseClick += BtnCal_MouseClick;

                Button btnOffset = new Button()
                {
                    Size = new Size(160, 50),
                    Location = new Point(x + 160, y),
                    Name = $"btn{data.ChannelName}PExt",
                    Text = data.ChannelName + $" PExt",
                    Tag = data
                };

                Button btnPortMacthing = new Button()
                {
                    Size = new Size(160, 50),
                    Location = new Point(x + 330, y),
                    Name = $"btn{data.ChannelName}PMact",
                    Text = data.ChannelName + $" PMac",
                    Tag = data
                };
                btnPortMacthing.MouseClick += BtnPortMacthing_MouseClick;
                btnOffset.MouseClick += BtnCal_MouseClick;
                this.Controls.AddRange(new Control[] { btnCal, btnOffset, btnPortMacthing });
                y += 60;
                lastBtnLocationX = x + 330 + 140 + 15;
                lastBtnLocationY = y;
            }
            // 调整窗体大小
            int xx = lastBtnLocationX + 10;
            int yy = lastBtnLocationY + 100;
            this.Width = xx;
            this.Height = yy;

            Button btnClose = new Button()
            {
                Size = new Size(50, 20),
                Location = new Point(this.Width - 60, 5),
                Name = $"btnClose",
                Text = "关闭",
                BackColor = Color.Yellow
            };
            btnClose.MouseClick += BtnClose_MouseClick;

            Label label = new Label
            {
                Text = $"VNA{CalP.VNANo} Calibration",
                Size = new Size(200, 20),
                Location = new Point(5, 5),
                Font = new Font("Consolas", 10)
            };

            RadioButton ecalBtn = new RadioButton
            {
                Text = "E-CAL",
                Location = new Point(10, 25),
                Size = new Size(100, 25),
                Checked = true,
                Tag = CalType.ECAL,
            };
            ecalBtn.MouseClick += CalTypeChanged_MouseClick;
            RadioButton mcuBtn = new RadioButton
            {
                Text = "Mechanical-CalUnit",
                Location = new Point(170, 25),
                Size = new Size(200, 25),
                Tag = CalType.MechanicalCalUnit,
            };
            mcuBtn.MouseClick += CalTypeChanged_MouseClick;
            this.Controls.AddRange(new Control[] { label, btnClose, ecalBtn, mcuBtn });
        }

        private void BtnPortMacthing_MouseClick(object sender, MouseEventArgs e)
        {
            if (Check())
            {
                Button b = sender as Button;
                if (b != null)
                {
                    b.BackColor = default;
                    Task<bool> task = null;
                    VNAChannelData channelData = (VNAChannelData)b.Tag;
                    task = Task.Run(() => { return @base.DoPortMacthing(channelData); });
                    b.Cursor = Cursors.WaitCursor;
                    task.Wait();
                    MessageBox.Show($"{channelData.ChannelName} Port Macthing {(task.Result ? $"Completed" : $"Failed\r\nErrorCode:{@base.ErrorInfo.ErrorCode}\r\nErrorMsg:{@base.ErrorInfo.ErrorMsg}")}",
                        (task.Result ? "Tips" : "Error"), MessageBoxButtons.OK, task.Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    b.Cursor = Cursors.Default;
                    b.BackColor = task.Result ? Color.Green : Color.Red;
                }
            }
        }

        private void CalTypeChanged_MouseClick(object sender, MouseEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio != null)
            {
                calType = (CalType)radio.Tag;
                if (calType == CalType.MechanicalCalUnit)
                {
                    FrmCalKit.Instance.ShowDialog();
                    calKit = FrmCalKit.CalKitNum;
                }
            }
        }

        private void BtnClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void BtnCal_MouseClick(object sender, MouseEventArgs e)
        {
            if (Check())
            {
                Button b = sender as Button;
                if (b != null)
                {
                    b.BackColor = default;
                    Task<bool> task = null;
                    VNAChannelData channelData = (VNAChannelData)b.Tag;
                    channelData.CalKit = calKit;
                    string op = b.Text.Split()[1];
                    if (channelData.SwitchEnable)
                    {
                        SwitchControlDll @switch = new SwitchControlDll();
                        if (@switch.Connect())
                        {
                            @switch.OpenAll();
                            @switch.Close(channelData.SwitchChennel);
                        }
                        else
                        {
                            MessageBox.Show("Switch连接失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    FrmActions.Instance.ShowDialog();

                    switch (op)
                    {
                        case "Cal":

                            task = Task.Run(() =>
                            {
                                return @base.DoOnePortCalibration(channelData,
                                                               CalP.VNAChannelDatas.Count,
                                                                      FrmActions.Instance.actionName == "No Action" ? null : VNABase.Actions[FrmActions.Instance.actionName],
                                                                     calType);
                            });
                            break;
                        case "PExt":

                            task = Task.Run(() =>
                            {
                                return @base.DoPortExtensions(channelData,
                                                                  FrmActions.Instance.actionName == "No Action" ? null : VNABase.Actions[FrmActions.Instance.actionName]);
                            });
                            break;
                    }
                    b.Cursor = Cursors.WaitCursor;
                    task.Wait();
                    MessageBox.Show($"{channelData.ChannelName} {(op == "Cal" ? "Calibration" : "Port Extensions")} {(task.Result ? $"Completed" : $"Failed\r\nErrorCode:{@base.ErrorInfo.ErrorCode}\r\nErrorMsg:{@base.ErrorInfo.ErrorMsg}")}",
                        (task.Result ? "Tips" : "Error"), MessageBoxButtons.OK, task.Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    b.Cursor = Cursors.Default;
                    b.BackColor = task.Result ? Color.Green : Color.Red;
                    if (task.Result)
                    {
                        VNADataFormat data = new VNADataFormat();

                        if (@base.GetData(channelData, CalP.VNAChannelDatas.Count, ref data))
                        {
                            if (op == "Cal")
                            {
                                int index = data.MajorValueOfTheData.Count((i) => i > -50);
                                MessageBox.Show($"校准结果:\r\n{(index == 0 ? "通过" : $"当前数据存在{index}个点大于-50db,未通过,请检查")}");
                                b.BackColor = index == 0 ? Color.Green : Color.Red;
                            }
                            FrmCalChart frmCal = new FrmCalChart();
                            frmCal.UpdateZed(data);
                            frmCal.ShowDialog();
                        }
                    }
                }
            }
        }

        private bool Check()
        {
            if (@base == null)
            {
                MessageBox.Show("软件需求环境异常\r\n请确认电脑是否安装了'IOLibSuite'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
