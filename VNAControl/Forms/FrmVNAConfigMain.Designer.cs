namespace VNAControl.Forms
{
    partial class FrmVNAConfigMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVNAConfigMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.vnaDetail = new System.Windows.Forms.TextBox();
            this.lvVNAIconItems = new System.Windows.Forms.ListView();
            this.vnaImage = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmVNAConnectTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAlterConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVNADelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCreateNewVNAConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReadVNAConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddVNA = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.vnaDetail);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvVNAIconItems);
            this.splitContainer1.Size = new System.Drawing.Size(980, 589);
            this.splitContainer1.SplitterDistance = 143;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // vnaDetail
            // 
            this.vnaDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.vnaDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vnaDetail.Location = new System.Drawing.Point(0, 27);
            this.vnaDetail.Multiline = true;
            this.vnaDetail.Name = "vnaDetail";
            this.vnaDetail.ReadOnly = true;
            this.vnaDetail.Size = new System.Drawing.Size(980, 116);
            this.vnaDetail.TabIndex = 0;
            // 
            // lvVNAIconItems
            // 
            this.lvVNAIconItems.BackColor = System.Drawing.SystemColors.Control;
            this.lvVNAIconItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVNAIconItems.HideSelection = false;
            this.lvVNAIconItems.Location = new System.Drawing.Point(0, 0);
            this.lvVNAIconItems.Name = "lvVNAIconItems";
            this.lvVNAIconItems.Size = new System.Drawing.Size(980, 445);
            this.lvVNAIconItems.TabIndex = 0;
            this.lvVNAIconItems.UseCompatibleStateImageBehavior = false;
            this.lvVNAIconItems.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvVNAIconItems_MouseUp);
            // 
            // vnaImage
            // 
            this.vnaImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("vnaImage.ImageStream")));
            this.vnaImage.TransparentColor = System.Drawing.SystemColors.Control;
            this.vnaImage.Images.SetKeyName(0, "E5071C_2P.png");
            this.vnaImage.Images.SetKeyName(1, "E5071C_4P.png");
            this.vnaImage.Images.SetKeyName(2, "ZNB8_2P.jpg");
            this.vnaImage.Images.SetKeyName(3, "ZNB8_4P.jpg");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVNAConnectTest,
            this.tsmConfig,
            this.tsmCalibration,
            this.tsmAlterConfig,
            this.tsmVNADelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 136);
            // 
            // tsmVNAConnectTest
            // 
            this.tsmVNAConnectTest.Image = global::VNAControl.Properties.Resources.连接;
            this.tsmVNAConnectTest.Name = "tsmVNAConnectTest";
            this.tsmVNAConnectTest.Size = new System.Drawing.Size(152, 22);
            this.tsmVNAConnectTest.Text = "测试连接";
            this.tsmVNAConnectTest.Click += new System.EventHandler(this.tsmVNAConnectTest_Click);
            // 
            // tsmConfig
            // 
            this.tsmConfig.Image = global::VNAControl.Properties.Resources.配置1;
            this.tsmConfig.Name = "tsmConfig";
            this.tsmConfig.Size = new System.Drawing.Size(152, 22);
            this.tsmConfig.Text = "参数详情配置";
            this.tsmConfig.Click += new System.EventHandler(this.tsmConfig_Click);
            // 
            // tsmCalibration
            // 
            this.tsmCalibration.Image = global::VNAControl.Properties.Resources.校准;
            this.tsmCalibration.Name = "tsmCalibration";
            this.tsmCalibration.Size = new System.Drawing.Size(180, 22);
            this.tsmCalibration.Text = "校准";
            this.tsmCalibration.Click += new System.EventHandler(this.tsmCalibration_Click);
            // 
            // tsmAlterConfig
            // 
            this.tsmAlterConfig.Image = global::VNAControl.Properties.Resources.修改;
            this.tsmAlterConfig.Name = "tsmAlterConfig";
            this.tsmAlterConfig.Size = new System.Drawing.Size(152, 22);
            this.tsmAlterConfig.Text = "修改";
            this.tsmAlterConfig.Click += new System.EventHandler(this.tsmAlterConfig_Click);
            // 
            // tsmVNADelete
            // 
            this.tsmVNADelete.Image = global::VNAControl.Properties.Resources.删除;
            this.tsmVNADelete.Name = "tsmVNADelete";
            this.tsmVNADelete.Size = new System.Drawing.Size(152, 22);
            this.tsmVNADelete.Text = "删除";
            this.tsmVNADelete.Click += new System.EventHandler(this.tsmVNADelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 24);
            this.panel1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCreateNewVNAConfig,
            this.tsmReadVNAConfig,
            this.tsmAddVNA});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // tsmCreateNewVNAConfig
            // 
            this.tsmCreateNewVNAConfig.Name = "tsmCreateNewVNAConfig";
            this.tsmCreateNewVNAConfig.Size = new System.Drawing.Size(126, 22);
            this.tsmCreateNewVNAConfig.Text = "新建配置";
            // 
            // tsmReadVNAConfig
            // 
            this.tsmReadVNAConfig.Name = "tsmReadVNAConfig";
            this.tsmReadVNAConfig.Size = new System.Drawing.Size(126, 22);
            this.tsmReadVNAConfig.Text = "读取配置";
            this.tsmReadVNAConfig.Click += new System.EventHandler(this.tsmReadVNAConfig_Click);
            // 
            // tsmAddVNA
            // 
            this.tsmAddVNA.Name = "tsmAddVNA";
            this.tsmAddVNA.Size = new System.Drawing.Size(126, 22);
            this.tsmAddVNA.Text = "添加VNA";
            this.tsmAddVNA.Click += new System.EventHandler(this.tsmAddVNA_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 2000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // FrmVNAConfigMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 589);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(996, 628);
            this.Name = "FrmVNAConfigMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VNA配置主界面";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ImageList vnaImage;
        private System.Windows.Forms.ListView lvVNAIconItems;
        private System.Windows.Forms.TextBox vnaDetail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmVNADelete;
        private System.Windows.Forms.ToolStripMenuItem tsmVNAConnectTest;
        private System.Windows.Forms.ToolStripMenuItem tsmConfig;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCreateNewVNAConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmReadVNAConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmAddVNA;
        private System.Windows.Forms.ToolStripMenuItem tsmAlterConfig;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem tsmCalibration;
    }
}