namespace VNAControl.Forms
{
    using System.Windows.Forms;
    internal partial class FrmShowHelp : Form
    {

        private FrmShowHelp()
        {
            InitializeComponent();
        }

        private static FrmShowHelp _instance;

        public static FrmShowHelp Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmShowHelp();
                }
                return _instance;
            }
        }

        internal void ShowHelp(string msg)
        {
            rtbHelp.Text = msg;
        }
    }
}
