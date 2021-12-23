namespace VNAControl.Forms
{
    partial class FrmCreateCalFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateCalFile));
            this.dgvCalDetail = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.vnaConfig = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.vnaPort = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.vnaChannel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.traceNolist = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sParameter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.readType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.showType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.switchEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.switchChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalDetail)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCalDetail
            // 
            this.dgvCalDetail.AllowUserToAddRows = false;
            this.dgvCalDetail.AllowUserToDeleteRows = false;
            this.dgvCalDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCalDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vnaConfig,
            this.vnaPort,
            this.vnaChannel,
            this.Column4,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6,
            this.traceNolist,
            this.Column7,
            this.sParameter,
            this.readType,
            this.showType,
            this.switchEnable,
            this.switchChannel,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14});
            this.dgvCalDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvCalDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvCalDetail.Name = "dgvCalDetail";
            this.dgvCalDetail.RowHeadersVisible = false;
            this.dgvCalDetail.Size = new System.Drawing.Size(996, 291);
            this.dgvCalDetail.TabIndex = 0;
            this.dgvCalDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCalDetail_DataError);
            this.dgvCalDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvCalDetail_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpenFile,
            this.tsmAddRow,
            this.tsmDeleteRow,
            this.tsmSaveFile,
            this.tsmHelp,
            this.tsmExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 136);
            // 
            // tsmOpenFile
            // 
            this.tsmOpenFile.Image = global::VNAControl.Properties.Resources.json;
            this.tsmOpenFile.Name = "tsmOpenFile";
            this.tsmOpenFile.Size = new System.Drawing.Size(133, 22);
            this.tsmOpenFile.Text = "Open File";
            this.tsmOpenFile.Click += new System.EventHandler(this.tsmOpenFile_Click);
            // 
            // tsmAddRow
            // 
            this.tsmAddRow.Image = ((System.Drawing.Image)(resources.GetObject("tsmAddRow.Image")));
            this.tsmAddRow.Name = "tsmAddRow";
            this.tsmAddRow.Size = new System.Drawing.Size(133, 22);
            this.tsmAddRow.Text = "Add Row";
            this.tsmAddRow.Click += new System.EventHandler(this.tsmAddRow_Click);
            // 
            // tsmDeleteRow
            // 
            this.tsmDeleteRow.Image = global::VNAControl.Properties.Resources.删_除;
            this.tsmDeleteRow.Name = "tsmDeleteRow";
            this.tsmDeleteRow.Size = new System.Drawing.Size(133, 22);
            this.tsmDeleteRow.Text = "Delete Row";
            this.tsmDeleteRow.Click += new System.EventHandler(this.tsmDeleteRow_Click);
            // 
            // tsmSaveFile
            // 
            this.tsmSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("tsmSaveFile.Image")));
            this.tsmSaveFile.Name = "tsmSaveFile";
            this.tsmSaveFile.Size = new System.Drawing.Size(133, 22);
            this.tsmSaveFile.Text = "Save";
            this.tsmSaveFile.Click += new System.EventHandler(this.tsmSaveFile_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.Image = global::VNAControl.Properties.Resources.help;
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(133, 22);
            this.tsmHelp.Text = "Help";
            this.tsmHelp.Click += new System.EventHandler(this.tsmHelp_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Image = ((System.Drawing.Image)(resources.GetObject("tsmExit.Image")));
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(133, 22);
            this.tsmExit.Text = "Exit";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(466, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // vnaConfig
            // 
            this.vnaConfig.HeaderText = "VNAConfig";
            this.vnaConfig.Name = "vnaConfig";
            // 
            // vnaPort
            // 
            this.vnaPort.HeaderText = "vnaPort";
            this.vnaPort.Name = "vnaPort";
            // 
            // vnaChannel
            // 
            this.vnaChannel.HeaderText = "vnaChannel";
            this.vnaChannel.Name = "vnaChannel";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ChannelName";
            this.Column4.Name = "Column4";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "StartFrequency";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "StopFrequency";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Point";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "SmoothingEnable";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "SmoothingAperture";
            this.Column6.Name = "Column6";
            // 
            // traceNolist
            // 
            this.traceNolist.HeaderText = "TraceNo";
            this.traceNolist.Name = "traceNolist";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "TraceName";
            this.Column7.Name = "Column7";
            // 
            // sParameter
            // 
            this.sParameter.HeaderText = "SParameter";
            this.sParameter.Name = "sParameter";
            // 
            // readType
            // 
            this.readType.HeaderText = "ReadType";
            this.readType.Name = "readType";
            // 
            // showType
            // 
            this.showType.HeaderText = "ShowType";
            this.showType.Name = "showType";
            // 
            // switchEnable
            // 
            this.switchEnable.HeaderText = "SwitchEnable";
            this.switchEnable.Name = "switchEnable";
            // 
            // switchChannel
            // 
            this.switchChannel.HeaderText = "SwitchChannel";
            this.switchChannel.Name = "switchChannel";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Fixture Simulator";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Port Matching";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.HeaderText = "Circuit";
            this.Column10.Items.AddRange(new object[] {
            "NONE : 指定无电路",
            "SLPC : 指定由串联分路L和分路C组成的电路",
            "PCSL : 指定由分路C和串联分路L组成的电路",
            "PLSC : 指定由分路L和串联分路C组成的电路",
            "SCPL : 指定由串联分路C和分路L组成的电路",
            "PLPC : 指定由分路L和分路C组成的电路"});
            this.Column10.Name = "Column10";
            this.Column10.Width = 42;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "C(F法拉)";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "G(S西门子)";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "L(H亨利)";
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "R(欧姆)";
            this.Column14.Name = "Column14";
            // 
            // FrmCreateCalFile
            // 
            this.ClientSize = new System.Drawing.Size(996, 291);
            this.Controls.Add(this.dgvCalDetail);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCreateCalFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalDetail)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvCalDetail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmAddRow;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveFile;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteRow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.DataGridViewComboBoxColumn vnaConfig;
        private System.Windows.Forms.DataGridViewComboBoxColumn vnaPort;
        private System.Windows.Forms.DataGridViewComboBoxColumn vnaChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewComboBoxColumn traceNolist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn sParameter;
        private System.Windows.Forms.DataGridViewComboBoxColumn readType;
        private System.Windows.Forms.DataGridViewComboBoxColumn showType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn switchEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn switchChannel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column9;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
    }
}