namespace sdnFaceVerify_Test
{
    partial class FaceVerify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceVerify));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStartCapture = new System.Windows.Forms.Button();
            this.btnStopCapture = new System.Windows.Forms.Button();
            this.lbVerify = new System.Windows.Forms.Label();
            this.VisiblePhoto = new System.Windows.Forms.PictureBox();
            this.InfredPhoto = new System.Windows.Forms.PictureBox();
            this.RegistrationPhoto = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UploadRegistrationPhoto = new System.Windows.Forms.Button();
            this.sdnLiveness = new AxDUALCAMERALIVENESSCONTROLLib.AxDualCameraLivenessControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Verify = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VisiblePhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfredPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegistrationPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdnLiveness)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Verify);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.sdnLiveness);
            this.panel1.Controls.Add(this.UploadRegistrationPhoto);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.RegistrationPhoto);
            this.panel1.Controls.Add(this.InfredPhoto);
            this.panel1.Controls.Add(this.VisiblePhoto);
            this.panel1.Controls.Add(this.lbVerify);
            this.panel1.Controls.Add(this.btnStopCapture);
            this.panel1.Controls.Add(this.btnStartCapture);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1194, 480);
            this.panel1.TabIndex = 0;
            // 
            // btnStartCapture
            // 
            this.btnStartCapture.Location = new System.Drawing.Point(89, 445);
            this.btnStartCapture.Name = "btnStartCapture";
            this.btnStartCapture.Size = new System.Drawing.Size(131, 23);
            this.btnStartCapture.TabIndex = 1;
            this.btnStartCapture.Text = "开始捕获";
            this.btnStartCapture.UseVisualStyleBackColor = true;
            this.btnStartCapture.Click += new System.EventHandler(this.btnStartCapture_Click);
            // 
            // btnStopCapture
            // 
            this.btnStopCapture.Location = new System.Drawing.Point(265, 445);
            this.btnStopCapture.Name = "btnStopCapture";
            this.btnStopCapture.Size = new System.Drawing.Size(131, 23);
            this.btnStopCapture.TabIndex = 2;
            this.btnStopCapture.Text = "停止捕获";
            this.btnStopCapture.UseVisualStyleBackColor = true;
            this.btnStopCapture.Click += new System.EventHandler(this.btnStopCapture_Click);
            // 
            // lbVerify
            // 
            this.lbVerify.AutoSize = true;
            this.lbVerify.Location = new System.Drawing.Point(196, 417);
            this.lbVerify.Name = "lbVerify";
            this.lbVerify.Size = new System.Drawing.Size(77, 12);
            this.lbVerify.TabIndex = 3;
            this.lbVerify.Text = "未检测到人脸";
            // 
            // VisiblePhoto
            // 
            this.VisiblePhoto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.VisiblePhoto.Location = new System.Drawing.Point(576, 12);
            this.VisiblePhoto.Name = "VisiblePhoto";
            this.VisiblePhoto.Size = new System.Drawing.Size(208, 171);
            this.VisiblePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VisiblePhoto.TabIndex = 4;
            this.VisiblePhoto.TabStop = false;
            // 
            // InfredPhoto
            // 
            this.InfredPhoto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.InfredPhoto.Location = new System.Drawing.Point(576, 242);
            this.InfredPhoto.Name = "InfredPhoto";
            this.InfredPhoto.Size = new System.Drawing.Size(208, 171);
            this.InfredPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InfredPhoto.TabIndex = 5;
            this.InfredPhoto.TabStop = false;
            // 
            // RegistrationPhoto
            // 
            this.RegistrationPhoto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.RegistrationPhoto.Location = new System.Drawing.Point(919, 12);
            this.RegistrationPhoto.Name = "RegistrationPhoto";
            this.RegistrationPhoto.Size = new System.Drawing.Size(208, 171);
            this.RegistrationPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RegistrationPhoto.TabIndex = 6;
            this.RegistrationPhoto.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(647, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "可见光照片";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(647, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "红外线照片";
            // 
            // UploadRegistrationPhoto
            // 
            this.UploadRegistrationPhoto.Location = new System.Drawing.Point(972, 199);
            this.UploadRegistrationPhoto.Name = "UploadRegistrationPhoto";
            this.UploadRegistrationPhoto.Size = new System.Drawing.Size(124, 23);
            this.UploadRegistrationPhoto.TabIndex = 9;
            this.UploadRegistrationPhoto.Text = "上传登记照片…";
            this.UploadRegistrationPhoto.UseVisualStyleBackColor = true;
            this.UploadRegistrationPhoto.Click += new System.EventHandler(this.UploadRegistrationPhoto_Click);
            // 
            // sdnLiveness
            // 
            this.sdnLiveness.Enabled = true;
            this.sdnLiveness.Location = new System.Drawing.Point(12, 12);
            this.sdnLiveness.Name = "sdnLiveness";
            this.sdnLiveness.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("sdnLiveness.OcxState")));
            this.sdnLiveness.Size = new System.Drawing.Size(518, 386);
            this.sdnLiveness.TabIndex = 10;
            this.sdnLiveness.OnCaptureSuccessCallbackHandler += new System.EventHandler(this.sdnLiveness_OnCaptureSuccessCallbackHandler);
            this.sdnLiveness.OnCaptureStatus += new AxDUALCAMERALIVENESSCONTROLLib._DDualCameraLivenessControlEvents_OnCaptureStatusEventHandler(this.sdnLiveness_OnCaptureStatus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(992, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "比对结果:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1057, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(992, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "比对分数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1057, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "?";
            // 
            // Verify
            // 
            this.Verify.Location = new System.Drawing.Point(994, 354);
            this.Verify.Name = "Verify";
            this.Verify.Size = new System.Drawing.Size(75, 23);
            this.Verify.TabIndex = 16;
            this.Verify.Text = "比对";
            this.Verify.UseVisualStyleBackColor = true;
            this.Verify.Click += new System.EventHandler(this.Verify_Click);
            // 
            // FaceVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 480);
            this.Controls.Add(this.panel1);
            this.Name = "FaceVerify";
            this.Text = "FaceVerify";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceVerify_FormClosing);
            this.Load += new System.EventHandler(this.FaceVerify_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VisiblePhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfredPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegistrationPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdnLiveness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox VisiblePhoto;
        private System.Windows.Forms.Label lbVerify;
        private System.Windows.Forms.Button btnStopCapture;
        private System.Windows.Forms.Button btnStartCapture;
        private System.Windows.Forms.PictureBox RegistrationPhoto;
        private System.Windows.Forms.PictureBox InfredPhoto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button UploadRegistrationPhoto;
        private AxDUALCAMERALIVENESSCONTROLLib.AxDualCameraLivenessControl sdnLiveness;
        private System.Windows.Forms.Button Verify;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
    }
}