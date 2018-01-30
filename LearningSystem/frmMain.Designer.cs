namespace LearningSystem
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.grbPPT = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treePPTList = new System.Windows.Forms.TreeView();
            this.axFramerControl1 = new AxDSOFramer.AxFramerControl();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.grbPPT.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axFramerControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // grbPPT
            // 
            this.grbPPT.Controls.Add(this.axFramerControl1);
            this.grbPPT.Location = new System.Drawing.Point(223, 34);
            this.grbPPT.Name = "grbPPT";
            this.grbPPT.Size = new System.Drawing.Size(889, 524);
            this.grbPPT.TabIndex = 0;
            this.grbPPT.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treePPTList);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 524);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "課程列表";
            // 
            // treePPTList
            // 
            this.treePPTList.Location = new System.Drawing.Point(6, 20);
            this.treePPTList.Name = "treePPTList";
            this.treePPTList.Size = new System.Drawing.Size(188, 498);
            this.treePPTList.TabIndex = 0;
            this.treePPTList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePPTList_AfterSelect);
            // 
            // axFramerControl1
            // 
            this.axFramerControl1.Enabled = true;
            this.axFramerControl1.Location = new System.Drawing.Point(6, 20);
            this.axFramerControl1.Name = "axFramerControl1";
            this.axFramerControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axFramerControl1.OcxState")));
            this.axFramerControl1.Size = new System.Drawing.Size(872, 498);
            this.axFramerControl1.TabIndex = 0;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.Black;
            this.txtInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtInfo.Location = new System.Drawing.Point(18, 7);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(979, 21);
            this.txtInfo.TabIndex = 2;
            this.txtInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(1003, 6);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(109, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 570);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbPPT);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grbPPT.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axFramerControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.GroupBox grbPPT;
        private AxDSOFramer.AxFramerControl axFramerControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treePPTList;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnUpload;
    }
}

