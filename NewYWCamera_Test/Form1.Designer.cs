namespace NewYWCamera_Test
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnStopRec = new System.Windows.Forms.Button();
            this.btnRec = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picBoxShow = new System.Windows.Forms.PictureBox();
            this.btnCap2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 325);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCap2);
            this.panel3.Controls.Add(this.btnCapture);
            this.panel3.Controls.Add(this.btnStopRec);
            this.panel3.Controls.Add(this.btnRec);
            this.panel3.Controls.Add(this.btnLogin);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 254);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(654, 71);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(474, 20);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 3;
            this.btnCapture.Text = "抓拍";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnStopRec
            // 
            this.btnStopRec.Location = new System.Drawing.Point(370, 20);
            this.btnStopRec.Name = "btnStopRec";
            this.btnStopRec.Size = new System.Drawing.Size(75, 23);
            this.btnStopRec.TabIndex = 2;
            this.btnStopRec.Text = "停止录像";
            this.btnStopRec.UseVisualStyleBackColor = true;
            this.btnStopRec.Click += new System.EventHandler(this.btnStopRec_Click);
            // 
            // btnRec
            // 
            this.btnRec.Location = new System.Drawing.Point(239, 21);
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(75, 23);
            this.btnRec.TabIndex = 1;
            this.btnRec.Text = "录像";
            this.btnRec.UseVisualStyleBackColor = true;
            this.btnRec.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(67, 21);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picBoxShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(654, 248);
            this.panel2.TabIndex = 0;
            // 
            // picBoxShow
            // 
            this.picBoxShow.Location = new System.Drawing.Point(41, 31);
            this.picBoxShow.Name = "picBoxShow";
            this.picBoxShow.Size = new System.Drawing.Size(212, 139);
            this.picBoxShow.TabIndex = 0;
            this.picBoxShow.TabStop = false;
            // 
            // btnCap2
            // 
            this.btnCap2.Location = new System.Drawing.Point(567, 20);
            this.btnCap2.Name = "btnCap2";
            this.btnCap2.Size = new System.Drawing.Size(75, 23);
            this.btnCap2.TabIndex = 4;
            this.btnCap2.Text = "抓拍2";
            this.btnCap2.UseVisualStyleBackColor = true;
            this.btnCap2.Click += new System.EventHandler(this.btnCap2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(654, 325);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.TransparencyKey = System.Drawing.Color.White;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBoxShow;
        private System.Windows.Forms.Button btnRec;
        private System.Windows.Forms.Button btnStopRec;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnCap2;
    }
}

