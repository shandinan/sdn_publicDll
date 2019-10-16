namespace DualCaptorAndVerifyDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StartCapture = new System.Windows.Forms.Button();
            this.StopCapture = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UploadRegistrationPhoto = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Verify = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.InfredPhoto = new System.Windows.Forms.PictureBox();
            this.RegistrationPhoto = new System.Windows.Forms.PictureBox();
            this.VisiblePhoto = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axDualCameraLivenessControl1 = new AxDUALCAMERALIVENESSCONTROLLib.AxDualCameraLivenessControl();
            ((System.ComponentModel.ISupportInitialize)(this.InfredPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegistrationPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisiblePhoto)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDualCameraLivenessControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartCapture
            // 
            this.StartCapture.Location = new System.Drawing.Point(44, 547);
            this.StartCapture.Name = "StartCapture";
            this.StartCapture.Size = new System.Drawing.Size(173, 23);
            this.StartCapture.TabIndex = 1;
            this.StartCapture.TabStop = false;
            this.StartCapture.Text = "开始捕获";
            this.StartCapture.UseVisualStyleBackColor = true;
            this.StartCapture.Click += new System.EventHandler(this.StartCapture_Click_1);
            // 
            // StopCapture
            // 
            this.StopCapture.Location = new System.Drawing.Point(271, 547);
            this.StopCapture.Name = "StopCapture";
            this.StopCapture.Size = new System.Drawing.Size(173, 23);
            this.StopCapture.TabIndex = 2;
            this.StopCapture.Text = "停止捕获";
            this.StopCapture.UseVisualStyleBackColor = true;
            this.StopCapture.Click += new System.EventHandler(this.StopCapture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(754, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "可见光照片";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "红外照片";
            // 
            // UploadRegistrationPhoto
            // 
            this.UploadRegistrationPhoto.Location = new System.Drawing.Point(986, 260);
            this.UploadRegistrationPhoto.Name = "UploadRegistrationPhoto";
            this.UploadRegistrationPhoto.Size = new System.Drawing.Size(123, 23);
            this.UploadRegistrationPhoto.TabIndex = 8;
            this.UploadRegistrationPhoto.Text = "上传登记照";
            this.UploadRegistrationPhoto.UseVisualStyleBackColor = true;
            this.UploadRegistrationPhoto.Click += new System.EventHandler(this.UploadRegistrationPhoto_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(988, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "比对结果:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(988, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "比对分数：";
            // 
            // Verify
            // 
            this.Verify.Location = new System.Drawing.Point(1008, 460);
            this.Verify.Name = "Verify";
            this.Verify.Size = new System.Drawing.Size(75, 23);
            this.Verify.TabIndex = 11;
            this.Verify.Text = "比对";
            this.Verify.UseVisualStyleBackColor = true;
            this.Verify.Click += new System.EventHandler(this.Verify_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1046, 383);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "?";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1047, 411);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 532);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "未检测到人脸";
            // 
            // InfredPhoto
            // 
            this.InfredPhoto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.InfredPhoto.Location = new System.Drawing.Point(691, 331);
            this.InfredPhoto.Name = "InfredPhoto";
            this.InfredPhoto.Size = new System.Drawing.Size(200, 200);
            this.InfredPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InfredPhoto.TabIndex = 5;
            this.InfredPhoto.TabStop = false;
            // 
            // RegistrationPhoto
            // 
            this.RegistrationPhoto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.RegistrationPhoto.Location = new System.Drawing.Point(944, 53);
            this.RegistrationPhoto.Name = "RegistrationPhoto";
            this.RegistrationPhoto.Size = new System.Drawing.Size(200, 200);
            this.RegistrationPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RegistrationPhoto.TabIndex = 4;
            this.RegistrationPhoto.TabStop = false;
            // 
            // VisiblePhoto
            // 
            this.VisiblePhoto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.VisiblePhoto.Location = new System.Drawing.Point(691, 53);
            this.VisiblePhoto.Name = "VisiblePhoto";
            this.VisiblePhoto.Size = new System.Drawing.Size(200, 200);
            this.VisiblePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VisiblePhoto.TabIndex = 3;
            this.VisiblePhoto.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.axDualCameraLivenessControl1);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 443);
            this.panel1.TabIndex = 15;
            // 
            // axDualCameraLivenessControl1
            // 
            this.axDualCameraLivenessControl1.Enabled = true;
            this.axDualCameraLivenessControl1.Location = new System.Drawing.Point(3, 3);
            this.axDualCameraLivenessControl1.Name = "axDualCameraLivenessControl1";
            this.axDualCameraLivenessControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axDualCameraLivenessControl1.OcxState")));
            this.axDualCameraLivenessControl1.Size = new System.Drawing.Size(665, 435);
            this.axDualCameraLivenessControl1.TabIndex = 0;
            this.axDualCameraLivenessControl1.OnCaptureSuccessCallbackHandler += new System.EventHandler(this.axDualCameraLivenessControl2_OnCaptureSuccessCallbackHandler);
            this.axDualCameraLivenessControl1.OnCaptureStatus += new AxDUALCAMERALIVENESSCONTROLLib._DDualCameraLivenessControlEvents_OnCaptureStatusEventHandler(this.axDualCameraLivenessControl2_OnCaptureStatus);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Verify);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UploadRegistrationPhoto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InfredPhoto);
            this.Controls.Add(this.RegistrationPhoto);
            this.Controls.Add(this.VisiblePhoto);
            this.Controls.Add(this.StopCapture);
            this.Controls.Add(this.StartCapture);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InfredPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegistrationPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisiblePhoto)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axDualCameraLivenessControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartCapture;
        private System.Windows.Forms.Button StopCapture;
        private System.Windows.Forms.PictureBox VisiblePhoto;
        private System.Windows.Forms.PictureBox RegistrationPhoto;
        private System.Windows.Forms.PictureBox InfredPhoto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button UploadRegistrationPhoto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Verify;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private AxDUALCAMERALIVENESSCONTROLLib.AxDualCameraLivenessControl axDualCameraLivenessControl1;


    }
}

