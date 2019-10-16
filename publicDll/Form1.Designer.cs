namespace publicDll
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
            this.btnTest = new sdnControls.sdnButtonEx.ButtonEx();
            this.btnCamera = new sdnControls.sdnButtonEx.ButtonEx();
            this.btnTestHttp = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnDownVideo = new System.Windows.Forms.Button();
            this.btnStopRec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(140, 135);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.Location = new System.Drawing.Point(24, 135);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(75, 23);
            this.btnCamera.TabIndex = 1;
            this.btnCamera.Text = "录像测试";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnTestHttp
            // 
            this.btnTestHttp.Location = new System.Drawing.Point(24, 163);
            this.btnTestHttp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTestHttp.Name = "btnTestHttp";
            this.btnTestHttp.Size = new System.Drawing.Size(50, 48);
            this.btnTestHttp.TabIndex = 2;
            this.btnTestHttp.Text = "测试http";
            this.btnTestHttp.UseVisualStyleBackColor = true;
            this.btnTestHttp.Click += new System.EventHandler(this.btnTestHttp_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(96, 176);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 3;
            this.btnRecord.Text = "开始录像";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnDownVideo
            // 
            this.btnDownVideo.Location = new System.Drawing.Point(140, 206);
            this.btnDownVideo.Name = "btnDownVideo";
            this.btnDownVideo.Size = new System.Drawing.Size(75, 23);
            this.btnDownVideo.TabIndex = 4;
            this.btnDownVideo.Text = "下载视频";
            this.btnDownVideo.UseVisualStyleBackColor = true;
            this.btnDownVideo.Click += new System.EventHandler(this.btnDownVideo_Click);
            // 
            // btnStopRec
            // 
            this.btnStopRec.Location = new System.Drawing.Point(177, 177);
            this.btnStopRec.Name = "btnStopRec";
            this.btnStopRec.Size = new System.Drawing.Size(75, 23);
            this.btnStopRec.TabIndex = 5;
            this.btnStopRec.Text = "结束录像";
            this.btnStopRec.UseVisualStyleBackColor = true;
            this.btnStopRec.Click += new System.EventHandler(this.btnStopRec_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnStopRec);
            this.Controls.Add(this.btnDownVideo);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnTestHttp);
            this.Controls.Add(this.btnCamera);
            this.Controls.Add(this.btnTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private sdnControls.sdnButtonEx.ButtonEx btnTest;
        private sdnControls.sdnButtonEx.ButtonEx btnCamera;
        private System.Windows.Forms.Button btnTestHttp;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnDownVideo;
        private System.Windows.Forms.Button btnStopRec;
    }
}

