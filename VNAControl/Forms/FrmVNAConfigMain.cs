namespace VNAControl.Forms
{
    using BasicLibraries.CommonControl.Json;
    using BasicLibraries.VNAControl.Base;
    using BasicLibraries.VNAControl.Format;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using VNAControl.Properties;
    using VNAControl.VNA;

    internal partial class FrmVNAConfigMain : Form
    {
        /// <summary>
        /// 文件默认存储文件夹
        /// </summary>
        internal static string ConfigPath { get; private set; } = $".\\vnaconfig";

        /// <summary>
        /// VNA配置默认文件名
        /// </summary>
        internal static string VNAConfigName { get; private set; } = "vna.json";

        /// <summary>
        /// VNA配置整合后的文件路径
        /// </summary>
        internal static string VNAConfigFileCombinePath { get; set; } = "";

        /// <summary>
        /// 所有配置
        /// </summary>
        internal List<VNAConfigFormat> allConfig = new List<VNAConfigFormat>();

        /// <summary>
        /// 校准参数
        /// </summary>
        internal VNAAllParameter[] allParameters;

        private FrmVNAConfigMain()
        {
            InitializeComponent();
            VNAConfigFileCombinePath = ConfigPath + "\\" + VNAConfigName;
            lvVNAIconItems.Items.Clear();
            lvVNAIconItems.Groups.Clear();
            lvVNAIconItems.View = View.LargeIcon;
            lvVNAIconItems.LargeImageList = vnaImage;
            lvVNAIconItems.Groups.Add(new ListViewGroup { Header = "Keysight", Name = "Keysight" });
            lvVNAIconItems.Groups.Add(new ListViewGroup { Header = "Rohde", Name = "Rohde" });
            lvVNAIconItems.ShowGroups = true;
            ReadAndAdd(VNAConfigFileCombinePath);
            ReadCal();
        }

        private static FrmVNAConfigMain _instance = new FrmVNAConfigMain();

        internal static FrmVNAConfigMain Instance
        {
            get
            {
                return _instance;
            }
        }

        private void tsmAddVNA_Click(object sender, EventArgs e)
        {
            if (FrmAddVNA.Instance.ShowDialog() == DialogResult.OK)
            {
                if (!Check()) return;
                lvVNAIconItems.Items.Add(
                    new ListViewItem
                    {
                        Text = FrmAddVNA.Instance.VNAConfig.IP,
                        ImageIndex = GetImageIndex(FrmAddVNA.Instance.VNAConfig.Type),
                        Tag = FrmAddVNA.Instance.VNAConfig,
                        Group = FrmAddVNA.Instance.VNAConfig.Type == VNAType.ZNB8 ?
                        lvVNAIconItems.Groups[1] : lvVNAIconItems.Groups[0],
                        ToolTipText = FrmAddVNA.Instance.VNAConfig.ToString()
                    }
                    );
                allConfig.Add(FrmAddVNA.Instance.VNAConfig);
                Save();
            }
        }

        private void lvVNAIconItems_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lvVNAIconItems.ContextMenuStrip = null;
                if (lvVNAIconItems.SelectedItems.Count > 0)
                {
                    contextMenuStrip1.Show(lvVNAIconItems, e.Location);
                }
            }
            else
            {
                if (lvVNAIconItems.SelectedItems.Count > 0)
                {
                    toolTip1.Show(((VNAConfigFormat)lvVNAIconItems.SelectedItems[0].Tag).ToString(), lvVNAIconItems, e.Location);
                }
                else
                {
                    toolTip1.Hide(lvVNAIconItems);
                }
            }
        }

        private void tsmVNADelete_Click(object sender, EventArgs e)
        {
            if (lvVNAIconItems.SelectedItems.Count > 0)
            {
                int count = lvVNAIconItems.SelectedItems.Count;
                for (int i = 0; i < count; i++)
                {
                    allConfig.RemoveAt(lvVNAIconItems.SelectedItems[0].Index);
                    lvVNAIconItems.Items.RemoveAt(lvVNAIconItems.SelectedItems[0].Index);
                }
                Save();
            }
        }

        private void tsmVNAConnectTest_Click(object sender, EventArgs e)
        {
            if (lvVNAIconItems.SelectedItems.Count > 0)
            {
                vnaDetail.Text = "";
                try
                {
                    List<string> allResoures = VNABase.FindResources(BasicLibraries.VisaControl.VisaBase.ResourceFindPattern.All);
                    for (int i = 0; i < lvVNAIconItems.SelectedItems.Count; i++)
                    {
                        bool inPc = allResoures.FindAll((x) => { return x.Contains(lvVNAIconItems.SelectedItems[i].Text); }).Count > 0;
                        if (inPc)
                        {
                            VNABase temp = new VNABase();
                            if (temp.OpenResource(allResoures.Find((x) => { return x.Contains(lvVNAIconItems.SelectedItems[i].Text); })))
                            {
                                vnaDetail.AppendText(temp.Query("*IDN?"));
                            }
                        }
                        else
                        {
                            vnaDetail.AppendText(lvVNAIconItems.SelectedItems[i].Text + "资源不存在\r\n");
                        }
                    }
                }
                catch (DllNotFoundException dfe)
                {
                    MessageBox.Show($"软件环境需求异常\r\n{dfe.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"测试异常\r\n{ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsmConfig_Click(object sender, EventArgs e)
        {
            FrmCreateCalFile.Instance.ShowDialog();
            ReadCal();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void Save()
        {
            Directory.CreateDirectory(ConfigPath);
            allConfig.Sort((x, y) => { return x.VNAID.CompareTo(y.VNAID); });
            using (FileStream fs = new FileStream(VNAConfigFileCombinePath, FileMode.Create, FileAccess.Write))
            {
                byte[] datas = Encoding.UTF8.GetBytes(allConfig.ObjToJson());
                fs.Write(datas, 0, datas.Length);
            }
        }

        /// <summary>
        /// 读取配置及显示到界面
        /// </summary>
        /// <param name="path"></param>
        private void ReadAndAdd(string path)
        {
            if (!File.Exists(path)) return;
            allConfig = File.ReadAllText(path).JsonToObj<List<VNAConfigFormat>>();
            if (allConfig.Count > 0)
            {
                lvVNAIconItems.Items.Clear();
                allConfig.Sort((x, y) => { return x.VNAID.CompareTo(y.VNAID); });
                foreach (VNAConfigFormat item in allConfig)
                {
                    lvVNAIconItems.Items.Add(new ListViewItem
                    {
                        Text = item.IP,
                        ImageIndex = GetImageIndex(item.Type),
                        Tag = item,
                        Group = item.Type == VNAType.ZNB8 ?
                        lvVNAIconItems.Groups[1] : lvVNAIconItems.Groups[0]
                    });
                }
            }
        }

        /// <summary>
        /// 获取图片索引
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetImageIndex(VNAType type)
        {
            int imageIndex = 0;
            switch (type)
            {
                case VNAType.ZNB8:
                    imageIndex = 3;
                    break;
                case VNAType.E5071C:
                    imageIndex = 1;
                    break;
            }
            return imageIndex;
        }

        private void tsmReadVNAConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "*.json|*.json"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ReadAndAdd(openFile.FileName);
            }
        }

        private void tsmAlterConfig_Click(object sender, EventArgs e)
        {
            FrmAddVNA.ConfigFormat = (VNAConfigFormat)lvVNAIconItems.SelectedItems[0].Tag;
            if (FrmAddVNA.Instance.ShowDialog() == DialogResult.OK)
            {
                allConfig.RemoveAt(lvVNAIconItems.SelectedItems[0].Index);
                if (!Check())
                {
                    allConfig.Add(FrmAddVNA.ConfigFormat);
                    Save();
                    ReadAndAdd(VNAConfigFileCombinePath);
                    return;
                }
                lvVNAIconItems.SelectedItems[0].Text = FrmAddVNA.Instance.VNAConfig.IP;
                lvVNAIconItems.SelectedItems[0].ImageIndex = GetImageIndex(FrmAddVNA.Instance.VNAConfig.Type);
                lvVNAIconItems.SelectedItems[0].Tag = FrmAddVNA.Instance.VNAConfig;
                lvVNAIconItems.SelectedItems[0].Group = FrmAddVNA.Instance.VNAConfig.Type == VNAType.ZNB8 ?
                lvVNAIconItems.Groups[1] : lvVNAIconItems.Groups[0];
                allConfig.Add(FrmAddVNA.Instance.VNAConfig);
                Save();
                ReadAndAdd(VNAConfigFileCombinePath);
            }
        }

        /// <summary>
        /// 检查添加或者修改是否重复
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            int temp = allConfig.FindAll((x) => { return x.VNAID == FrmAddVNA.Instance.VNAConfig.VNAID; }).Count;
            if (temp != 0)
            {
                MessageBox.Show("添加失败,此ID设备已存在", "tips", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            int temp1 = allConfig.FindAll((x) => { return x.IP == FrmAddVNA.Instance.VNAConfig.IP; }).Count;
            if (temp1 != 0)
            {
                MessageBox.Show("添加失败,此IP设备已存在", "tips", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void tsmCalibration_Click(object sender, EventArgs e)
        {
            if (lvVNAIconItems.SelectedItems.Count > 0)
            {
                FrmCalibration.CalP = default;
                int index = ((VNAConfigFormat)lvVNAIconItems.SelectedItems[0].Tag).VNAID;
                for (int i = 0; i < allParameters.Length; i++)
                {
                    if (index == allParameters[i].VNANo)
                    {
                        FrmCalibration.CalP = allParameters[i];
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(FrmCalibration.CalP.VNAIP))
            {
                MessageBox.Show("当前VNA未配置参数,请配置参数后进行校准操作", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmCalibration.Instance.ShowDialog();
        }

        private void ReadCal()
        {
            try
            {
                VNACommonData.GetVNAAllParameters(out allParameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取校准信息失败\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
