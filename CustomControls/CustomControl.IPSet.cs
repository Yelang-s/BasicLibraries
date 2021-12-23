using System.Windows.Forms;

namespace CustomControls
{
    public partial class IPSet : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IPSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Text
        /// </summary>
        public override string Text
        {
            get => $"{txtIP1.Text.Trim()}.{txtIP2.Text.Trim()}.{txtIP3.Text.Trim()}.{txtIP4.Text.Trim()}";
            set
            {
                txtIP1.Text = value.Split('.')[0];
                txtIP2.Text = value.Split('.')[1];
                txtIP3.Text = value.Split('.')[2];
                txtIP4.Text = value.Split('.')[3];
            }
        }
    }
}
