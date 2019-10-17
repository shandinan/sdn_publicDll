namespace winForm_test
{
    partial class Form1
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
            this.btnInit = new System.Windows.Forms.Button();
            this.btnStartRec = new System.Windows.Forms.Button();
            this.btnStopRec = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picShow = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 57);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnStartRec
            // 
            this.btnStartRec.Location = new System.Drawing.Point(12, 96);
            this.btnStartRec.Name = "btnStartRec";
            this.btnStartRec.Size = new System.Drawing.Size(75, 23);
            this.btnStartRec.TabIndex = 1;
            this.btnStartRec.Text = "开始录像";
            this.btnStartRec.UseVisualStyleBackColor = true;
            this.btnStartRec.Click += new System.EventHandler(this.btnStartRec_Click);
            // 
            // btnStopRec
            // 
            this.btnStopRec.Location = new System.Drawing.Point(12, 134);
            this.btnStopRec.Name = "btnStopRec";
            this.btnStopRec.Size = new System.Drawing.Size(75, 23);
            this.btnStopRec.TabIndex = 2;
            this.btnStopRec.Text = "结束录像";
            this.btnStopRec.UseVisualStyleBackColor = true;
            this.btnStopRec.Click += new System.EventHandler(this.btnStopRec_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picShow);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(131, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 261);
            this.panel1.TabIndex = 3;
            // 
            // picShow
            // 
            this.picShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picShow.Location = new System.Drawing.Point(0, 0);
            this.picShow.Name = "picShow";
            this.picShow.Size = new System.Drawing.Size(153, 261);
            this.picShow.TabIndex = 0;
            this.picShow.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStopRec);
            this.Controls.Add(this.btnStartRec);
            this.Controls.Add(this.btnInit);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnStartRec;
        private System.Windows.Forms.Button btnStopRec;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picShow;
    }
}

