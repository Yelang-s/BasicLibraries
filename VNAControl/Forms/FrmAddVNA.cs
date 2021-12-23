namespace VNAControl.Forms
{
    using BasicLibraries.VNAControl.Format;
    using System;
    using System.Windows.Forms;

    internal partial class FrmAddVNA : Form
    {
        internal static VNAConfigFormat ConfigFormat { get; set; }
        private FrmAddVNA()
        {
            InitializeComponent();
            cboxVNAType.Items.AddRange(Enum.GetNames(typeof(VNAType)));
            cboxVNAType.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(ConfigFormat.IP))
            {
                txtVNANo.Text = ConfigFormat.VNAID.ToString();
                txtip.Text = ConfigFormat.IP;
                cboxVNAType.Text = Enum.GetName(typeof(VNAType), ConfigFormat.Type);
            }
        }

        internal VNAConfigFormat VNAConfig { get; private set; }

        private static FrmAddVNA _instance;

        internal static FrmAddVNA Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmAddVNA();
                }
                return _instance;
            }
        }

        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (!int.TryParse(txtVNANo.Text.Trim(), out int no))
            {
                MessageBox.Show("编号设置错误,仅为正整数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!System.Net.IPAddress.TryParse(txtip.Text.Trim(), out System.Net.IPAddress iPAddress))
            {
                MessageBox.Show("IP设置错误", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            VNAConfig = new VNAConfigFormat
            {
                IP = iPAddress.ToString(),
                VNAID = no,
                Type = (VNAType)Enum.Parse(typeof(VNAType), cboxVNAType.Text.Trim())
            };
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
