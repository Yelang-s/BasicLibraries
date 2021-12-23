namespace VNAControl.Forms
{
    partial class FrmCalKit
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
            this.cboxCalKits = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboxCalKits
            // 
            this.cboxCalKits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCalKits.FormattingEnabled = true;
            this.cboxCalKits.Location = new System.Drawing.Point(12, 12);
            this.cboxCalKits.Name = "cboxCalKits";
            this.cboxCalKits.Size = new System.Drawing.Size(121, 21);
            this.cboxCalKits.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(139, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmCalKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 44);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboxCalKits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCalKit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmCalKit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboxCalKits;
        private System.Windows.Forms.Button btnOK;
    }
}