namespace YWCamera
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
            this.plPlayer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCapter = new System.Windows.Forms.Button();
            this.btnStopRec = new System.Windows.Forms.Button();
            this.btnRec = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.plPlayer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(231, 4);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // plPlayer
            // 
            this.plPlayer.Controls.Add(this.txtPwd);
            this.plPlayer.Controls.Add(this.label3);
            this.plPlayer.Controls.Add(this.txtUid);
            this.plPlayer.Controls.Add(this.label2);
            this.plPlayer.Controls.Add(this.label1);
            this.plPlayer.Controls.Add(this.txtIp);
            this.plPlayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.plPlayer.Location = new System.Drawing.Point(0, 0);
            this.plPlayer.Name = "plPlayer";
            this.plPlayer.Size = new System.Drawing.Size(867, 528);
            this.plPlayer.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCapter);
            this.panel2.Controls.Add(this.btnStopRec);
            this.panel2.Controls.Add(this.btnRec);
            this.panel2.Controls.Add(this.btnPlay);
            this.panel2.Controls.Add(this.btnInit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 348);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(867, 30);
            this.panel2.TabIndex = 2;
            // 
            // btnCapter
            // 
            this.btnCapter.Location = new System.Drawing.Point(768, 4);
            this.btnCapter.Name = "btnCapter";
            this.btnCapter.Size = new System.Drawing.Size(75, 23);
            this.btnCapter.TabIndex = 4;
            this.btnCapter.Text = "抓拍图片";
            this.btnCapter.UseVisualStyleBackColor = true;
            this.btnCapter.Click += new System.EventHandler(this.btnCapter_Click);
            // 
            // btnStopRec
            // 
            this.btnStopRec.Location = new System.Drawing.Point(652, 4);
            this.btnStopRec.Name = "btnStopRec";
            this.btnStopRec.Size = new System.Drawing.Size(75, 23);
            this.btnStopRec.TabIndex = 3;
            this.btnStopRec.Text = "录像结束";
            this.btnStopRec.UseVisualStyleBackColor = true;
            this.btnStopRec.Click += new System.EventHandler(this.btnStopRec_Click);
            // 
            // btnRec
            // 
            this.btnRec.Location = new System.Drawing.Point(520, 3);
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(75, 23);
            this.btnRec.TabIndex = 2;
            this.btnRec.Text = "录像开始";
            this.btnRec.UseVisualStyleBackColor = true;
            this.btnRec.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(368, 4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "播放";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(80, 26);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(119, 21);
            this.txtIp.TabIndex = 0;
            this.txtIp.Text = "192.1.6.96";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "摄像头IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(276, 26);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(111, 21);
            this.txtUid.TabIndex = 3;
            this.txtUid.Text = "888888";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(455, 25);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(131, 21);
            this.txtPwd.TabIndex = 5;
            this.txtPwd.Text = "888888";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 378);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.plPlayer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.plPlayer.ResumeLayout(false);
            this.plPlayer.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Panel plPlayer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStopRec;
        private System.Windows.Forms.Button btnRec;
        private System.Windows.Forms.Button btnCapter;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIp;
    }
}

