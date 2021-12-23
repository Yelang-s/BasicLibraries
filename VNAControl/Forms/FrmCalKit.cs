namespace VNAControl.Forms
{
    using BasicLibraries.VNAControl.Format;
    using System;
    using System.Windows.Forms;

    internal partial class FrmCalKit : Form
    {
        internal static CalKits CalKitNum { get; private set; } = CalKits.CK_85033E;
        private FrmCalKit()
        {
            InitializeComponent();
            cboxCalKits.Items.AddRange(Enum.GetNames(typeof(CalKits)));
            cboxCalKits.SelectedIndex = 0;
        }

        private static FrmCalKit _instance;

        public static FrmCalKit Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmCalKit();
                }
                return _instance;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            CalKitNum = (CalKits)Enum.Parse(typeof(CalKits), cboxCalKits.Text);
            this.Close();
        }
    }
}
