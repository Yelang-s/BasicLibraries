using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLibraries.VNAControl.Base;

namespace VNAControl.Forms
{
    public partial class FrmActions : Form
    {
        public string actionName = "";
        public FrmActions()
        {
            InitializeComponent();
        }

        private static FrmActions _instance;

        public static FrmActions Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmActions();
                }
                return _instance;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            actionName = cboxAction.Text.Trim();
            this.Hide();
        }

        private void FrmActions_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                actionName = "";
                cboxAction.Items.Clear();
                cboxAction.Items.Add("No Action");
                foreach (var item in VNABase.Actions)
                {
                    cboxAction.Items.Add(item.Key);
                }
                cboxAction.SelectedIndex = 0;
            }
        }
    }
}
